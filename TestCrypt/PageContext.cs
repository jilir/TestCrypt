using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;

namespace TestCrypt
{
    /// <summary>
    /// Enumerates all supported modes of TestCrypt.
    /// </summary>
    public enum Mode
    {
        SearchQuick,
        SearchDeep,
        SearchAdvanced,
        SearchFragment,
        MountInPlace,
        MountRescueDisk
    }

    /// <summary>
    /// Class for a device to be analyzed by TestCrypt.
    /// </summary>
    public class Device
    {
        #region Properties
        /// <summary>
        /// Gets or sets the drive of the device.
        /// </summary>
        public PhysicalDrive.DriveInfo Drive { get; set; }

        /// <summary>
        /// Gets or sets the partition of the drive.
        /// </summary>
        public Nullable<uint> Partition { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="drive">The drive of the device.</param>
        /// <param name="partition">The partition of the drive.</param>
        public Device(PhysicalDrive.DriveInfo drive, uint partition)
        {
            this.Drive = drive;
            this.Partition = partition;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="drive">The drive of the device.</param>
        public Device(PhysicalDrive.DriveInfo drive)
        {
            this.Drive = drive;
            this.Partition = null;
        }
        #endregion
    }

    /// <summary>
    /// Class containing the information shared between the different pages of the TestCrypt wizard.
    /// </summary>
    public class PageContext
    {
        #region LocalTypes
        public class ScanRange
        {
            #region Attributes
            /// <summary>
            /// The start address (LBA) of the range to scan.
            /// </summary>
            public Int64 StartLba;

            /// <summary>
            /// The end address (LBA) of the range to scan.
            /// </summary>
            public Int64 EndLba;
            #endregion

            #region Constructors
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="startOffset">The start address (LBA) of the range to scan.</param>
            /// <param name="endOffset">The end address (LBA) of the range to scan.</param>
            public ScanRange(Int64 startLba, Int64 endLba)
            {
                this.StartLba = startLba;
                this.EndLba = endLba;
            }
            #endregion
        }

        public class Header
        {
            #region Attributes
            public Int64 Lba;

            public TrueCrypt.CRYPTO_INFO CryptoInfo;
            #endregion 

            #region Constructors
            public Header(Int64 lba, TrueCrypt.CRYPTO_INFO cryptoInfo)
            {
                this.Lba = lba;
                this.CryptoInfo = cryptoInfo;
            }
            #endregion
        }
        #endregion

        #region Attribute
        private static PageContext context = new PageContext();
        private ResourceManager resourceManager = new ResourceManager("TestCrypt.LocalizedStrings", typeof(TestCrypt.Forms.FormMain).Assembly);
        private List<Header> headerList;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets The current mode of TestCrypt.
        /// </summary>
        public Mode Mode { get; set; }

        /// <summary>
        /// Gets or sets the TrueCrypt password.
        /// </summary>
        public TrueCrypt.Password Password { get; set; }

        /// <summary>
        /// Gets or sets the currently selected device.
        /// </summary>
        public Device Device { get; set; }
        
        public List<Header> HeaderList
        {
            get { return this.headerList; }
        }
        #endregion

        #region Constructors
        private PageContext()
        {
            this.headerList = new List<Header>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the instance of class (singleton).
        /// </summary>
        /// <returns>The instance of the class (singleton).</returns>
        public static PageContext GetInstance()
        {
            return context;
        }

        /// <summary>
        /// Returns the value of the string resource localized for the specified culture.
        /// </summary>
        /// <param name="name">The name of the resource to retrieve. </param>
        /// <returns>The value of the resource localized for the specified culture, or null if name cannot be found in a resource set.</returns>
        public string GetResourceString(string name)
        {
            return resourceManager.GetString(name).Replace(@"\n", Environment.NewLine);
        }

        /*private void AddRange(List<ScanRange> rangeList, ScanRange range)
        {
            Int64 totalLba = drive.Size / drive.Geometry.BytesPerSector;
            Int64 lastLbaForScan = totalLba - (TrueCrypt.TC_VOLUME_HEADER_SIZE_LEGACY / drive.Geometry.BytesPerSector);

            // limit the start LBA to a valid LBA
            if (range.StartLba > lastLbaForScan)
            {
                range.StartLba = lastLbaForScan;
            }
            else if (range.StartLba < 0)
            {
                range.StartLba = 0;
            }

            // limit the end LBA to a valid LBA
            if (range.EndLba > lastLbaForScan)
            {
                range.EndLba = lastLbaForScan;
            }
            else if (range.EndLba < 0)
            {
                range.EndLba = 0;
            }

            // try to optimize the number of ranges that have to be scanned by combining two ranges which overlap        
            for (int i = rangeList.Count - 1; i >= 0; i--)
            {
                // check whether the range that should be added lies completely before or after the current range of 
                // the list
                if ((range.EndLba < rangeList[i].StartLba) || (range.StartLba > rangeList[i].EndLba))
                {
                    // the range that should be added lies completely before or after the current range of the list: no
                    // optimization is possible
                }
                else
                {
                    // optimize the ranges by building one range which covers both ranges
                    if (rangeList[i].StartLba < range.StartLba)
                    {
                        range.StartLba = rangeList[i].StartLba;
                    }
                    if (rangeList[i].EndLba > range.EndLba)
                    {
                        range.EndLba = rangeList[i].EndLba;
                    }

                    // remove the current range of the list because it will be replaced by the new range
                    rangeList.RemoveAt(i);
                }
            }
          
            rangeList.Add(range);
        }

        class AscendingScanRangeComparer : IComparer<ScanRange>
        {
            public int Compare(ScanRange x, ScanRange y)
            {
                if (x.StartLba == y.StartLba)
                {
                    return 0;
                }
                else if (x.StartLba > y.StartLba)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            }
        }


        /// <summary>
        /// Gets a list of ranges to scan.
        /// 
        /// The list will be constructed using the configured partition, volume and custom analyzer settings. The list
        /// will be optimized to not scan the same range twice.
        /// </summary>
        /// <returns>THe list of ranges to scan.</returns>
        public List<ScanRange> GetOptimizedScanRanges()
        {
            List<ScanRange> scanRanges = new List<ScanRange>();

            Int64 driveSectorCount = drive.Size / drive.Geometry.BytesPerSector;

            switch (volumeBeginAnalyzer.AnalyzerType)
            {
                case AnalyzeType.Automatic:
                    // scan the first 2 MiB of the volume
                    AddRange(scanRanges, new ScanRange(0, 4096));
                    break;
                case AnalyzeType.Manual:
                    System.Diagnostics.Debug.Assert(volumeBeginAnalyzer.Sectors > 0);
                    AddRange(scanRanges, new ScanRange(0, volumeBeginAnalyzer.Sectors - 1));
                    break;
            }

            switch (volumeEndAnalyzer.AnalyzerType)
            {
                case AnalyzeType.Automatic:
                    // scan the last 10 MiB of the volume
                    AddRange(scanRanges, new ScanRange(driveSectorCount - 20480, driveSectorCount));
                    break;
                case AnalyzeType.Manual:
                    AddRange(scanRanges, new ScanRange(driveSectorCount - volumeEndAnalyzer.Sectors, driveSectorCount));
                    break;
            }

            foreach (PartitionAnalyzer analyzer in partitionBeginAnalyzer)
            {
                for (int i = 0; i < drive.Partitions.Count; i++)
                {
                    if (drive.Partitions[i].PartitionNumber == analyzer.PartitionNumber)
                    {
                        Int64 offset = drive.Partitions[i].StartingOffset / drive.Geometry.BytesPerSector;
                        switch (analyzer.AnalyzerType)
                        {
                            case AnalyzeType.Automatic:
                                // scan 2 MiB before and 2 MiB after the begin of the partition
                                AddRange(scanRanges, new ScanRange(offset - 4096, offset + 4096));
                                break;
                            case AnalyzeType.Manual:
                                AddRange(scanRanges, new ScanRange(offset - analyzer.SectorsBefore, offset + analyzer.SectorsAfter));
                                break;
                        }
                        break;
                    }
                }
                
            }

            foreach (PartitionAnalyzer analyzer in partitionEndAnalyzer)
            {
                for (int i = 0; i < drive.Partitions.Count; i++)
                {
                    if (drive.Partitions[i].PartitionNumber == analyzer.PartitionNumber)
                    {
                        Int64 offset = (drive.Partitions[i].StartingOffset + drive.Partitions[i].PartitionLength) / drive.Geometry.BytesPerSector;
                        switch (analyzer.AnalyzerType)
                        {
                            case AnalyzeType.Automatic:
                                // scan 2 MiB before and 2 MiB after the end of the partition
                                AddRange(scanRanges, new ScanRange(offset - 4096, offset + 4096));
                                break;
                            case AnalyzeType.Manual:
                                AddRange(scanRanges, new ScanRange(offset - analyzer.SectorsBefore, offset + analyzer.SectorsAfter));
                                break;
                        }
                    }
                }
            }

            foreach (ScanRange range in customAnalyzer)
            {
                AddRange(scanRanges, range);
            }

            // sort the scan ranges in the list ascending by the start LBA
            scanRanges.Sort(new AscendingScanRangeComparer());

            return scanRanges;
        }*/
        #endregion
    }
}
