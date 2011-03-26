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

            /// <summary>
            /// Gets or sets the TrueCrypt header of the volume.
            /// </summary>
            public PageContext.Header Header
            {
                get { return this.header; }
                set { this.header = value; }
            }

            /// <summary>
            /// Gets whether this TrueCrypt header is supported by TestCrypt.
            /// </summary>
            public bool Supported
            {
                get { return this.header.CryptoInfo.HeaderVersion >= 3; }
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

            #region Methods
            /// <summary>
            /// Gets whether this could be the normal header of a TrueCrypt volume.
            /// </summary>
            /// <param name="size">The size of the disk in bytes.</param>
            /// <returns>True if this could be the normal header of a Truecrypt volume, otherwise false.</returns>
            public bool IsPossibleNormalHeader(long size)
            {
                return ((header.CryptoInfo.HeaderVersion >= 4) || (!header.CryptoInfo.hiddenVolume)) && 
                       ((normalStart >= 0) && (normalEnd <= size));
            }

            /// <summary>
            /// Gets whether this could be the embedded backup header of a TrueCrypt volume.
            /// </summary>
            /// <param name="size">The size of the disk in bytes.</param>
            /// <returns>True if this could be the embedded backup header of a Truecrypt volume, otherwise false.</returns>
            public bool IsPossibleBackupHeader(long size)
            {
                // the embedded backup header is only present in TrueCrypt header version 4 or newer
                return (header.CryptoInfo.HeaderVersion >= 4) && (backupStart >= 0) && (backupEnd <= size);
            }

            /// <summary>
            /// Gets a string representing the volume boundaries for a normal volume header.
            /// </summary>
            /// <param name="drive">The information about the drive containing the volume.</param>
            /// <returns>A string representing the volume boundaries for a normal volume header.</returns>
            public string ToNormalHeaderString(PhysicalDrive.DriveInfo drive)
            {
                if ((normalStart >= 0) && (normalEnd <= drive.Size))
                {
                    long startSector = normalStart / drive.Geometry.BytesPerSector;
                    long endSector = (normalEnd - 1) / drive.Geometry.BytesPerSector;
                    PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs(startSector, drive.Geometry);
                    PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs(endSector, drive.Geometry);
                    if ((header.CryptoInfo.HeaderVersion == 3) && (header.CryptoInfo.hiddenVolume))
                    {
                        return string.Format("?/?/? - {0}", chsEndOffset);
                    }
                    else
                    {
                        return string.Format("{0} - {1}", chsStartOffset, chsEndOffset);
                    }
                }
                else
                {
                    return "n/a";
                }
            }

            /// <summary>
            /// Gets a string representing the volume boundaries for an embedded backup volume header.
            /// </summary>
            /// <param name="drive">The information about the drive containing the volume.</param>
            /// <returns>A string representing the volume boundaries for an embedded backup volume header.</returns>
            public string ToBackupHeaderString(PhysicalDrive.DriveInfo drive)
            {
                if (IsPossibleBackupHeader(drive.Size))
                {
                    long startSector = backupStart / drive.Geometry.BytesPerSector;
                    long endSector = (backupEnd - 1) / drive.Geometry.BytesPerSector;
                    PhysicalDrive.CylinderHeadSector chsStartOffset = PhysicalDrive.LbaToChs(startSector, drive.Geometry);
                    PhysicalDrive.CylinderHeadSector chsEndOffset = PhysicalDrive.LbaToChs(endSector, drive.Geometry);
                    return string.Format("{0} - {1}", chsStartOffset, chsEndOffset);
                }
                else
                {
                    return "n/a";
                }
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
        /// <summary>
        /// Mounts a possible TrueCrypt volume by using the given header.
        /// </summary>
        /// <param name="headerInfo">The TrueCrypt header information which describes the TrueCrypt volume to be mounted.</param>
        /// <param name="useBackupHeader">True if the embedded backup header of the volume should be used, otherwise false.</param>
        private void Mount(HeaderInfo headerInfo, bool useBackupHeader)
        {
            long diskStartOffset = (useBackupHeader) ? headerInfo.BackupStart : headerInfo.NormalStart;
            long diskEndOffset = (useBackupHeader) ? headerInfo.BackupEnd : headerInfo.NormalEnd;
            long diskLength = diskEndOffset - diskStartOffset;
            string errMessage;
            TrueCrypt.Password pwd = context.TcPassword;
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
            bool legacyHiddenVolumeHeader = false;
            lsvHeader.Items.Clear();
            foreach (PageContext.Header header in context.HeaderList)
            {
                // A TrueCrypt header has been found: this could be either a normal header at the beginning of the 
                // volume or a backup header at the end of the volume. Moreover the header could be either the header 
                // of a normal or a hidden volume. The information whether a backup or a normal header has been found
                // is not stored within the header - therefore print both possibilities. The hidden volume information
                // can be read from the header and therefore this can be handled transparently. Currently only header
                // version 3 (TrueCrypt 5), 4 (TrueCrypt 6) and 5 (TrueCrypt 7) are supported - however the tool also
                // tries to work with newer versions
                HeaderInfo headerInfo = new HeaderInfo(header);
                PhysicalDrive.CylinderHeadSector chsOffset = PhysicalDrive.LbaToChs(header.Lba, context.Drive.Geometry);

                ListViewItem item = new ListViewItem(string.Format("{0}/{1}/{2}", chsOffset.Cylinders, chsOffset.TracksPerCylinder, chsOffset.SectorsPerTrack));
                item.Tag = headerInfo;

                item.SubItems.Add(PhysicalDrive.GetAsBestFitSizeUnit(header.CryptoInfo.VolumeSize));
                item.SubItems.Add(header.CryptoInfo.hiddenVolume.ToString());
                item.SubItems.Add(header.CryptoInfo.HeaderVersion.ToString());
                item.SubItems.Add(headerInfo.Supported.ToString());

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
                }
                else if (header.CryptoInfo.HeaderVersion == 3)
                {
                    // This version has to be handled separately due to the missing embedded backup header and completely
                    // different hidden volume handling. A normal volume header is placed at the beginning of the volume,
                    // a hidden volume header is placed at the end of the volume though. Due to the fact that the hidden 
                    // volume header does not know anything about the normal volume, the calculated partition start and
                    // end borders for a hidden volume header only include the hidden volume: you will neither be able to 
                    // mount the normal nor the hidden volume with the suggested settings. The normal volume cannot be 
                    // mounted due to the fact that it is not included in the partition and to mount the hidden volume 
                    // the correct absolut offset within the volume is required to encrypt the data. In order to get the
                    // real partition borders, the normal header password should be used. Unfortunately the normal volume
                    // header is much more likely to be destroyed for example by overwriting though
                    if (header.CryptoInfo.hiddenVolume)
                    {
                        // hidden volume
                        headerInfo.NormalEnd = (header.Lba * context.Drive.Geometry.BytesPerSector) + TrueCrypt.TC_HIDDEN_VOLUME_HEADER_OFFSET_LEGACY;
                        // the start offset of the partition is not known
                        legacyHiddenVolumeHeader = true;
                    }
                    else
                    {
                        // normal volume
                        headerInfo.NormalStart = (header.Lba * context.Drive.Geometry.BytesPerSector);
                        headerInfo.NormalEnd = headerInfo.NormalStart + (long)header.CryptoInfo.EncryptedAreaLength + TrueCrypt.TC_VOLUME_HEADER_SIZE_LEGACY;
                    }
                }
                else
                {
                    // only TrueCrypt header version 3 and newer are supported
                }

                item.SubItems.Add(headerInfo.ToNormalHeaderString(context.Drive));
                item.SubItems.Add(headerInfo.ToBackupHeaderString(context.Drive));
                lsvHeader.Items.Add(item);
            }
            lsvHeader.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            // check whether a legacy hidden volume header has been found and print further information about hidden legacy volumes
            if (legacyHiddenVolumeHeader)
            {
                MessageBox.Show(this, "A legacy hidden volume header (located at the end of the volume) has been found which does not " +
                                      "contain enough information to reconstruct the start offset of the volume. Using the information " +
                                      "from this header you can neither mount the normal nor the hidden volume." + 
                                      Environment.NewLine + Environment.NewLine +  
                                      "The normal volume header (located at the beginning of the volume) can be used to get the required " +
                                      "information to mount both the normal and the hidden legacy volume. The normal volume password has " +
                                      "to be used to search for this header. There is almost no chance to get access to the volume if " +
                                      "this header is corrupted or normal volume password has been lost.", 
                                      Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
                mnuMountAsNormalHeader.Enabled = headerInfo.Supported && headerInfo.IsPossibleNormalHeader(context.Drive.Size);
                mnuMountAsBackupHeader.Enabled = headerInfo.Supported && headerInfo.IsPossibleBackupHeader(context.Drive.Size);
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
