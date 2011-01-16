using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace TestCrypt.Pages
{
    public partial class ResultPage : WizardPage
    {
        #region Attributes
        private PageContext context;
        #endregion

        #region Constructors
        public ResultPage(PageContext context)
        {
            InitializeComponent();

            this.context = context;

            // this wizard page is always ready
            this.ready = true;
        }
        #endregion

        #region Events
        private void ResultPage_PageActivated(object sender, EventArgs e)
        {
            lsvHeader.Items.Clear();
            foreach (PageContext.Header header in context.HeaderList)
            {
                PhysicalDrive.CylinderHeadSector chsOffset = PhysicalDrive.LbaToChs(header.Lba, context.Drive.Geometry);

                ListViewItem item = new ListViewItem(string.Format("{1}/{2}/{3} ({0} LBA)", header.Lba, chsOffset.Cylinders, chsOffset.TracksPerCylinder, chsOffset.SectorsPerTrack));
                item.Tag = header;

                item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(header.CryptoInfo.VolumeSize));
                item.SubItems.Add(header.CryptoInfo.VolumeSize.ToString());
                lsvHeader.Items.Add(item);
            }
            lsvHeader.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ResultPage_PageBack(object sender, PageTransitionEventArgs e)
        {
            if (MessageBox.Show(this, "If you continue the analyzer results will be cleared. Are you sure to continue?", "TestCrypt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void lsvHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvHeader.SelectedItems.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                // a TrueCrypt header has been found: this could be either a normal header at the beginning of the 
                // volume or a backup header at the end of the volume
                PageContext.Header header = (PageContext.Header)lsvHeader.SelectedItems[0].Tag;
                uint headerSectorCount = TrueCrypt.TC_VOLUME_HEADER_SIZE / context.Drive.Geometry.BytesPerSector;
                Int64 endSectorTc = header.Lba + (headerSectorCount - 1);
                Int64 startSectorTc = header.Lba - (Int64)((header.CryptoInfo.VolumeSize / context.Drive.Geometry.BytesPerSector) + headerSectorCount);
                if ((startSectorTc >= 0) && (endSectorTc <= (context.Drive.Size / context.Drive.Geometry.BytesPerSector)))
                {
                    PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs(startSectorTc, context.Drive.Geometry);
                    PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs(endSectorTc, context.Drive.Geometry);

                    sb.AppendLine(string.Format("Possible backup header: {1}/{2}/{3} ({0} LBA) - {5}/{6}/{7} ({4} LBA)",
                                  startSectorTc, chsStartOffset.Cylinders, chsStartOffset.TracksPerCylinder, chsStartOffset.SectorsPerTrack,
                                  endSectorTc, chsEndOffset.Cylinders, chsEndOffset.TracksPerCylinder, chsEndOffset.SectorsPerTrack));
                }
                else
                {
                    sb.AppendLine(string.Format("Backup header out of bound: {0} LBA - {1} LBA", startSectorTc, endSectorTc));
                }

                startSectorTc = header.Lba;
                endSectorTc = startSectorTc + (Int64)((header.CryptoInfo.VolumeSize / context.Drive.Geometry.BytesPerSector) + (2 * headerSectorCount) - 1);
                if ((startSectorTc >= 0) && (endSectorTc <= (context.Drive.Size / context.Drive.Geometry.BytesPerSector)))
                {
                    PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs(startSectorTc, context.Drive.Geometry);
                    PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs(endSectorTc, context.Drive.Geometry);

                    sb.AppendLine(string.Format("Possible normal header: {1}/{2}/{3} ({0} LBA) - {5}/{6}/{7} ({4} LBA)",
                                  startSectorTc, chsStartOffset.Cylinders, chsStartOffset.TracksPerCylinder, chsStartOffset.SectorsPerTrack,
                                  endSectorTc, chsEndOffset.Cylinders, chsEndOffset.TracksPerCylinder, chsEndOffset.SectorsPerTrack));
                }
                else
                {
                    sb.AppendLine(string.Format("Normal header out of bound: {0} LBA - {1} LBA", startSectorTc, endSectorTc));
                }

                txtHeaderInfo.Text = sb.ToString();
            }
            else
            {
                txtHeaderInfo.Text = "";
            }
        }
        #endregion
    }
}
