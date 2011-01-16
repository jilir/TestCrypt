using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    public partial class PartitionAnalyzerPage : WizardPage
    {
        #region Attributes
        private PageContext context;

        private string volume;
        #endregion

        #region Constructor
        public PartitionAnalyzerPage(PageContext context)
        {
            InitializeComponent();

            this.context = context;

            // this wizard page is always ready
            this.ready = true;
        }
        #endregion

        #region Methods
        private void ResetBeginEndGroupBox()
        {
            grpBegin.Enabled = false;
            grpEnd.Enabled = false;
            txtBeginSectorsAfter.Value = 0;
            txtBeginSectorsBefore.Value = 0;
            txtEndSectorsAfter.Value = 0;
            txtEndSectorsBefore.Value = 0;
            optBeginNoAnalyze.Checked = true;
            optEndNoAnalyze.Checked = true;
        }

        private void SetAnalyzer(List<PageContext.PartitionAnalyzer> list, PageContext.AnalyzeType analyzerType, uint sectorsBefore, uint sectorsAfter)
        {
            PhysicalDrive.PARTITION_INFORMATION partition = (PhysicalDrive.PARTITION_INFORMATION)lsvPartitions.SelectedItems[0].Tag;

            // check whether to add new or to update existing analyzer parameters
            PageContext.PartitionAnalyzer analyzer = null;
            foreach (PageContext.PartitionAnalyzer partitionAnalyzer in list)
            {
                if (partitionAnalyzer.PartitionNumber == partition.PartitionNumber)
                {
                    analyzer = partitionAnalyzer;
                    break;
                }
            }

            if (analyzer == null)
            {
                // create and add new analyzer parameters
                if (analyzerType != PageContext.AnalyzeType.None)
                {
                    analyzer = new PageContext.PartitionAnalyzer(partition.PartitionNumber, analyzerType, sectorsBefore, sectorsAfter);
                    list.Add(analyzer);
                }
            }
            else
            {
                // update existing analyzer parameters
                if (analyzerType != PageContext.AnalyzeType.None)
                {
                    analyzer.AnalyzerType = analyzerType;
                    analyzer.SectorsBefore = sectorsBefore;
                    analyzer.SectorsAfter = sectorsAfter;
                }
                else
                {
                    list.Remove(analyzer);
                }
            }
        }
        #endregion

        #region Events
        private void PartitionAnalyzerPage_PageActivated(object sender, EventArgs e)
        {
            // ensure that a drive has been selected
            if (context.Drive == null)
            {
                throw new NullReferenceException();
            }

            // discard stored partition analyzer parameters in case another drive has been selected
            if (volume != null)
            {
                if (context.Drive.Volume != volume)
                {
                    context.PartitionBeginAnalyzer.Clear();
                    context.PartitionEndAnalyzer.Clear();
                }
            }
            volume = context.Drive.Volume;

            ResetBeginEndGroupBox();
            lsvPartitions.Items.Clear();
            Enabled = (context.Drive.Partitions.Count > 0);
            foreach (PhysicalDrive.PARTITION_INFORMATION partition in context.Drive.Partitions)
            {
                ListViewItem item = new ListViewItem(partition.PartitionNumber.ToString());
                item.Tag = partition;

                PhysicalDrive.DISK_GEOMETRY geometry = context.Drive.Geometry;
                PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs((partition.StartingOffset / geometry.BytesPerSector), geometry);
                item.SubItems.Add(string.Format("{0}/{1}/{2}", chsStartOffset.Cylinders, chsStartOffset.TracksPerCylinder, chsStartOffset.SectorsPerTrack));
                PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs((((partition.StartingOffset + partition.PartitionLength) / geometry.BytesPerSector) - 1), geometry);
                item.SubItems.Add(string.Format("{0}/{1}/{2}", chsEndOffset.Cylinders, chsEndOffset.TracksPerCylinder, chsEndOffset.SectorsPerTrack));
                item.SubItems.Add(partition.PartitionLength.ToString());
                item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(partition.PartitionLength));
                string type = PartitionTypes.GetPartitionType(partition.PartitionType);
                if (type == null)
                {
                    type = "Unknown";
                }
                item.SubItems.Add(String.Format("0x{0:X2} - {1}", partition.PartitionType, type));           
              
                lsvPartitions.Items.Add(item);
            }
            lsvPartitions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void lsvPartitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            // It seems that Java is not the only programming language which requires bad hacks to build up some
            // "basic" functionality: the SelectedIndexChanged event will fire for both selection and deselection of
            // items. This causes bad UI flickering because some controls on this wizard page will be disabled when no 
            // item in the ListView is selected. To "solve" this issue, both the KeyUp and MouseUp event will be used 
            // to detect more or less reliable whether the user has not selected an item. The SelectedIndexChanged
            // handler will only do something in case at least one item is selected
            if (lsvPartitions.SelectedItems.Count > 0)
            {
                PhysicalDrive.PARTITION_INFORMATION partition = (PhysicalDrive.PARTITION_INFORMATION)lsvPartitions.SelectedItems[0].Tag;

                // prepare the "Begin of Partition" controls
                PageContext.PartitionAnalyzer analyzer = null;
                foreach (PageContext.PartitionAnalyzer partitionAnalyzer in context.PartitionBeginAnalyzer)
                {
                    if (partitionAnalyzer.PartitionNumber == partition.PartitionNumber)
                    {
                        analyzer = partitionAnalyzer;
                        break;
                    }
                }

                txtBeginSectorsBefore.Minimum = 0;
                txtBeginSectorsBefore.Maximum = partition.StartingOffset / context.Drive.Geometry.BytesPerSector;
                txtBeginSectorsBefore.Value = (analyzer != null) ? analyzer.SectorsBefore : 0;
                txtBeginSectorsAfter.Minimum = 0;
                txtBeginSectorsAfter.Maximum = (context.Drive.Size - partition.StartingOffset) / context.Drive.Geometry.BytesPerSector;
                txtBeginSectorsAfter.Value = (analyzer != null) ? analyzer.SectorsAfter : 0;
                switch ((analyzer != null) ? analyzer.AnalyzerType : PageContext.AnalyzeType.None)
                {
                    case PageContext.AnalyzeType.Automatic:
                        optBeginAutomatic.Checked = true;
                        break;
                    case PageContext.AnalyzeType.Manual:
                        optBeginManual.Checked = true;
                        break;
                    case PageContext.AnalyzeType.None:
                        optBeginNoAnalyze.Checked = true;
                        break;
                }               
                grpBegin.Enabled = true;

                // prepare the "End of Partition" controls
                analyzer = null;
                foreach (PageContext.PartitionAnalyzer partitionAnalyzer in context.PartitionEndAnalyzer)
                {
                    if (partitionAnalyzer.PartitionNumber == partition.PartitionNumber)
                    {
                        analyzer = partitionAnalyzer;
                        break;
                    }
                }

                txtEndSectorsBefore.Minimum = 0;
                txtEndSectorsBefore.Maximum = (partition.StartingOffset + partition.PartitionLength) / context.Drive.Geometry.BytesPerSector;
                txtEndSectorsBefore.Value = (analyzer != null) ? analyzer.SectorsBefore : 0;
                txtEndSectorsAfter.Minimum = 0;
                txtEndSectorsAfter.Maximum = (context.Drive.Size - (partition.StartingOffset + partition.PartitionLength)) / context.Drive.Geometry.BytesPerSector;
                txtEndSectorsAfter.Value = (analyzer != null) ? analyzer.SectorsAfter : 0;
                switch ((analyzer != null) ? analyzer.AnalyzerType : PageContext.AnalyzeType.None)
                {
                    case PageContext.AnalyzeType.Automatic:
                        optEndAutomatic.Checked = true;
                        break;
                    case PageContext.AnalyzeType.Manual:
                        optEndManual.Checked = true;
                        break;
                    case PageContext.AnalyzeType.None:
                        optEndNoAnalyze.Checked = true;
                        break;
                }
                grpEnd.Enabled = true;
            }      
        }

        private void lsvPartitions_KeyUp(object sender, KeyEventArgs e)
        {
            // see the SelectedIndexChanged event handler for further explanation why this event handler is required
            if (lsvPartitions.SelectedItems.Count == 0)
            {
                ResetBeginEndGroupBox();
            }
        }

        private void lsvPartitions_MouseUp(object sender, MouseEventArgs e)
        {
            // see the SelectedIndexChanged event handler for further explanation why this event handler is required
            if (lsvPartitions.SelectedItems.Count == 0)
            {
                ResetBeginEndGroupBox();
            }
        }

        private void btnBeginSet_Click(object sender, EventArgs e)
        {
            if (lsvPartitions.SelectedItems.Count > 0)
            {
                PageContext.AnalyzeType analyzerType;
                if (optBeginAutomatic.Checked)
                {
                    analyzerType = PageContext.AnalyzeType.Automatic;
                }
                else if (optBeginManual.Checked)
                {
                    analyzerType = PageContext.AnalyzeType.Manual;
                }
                else
                {
                    analyzerType = PageContext.AnalyzeType.None;
                }

                SetAnalyzer(context.PartitionBeginAnalyzer, analyzerType, (uint)txtBeginSectorsBefore.Value, (uint)txtBeginSectorsAfter.Value);
            }
        }

        private void btnEndSet_Click(object sender, EventArgs e)
        {
            if (lsvPartitions.SelectedItems.Count > 0)
            {
                PageContext.AnalyzeType analyzerType;
                if (optEndAutomatic.Checked)
                {
                    analyzerType = PageContext.AnalyzeType.Automatic;
                }
                else if (optEndManual.Checked)
                {
                    analyzerType = PageContext.AnalyzeType.Manual;
                }
                else
                {
                    analyzerType = PageContext.AnalyzeType.None;
                }

                SetAnalyzer(context.PartitionEndAnalyzer, analyzerType, (uint)txtEndSectorsBefore.Value, (uint)txtEndSectorsAfter.Value);
            }
        }

        private void grpBegin_CheckedChanged(object sender, EventArgs e)
        {
            lblBeginSectorsAfter.Enabled = optBeginManual.Checked;
            txtBeginSectorsAfter.Enabled = optBeginManual.Checked;
            lblBeginSectorsBefore.Enabled = optBeginManual.Checked;
            txtBeginSectorsBefore.Enabled = optBeginManual.Checked;
        }

        private void grpEnd_CheckedChanged(object sender, EventArgs e)
        {
            lblEndSectorsAfter.Enabled = optEndManual.Checked;
            txtEndSectorsAfter.Enabled = optEndManual.Checked;
            lblEndSectorsBefore.Enabled = optEndManual.Checked;
            txtEndSectorsBefore.Enabled = optEndManual.Checked;
        }
        #endregion
    }
}
