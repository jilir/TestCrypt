using System;
using System.Collections.Generic;
using System.Text;

namespace TestCrypt
{
    public class PageContext
    {
        #region LocalTypes
        public enum AnalyzeType
        {
            Automatic,
            Manual,
            None
        };

        /// <summary>
        /// Class for the required analyzer parameters of a volume analyzer task.
        /// </summary>
        public class VolumeAnalyzer
        {
            #region Attributes
            public AnalyzeType AnalyzerType;
            public uint Sectors;
            #endregion

            #region Constructors
            public VolumeAnalyzer(AnalyzeType analyzerType, uint sectors)
            {
                this.AnalyzerType = analyzerType;
                this.Sectors = sectors;
            }
            #endregion
        }

        /// <summary>
        /// Class for the required analyzer parameters of a partition analyzer task.
        /// </summary>
        public class PartitionAnalyzer
        {
            #region Attributes
            /// <summary>
            /// The number of the partition (1-based).
            /// </summary>
            public uint PartitionNumber;
            public AnalyzeType AnalyzerType;
            public uint SectorsBefore;
            public uint SectorsAfter;
            #endregion

            #region Constructors
            public PartitionAnalyzer(uint partitionNumber, AnalyzeType analyzerType, uint sectorsBefore, uint sectorsAfter)
            {
                this.PartitionNumber = partitionNumber;
                this.AnalyzerType = analyzerType;
                this.SectorsBefore = sectorsBefore;
                this.SectorsAfter = sectorsAfter;
            }
            #endregion
        }

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
        private string password;
        private PhysicalDrive.DriveInfo drive;
       
        private List<PartitionAnalyzer> partitionBeginAnalyzer;
        private List<PartitionAnalyzer> partitionEndAnalyzer;

        private VolumeAnalyzer volumeBeginAnalyzer;
        private VolumeAnalyzer volumeEndAnalyzer;

        private List<Header> headerList;
        #endregion

        #region Properties
        public string Password
        {
            get { return this.password; }
            set { this.password = value; }
        }

        public PhysicalDrive.DriveInfo Drive
        {
            get { return this.drive; }
            set { this.drive = value; }
        }

        public List<PartitionAnalyzer> PartitionBeginAnalyzer
        {
            get { return this.partitionBeginAnalyzer; }
        }

        public List<PartitionAnalyzer> PartitionEndAnalyzer
        {
            get { return this.partitionEndAnalyzer; }
        }

        public VolumeAnalyzer VolumeBeginAnalyzer
        {
            get { return this.volumeBeginAnalyzer; }
            set { this.volumeBeginAnalyzer = value; }
        }

        public VolumeAnalyzer VolumeEndAnalyzer
        {
            get { return this.volumeEndAnalyzer; }
            set { this.volumeEndAnalyzer = value; }
        }

        public List<Header> HeaderList
        {
            get { return this.headerList; }
        }
        #endregion

        #region Constructors
        public PageContext()
        {
            this.partitionBeginAnalyzer = new List<PartitionAnalyzer>();
            this.partitionEndAnalyzer = new List<PartitionAnalyzer>();
            this.volumeBeginAnalyzer = new VolumeAnalyzer(AnalyzeType.None, 0);
            this.volumeEndAnalyzer = new VolumeAnalyzer(AnalyzeType.None, 0);
            this.headerList = new List<Header>();
        }
        #endregion

        #region Methods
        private void AddRange(List<ScanRange> rangeList, ScanRange range)
        {
            Int64 totalLba = drive.Size / drive.Geometry.BytesPerSector;
            Int64 lastLbaForScan = totalLba - (TrueCrypt.TC_VOLUME_HEADER_SIZE / drive.Geometry.BytesPerSector);

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
                    AddRange(scanRanges, new ScanRange(0, volumeBeginAnalyzer.Sectors));
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

            // sort the scan ranges in the list ascending by the start LBA
            scanRanges.Sort(new AscendingScanRangeComparer());

            return scanRanges;
        }

   
        #endregion
    }
}
