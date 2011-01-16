using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    public partial class SummaryPage : WizardPage
    {
        #region Attribute
        private PageContext context;
        #endregion

        #region Constructors
        public SummaryPage(PageContext context)
        {
            InitializeComponent();

            this.context = context;

            // this wizard page is always ready
            this.ready = true;
        }
        #endregion

        #region Methods
        private void PreparePartitionAnalyzerSummary(StringBuilder summaryBuilder, List<PageContext.PartitionAnalyzer> analyzerList)
        {
            foreach (PhysicalDrive.PARTITION_INFORMATION partition in context.Drive.Partitions)
            {
                string text = null;
                foreach (PageContext.PartitionAnalyzer analyzer in analyzerList)
                {
                    if (analyzer.PartitionNumber == partition.PartitionNumber)
                    {
                        if (analyzer.AnalyzerType == PageContext.AnalyzeType.Automatic)
                        {
                            text = string.Format("Partition {0}: Automatic", partition.PartitionNumber);
                        }
                        else
                        {
                            text = string.Format("Partition {0}: {1} Sectors Before, {2} Sectors After", partition.PartitionNumber, analyzer.SectorsBefore, analyzer.SectorsAfter);
                        }
                        break;
                    }
                }

                if (text == null)
                {
                    text = string.Format("Partition {0}: None", partition.PartitionNumber);
                }
                summaryBuilder.AppendLine(text);
            }
        }
        #endregion

        #region Events
        private void SummaryPage_PageActivated(object sender, EventArgs e)
        {
            // prepare the volume summary
            PhysicalDrive.DriveInfo drive = context.Drive;
            StringBuilder summaryBuilder = new StringBuilder();
            summaryBuilder.AppendLine(string.Format("Volume {0}", drive.Volume));
            summaryBuilder.AppendLine("================================================================================");
            summaryBuilder.AppendLine(string.Format("Type: {0}", drive.Geometry.MediaType.ToString()));
            summaryBuilder.AppendLine(string.Format("Size: {0} Bytes", drive.Size));
            summaryBuilder.AppendLine(string.Format("Bytes per Sector: {0}", drive.Geometry.BytesPerSector));
            summaryBuilder.AppendLine(string.Format("Geometry: {0}/{1}/{2}", drive.Geometry.Cylinders, drive.Geometry.TracksPerCylinder, drive.Geometry.SectorsPerTrack));
            summaryBuilder.AppendLine();

            // prepare the partition summary
            foreach (PhysicalDrive.PARTITION_INFORMATION partition in drive.Partitions)
            {
                summaryBuilder.AppendLine(string.Format("Partition {0}", partition.PartitionNumber));
                summaryBuilder.AppendLine("================================================================================");
                summaryBuilder.AppendLine(string.Format("Type: {0} - {1}", partition.PartitionType, PartitionTypes.GetPartitionType(partition.PartitionType)));
                summaryBuilder.AppendLine(string.Format("Bootable: {0}", partition.BootIndicator.ToString()));
                PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs((partition.StartingOffset / drive.Geometry.BytesPerSector), drive.Geometry);
                summaryBuilder.AppendLine(string.Format("Start Offset: {1}/{2}/{3} ({0} Bytes)", partition.StartingOffset, chsStartOffset.Cylinders, chsStartOffset.TracksPerCylinder, chsStartOffset.SectorsPerTrack));
                PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs((((partition.StartingOffset + partition.PartitionLength) / drive.Geometry.BytesPerSector) - 1), drive.Geometry);
                summaryBuilder.AppendLine(string.Format("End Offset: {1}/{2}/{3} ({0} Bytes)", partition.StartingOffset + partition.PartitionLength, chsEndOffset.Cylinders, chsEndOffset.TracksPerCylinder, chsEndOffset.SectorsPerTrack));
                summaryBuilder.AppendLine(string.Format("Hidden Sectors: {0}", partition.HiddenSectors));
                summaryBuilder.AppendLine();
            }

            // prepare the partition analyzer summary
            summaryBuilder.AppendLine("Begin of Partition Analyzer");
            summaryBuilder.AppendLine("================================================================================");
            PreparePartitionAnalyzerSummary(summaryBuilder, context.PartitionBeginAnalyzer);
            summaryBuilder.AppendLine();

            summaryBuilder.AppendLine("End of Partition Analyzer");
            summaryBuilder.AppendLine("================================================================================");
            PreparePartitionAnalyzerSummary(summaryBuilder, context.PartitionEndAnalyzer);
            summaryBuilder.AppendLine();

            // prepare the volume analyzer summary
            summaryBuilder.AppendLine("Begin of Volume Analyzer");
            summaryBuilder.AppendLine("================================================================================");
            if (context.VolumeBeginAnalyzer.AnalyzerType != PageContext.AnalyzeType.Manual)
            {
                summaryBuilder.AppendLine(context.VolumeBeginAnalyzer.AnalyzerType.ToString());
            }
            else
            {
                summaryBuilder.AppendLine(string.Format("{0} Sectors", context.VolumeBeginAnalyzer.Sectors));
            }            
            summaryBuilder.AppendLine();

            summaryBuilder.AppendLine("End of Volume Analyzer");
            summaryBuilder.AppendLine("================================================================================");
             if (context.VolumeEndAnalyzer.AnalyzerType != PageContext.AnalyzeType.Manual)
            {
                summaryBuilder.AppendLine(context.VolumeEndAnalyzer.AnalyzerType.ToString());
            }
            else
            {
                summaryBuilder.AppendLine(string.Format("{0} Sectors", context.VolumeEndAnalyzer.Sectors));
            }            
            summaryBuilder.AppendLine();

            // prepare the optimized scan ranges summary
            summaryBuilder.AppendLine("Scan Ranges (optimized)");
            summaryBuilder.AppendLine("================================================================================");
            foreach (PageContext.ScanRange range in context.GetOptimizedScanRanges())
            {
                PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs(range.StartLba, drive.Geometry);
                PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs(range.EndLba, drive.Geometry);
                summaryBuilder.AppendLine(string.Format("{1}/{2}/{3} ({0} LBA) - {5}/{6}/{7} ({4} LBA)", 
                                                        range.StartLba, chsStartOffset.Cylinders, chsStartOffset.TracksPerCylinder, chsStartOffset.SectorsPerTrack,
                                                        range.EndLba, chsEndOffset.Cylinders, chsEndOffset.TracksPerCylinder, chsEndOffset.SectorsPerTrack));   
            }
            
            // display the summary
            txtSummary.Text = summaryBuilder.ToString();
        }

        private void SummaryPage_PageNext(object sender, PageTransitionEventArgs e)
        {
            if (context.GetOptimizedScanRanges().Count == 0)
            {
                MessageBox.Show(this, "No scan ranges have been defined. In order to continue at least one scan range has to be defined first.", "TestCrypt", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }
        #endregion
    }
}
