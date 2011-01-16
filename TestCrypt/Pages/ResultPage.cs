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
        #region Local Types
        private class HeaderInfo
        {
            #region Attributes
            /// <summary>
            /// The start offset in bytes of the volume when assuming a normal header.
            /// </summary>
            private long normalStart;

            /// <summary>
            /// The end offset in bytes of the volume when assuming a normal header.
            /// </summary>
            private long normalEnd;

            /// <summary>
            /// The start offset in bytes of the volume when assuming a backup header.
            /// </summary>
            private long backupStart;

            /// <summary>
            /// The end offset in bytes of the volume when assuming a backup header.
            /// </summary>
            private long backupEnd;

            /// <summary>
            /// The TrueCrypt header of the volume.
            /// </summary>
            private PageContext.Header header;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the start offset in bytes of the volume when assuming a normal header.
            /// </summary>
            public long NormalStart
            {
                get { return this.normalStart; }
                set { this.normalStart = value; }
            }

            /// <summary>
            /// Gets or sets the end offset in bytes of the volume when assuming a normal header.
            /// </summary>
            public long NormalEnd
            {
                get { return this.normalEnd; }
                set { this.normalEnd = value; }
            }

            /// <summary>
            /// Gets or sets the start offset in bytes of the volume when assuming a backup header.
            /// </summary>
            public long BackupStart
            {
                get { return this.backupStart; }
                set { this.backupStart = value; }
            }

            /// <summary>
            /// Gets or sets the end offset in bytes of the volume when assuming a backup header.
            /// </summary>
            public long BackupEnd
            {
                get { return this.backupEnd; }
                set { this.backupEnd = value; }
            }
            #endregion

            #region Constructor
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="header">The TrueCrypt header of the volume.</param>
            public HeaderInfo(PageContext.Header header)
            {
                this.header = header;
            }
            #endregion
        }
        #endregion

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
        private bool IsPossibleVolume(long start, long end)
        {
            return (start >= 0) && (end <= context.Drive.Size);
        }

        private string PrepareVolumeInfo(long start, long end)
        {
            if (IsPossibleVolume(start, end))
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

        private void Mount(HeaderInfo headerInfo, bool useBackupHeader)
        {
            long diskStartOffset = (useBackupHeader) ? headerInfo.BackupStart : headerInfo.NormalStart;
            long diskEndOffset = (useBackupHeader) ? headerInfo.BackupEnd : headerInfo.NormalEnd;
            long diskLength = diskEndOffset - diskStartOffset;
            string errMessage;
            TrueCrypt.Password pwd = new TrueCrypt.Password(context.Password);
            int dosDriveNo;
            if (TrueCrypt.Mount(pwd, useBackupHeader, out dosDriveNo, context.Drive.Volume, diskStartOffset, diskLength, out errMessage))
            {
                MessageBox.Show(this, string.Format("Successfully mounted the volume as drive \"{0}:\\\"", Char.ToString((char)('A' + dosDriveNo))), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, string.Format("Unable to mount the volume: {0}", errMessage), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                HeaderInfo headerInfo = new HeaderInfo(header);
                PhysicalDrive.CylinderHeadSector chsOffset = PhysicalDrive.LbaToChs(header.Lba, context.Drive.Geometry);

                ListViewItem item = new ListViewItem(string.Format("{0}/{1}/{2}", chsOffset.Cylinders, chsOffset.TracksPerCylinder, chsOffset.SectorsPerTrack));
                item.Tag = headerInfo;

                item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(header.CryptoInfo.VolumeSize));
                item.SubItems.Add(header.CryptoInfo.hiddenVolume.ToString());
                item.SubItems.Add(header.CryptoInfo.HeaderVersion.ToString());
                item.SubItems.Add((header.CryptoInfo.HeaderVersion >= 4).ToString());

                if (header.CryptoInfo.HeaderVersion >= 4)
                {
                    // assume a normal header

                    headerInfo.NormalStart = (header.Lba * context.Drive.Geometry.BytesPerSector);
                    if (header.CryptoInfo.hiddenVolume)
                    {
                        // in case of a hidden TrueCrypt volume the header has an offset of a complete TrueCrypt header
                        headerInfo.NormalStart -= TrueCrypt.TC_VOLUME_HEADER_SIZE;
                    }
                    headerInfo.NormalEnd = headerInfo.NormalStart + (long)(header.CryptoInfo.EncryptedAreaStart + header.CryptoInfo.EncryptedAreaLength + TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE);
                    if (header.CryptoInfo.hiddenVolume)
                    {
                        // the offset of a hidden volume does not include the TrueCrypt header group unlike the offset 
                        // of a normal volume
                        headerInfo.NormalEnd += TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE;
                    }

                    // assume an embedded backup header
                    headerInfo.BackupEnd = (header.Lba * context.Drive.Geometry.BytesPerSector);
                    // in case of a hidden TrueCrypt volume the header has an offset of a complete TrueCrypt header
                    headerInfo.BackupEnd += (header.CryptoInfo.hiddenVolume) ? TrueCrypt.TC_VOLUME_HEADER_SIZE : TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE;
                    headerInfo.BackupStart = headerInfo.BackupEnd - (long)(header.CryptoInfo.EncryptedAreaStart + header.CryptoInfo.EncryptedAreaLength + TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE);
                    if (header.CryptoInfo.hiddenVolume)
                    {
                        // the offset of a hidden volume does not include the TrueCrypt header group unlike the offset 
                        // of a normal volume
                        headerInfo.BackupStart -= TrueCrypt.TC_VOLUME_HEADER_GROUP_SIZE;
                    }

                    item.SubItems.Add(PrepareVolumeInfo(headerInfo.NormalStart, headerInfo.NormalEnd));
                    item.SubItems.Add(PrepareVolumeInfo(headerInfo.BackupStart, headerInfo.BackupEnd));
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
            if (MessageBox.Show(this, "If you continue the analyzer results will be cleared. Are you sure to continue?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }

            // skip the analyzer page when going back
            e.PageCount = 2;
        }

        private void lsvHeader_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvHeader.SelectedItems.Count == 0)
            {
                // no item selected
                mnuMountAsNormalHeader.Enabled = false;
                mnuMountAsBackupHeader.Enabled = false;
            }
            else
            {
                // enable the context menu items in case the selected header could be a normal or a backup header
                HeaderInfo headerInfo = (HeaderInfo)lsvHeader.SelectedItems[0].Tag;
                mnuMountAsNormalHeader.Enabled = IsPossibleVolume(headerInfo.NormalStart, headerInfo.NormalEnd);
                mnuMountAsBackupHeader.Enabled = IsPossibleVolume(headerInfo.BackupStart, headerInfo.BackupEnd);
            }
        }

        private void mnuMountAsNormalHeader_Click(object sender, EventArgs e)
        {
            Mount((HeaderInfo)lsvHeader.SelectedItems[0].Tag, false);
        }

        private void mnuMountAsBackupHeader_Click(object sender, EventArgs e)
        {
            Mount((HeaderInfo)lsvHeader.SelectedItems[0].Tag, true);
        }

        private void mnuDismountAll_Click(object sender, EventArgs e)
        {
            string errMessage;
            if (!TrueCrypt.DismountAll(out errMessage))
            {
                MessageBox.Show(this, string.Format("Unable to dismount all volumes: {0}", errMessage), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion
    }
}
