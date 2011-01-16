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
            if (range.StartLba > lastLbaForScan)
            {
                range.StartLba = lastLbaForScan;
            }
            if (range.EndLba > lastLbaForScan)
            {
                range.EndLba = lastLbaForScan;
            }
            rangeList.Add(range);
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
                    if (driveSectorCount > 4096)
                    {
                        AddRange(scanRanges, new ScanRange(0, 4096));
                    }
                    else
                    {
                        AddRange(scanRanges, new ScanRange(0, driveSectorCount));
                    }
                    break;
                case AnalyzeType.Manual:
                    AddRange(scanRanges, new ScanRange(0, volumeBeginAnalyzer.Sectors));
                    break;
            }

            switch (volumeEndAnalyzer.AnalyzerType)
            {
                case AnalyzeType.Automatic:
                    // scan the last 10 MiB of the volume
                    if (driveSectorCount > 20480)
                    {
                        AddRange(scanRanges, new ScanRange(driveSectorCount - 20480, driveSectorCount));
                    }
                    else
                    {
                        AddRange(scanRanges, new ScanRange(0, driveSectorCount));
                    }
                    break;
                case AnalyzeType.Manual:
                    AddRange(scanRanges, new ScanRange(driveSectorCount - volumeEndAnalyzer.Sectors, driveSectorCount));
                    break;
            }

            return scanRanges;
        }

   
        #endregion
    }
}
