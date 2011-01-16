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

        #region Methods
        private string PrepareVolumeInfo(long start, long end)
        {
            if ((start >= 0) && (end <= context.Drive.Size))
            {
                long startSector = start / context.Drive.Geometry.BytesPerSector;
                long endSector = (end - 1) / context.Drive.Geometry.BytesPerSector;
                PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs(startSector, context.Drive.Geometry);
                PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs(endSector, context.Drive.Geometry);

                return string.Format("{0}/{1}/{2} - {3}/{4}/{5}",
                                     chsStartOffset.Cylinders, chsStartOffset.TracksPerCylinder, chsStartOffset.SectorsPerTrack,
                                     chsEndOffset.Cylinders, chsEndOffset.TracksPerCylinder, chsEndOffset.SectorsPerTrack);

            }
            else
            {
                return "n/a";
            }
        }
        #endregion

        #region Events
        private void ResultPage_PageActivated(object sender, EventArgs e)
        {
            lsvHeader.Items.Clear();
            foreach (PageContext.Header header in context.HeaderList)
            {
                // A TrueCrypt header has been found: this could be either a normal header at the beginning of the 
                // volume or a backup header at the end of the volume. Moreover the header could be either the header 
                // of a normal or a hidden volume. The information whether a backup or a normal header has been found
                // is not stored within the header - therefore print both possibilities. The hidden volume information
                // can be read from the header and therefore this can be handled transparently. Currently only header
                // version 4 (TrueCrypt 6) and 5 (TrueCrypt 7) are supported - however the tool also tries to work with
                // newer versions
                PhysicalDrive.CylinderHeadSector chsOffset = PhysicalDrive.LbaToChs(header.Lba, context.Drive.Geometry);

                ListViewItem item = new ListViewItem(string.Format("{0}/{1}/{2}", chsOffset.Cylinders, chsOffset.TracksPerCylinder, chsOffset.SectorsPerTrack));
                item.Tag = header;

                item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(header.CryptoInfo.VolumeSize));
                item.SubItems.Add(header.CryptoInfo.hiddenVolume.ToString());
                item.SubItems.Add(header.CryptoInfo.HeaderVersion.ToString());
                item.SubItems.Add((header.CryptoInfo.HeaderVersion >= 4).ToString());

                if (header.CryptoInfo.HeaderVersion >= 4)
                {
                    // assume a normal header
                    long start = (header.Lba * context.Drive.Geometry.BytesPerSector);
                    if (header.CryptoInfo.hiddenVolume)
                    {
                        // in case of a hidden TrueCrypt volume the header has an offset of a complete TrueCrypt header
                        start -= TrueCrypt.TC_VOLUME_HEADER_SIZE;
                    }
                    long end = start + (long)(header.CryptoInfo.EncryptedAreaStart + header.CryptoInfo.EncryptedAreaLength + TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE);
                    if (header.CryptoInfo.hiddenVolume)
                    {
                        // the offset of a hidden volume does not include the TrueCrypt header group unlike the offset 
                        // of a normal volume
                        end += TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE;
                    }
                    item.SubItems.Add(PrepareVolumeInfo(start, end));

                    // assume an embedded backup header
                    end = (header.Lba * context.Drive.Geometry.BytesPerSector);
                    // in case of a hidden TrueCrypt volume the header has an offset of a complete TrueCrypt header
                    end += (header.CryptoInfo.hiddenVolume) ? TrueCrypt.TC_VOLUME_HEADER_SIZE : TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE;
                    start = end - (long)(header.CryptoInfo.EncryptedAreaStart + header.CryptoInfo.EncryptedAreaLength + TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE);
                    if (header.CryptoInfo.hiddenVolume)
                    {
                        // the offset of a hidden volume does not include the TrueCrypt header group unlike the offset 
                        // of a normal volume
                        start -= TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE;
                    }
                    item.SubItems.Add(PrepareVolumeInfo(start, end));
                }
                else
                {
                    // only TrueCrypt header version 4 and newer are supported
                    item.SubItems.Add("n/a");
                    item.SubItems.Add("n/a");
                }
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

            // skip the analyzer page when going back
            e.PageCount = 2;
        }
        #endregion
    }
}
