using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using System.Text;

namespace TestCrypt
{
    public class PhysicalDrive
    {
        #region Constants
        private const uint MAX_NUMBER_OF_DRIVES = 64;

        /// <summary>
        /// Retrieves extended information about the physical disk's geometry: type, number of cylinders, tracks per
        /// cylinder, sectors per track, and bytes per sector.
        /// </summary>
        private const uint IOCTL_DISK_GET_DRIVE_GEOMETRY_EX = 0x000700A0U;

        /// <summary>
        /// Retrieves information for each entry in the partition tables for a disk.
        /// </summary>
        private const uint IOCTL_DISK_GET_DRIVE_LAYOUT = 0x0007400CU;
        #endregion

        #region Properties
        /// <summary>
        /// Gets information about all physical drives detected on this machine.
        /// </summary>
        public static IEnumerable<DriveInfo> Drives 
        {
            get 
            {
                // simply try to open all physical drives up to MAX_NUMBER_OF_DRIVES
                List<DriveInfo> driveInfoList = new List<DriveInfo>();
                for (uint i = 0; i < MAX_NUMBER_OF_DRIVES; i++)
                {
                    // try to open the current physical drive
                    string volume = string.Format("\\\\.\\PhysicalDrive{0}", i);
                    SafeFileHandle hndl = DeviceApi.CreateFile(volume,
                                                               DeviceApi.GENERIC_READ,
                                                               DeviceApi.FILE_SHARE_READ | DeviceApi.FILE_SHARE_WRITE, 
                                                               IntPtr.Zero,
                                                               DeviceApi.OPEN_EXISTING,
                                                               DeviceApi.FILE_ATTRIBUTE_READONLY, 
                                                               IntPtr.Zero);
                    if (!hndl.IsInvalid)
                    {
                        // try to use the I/O-control IOCTL_DISK_GET_DRIVE_GEOMETRY_EX for the current physical drive
                        uint dummy = 0;
                        IntPtr dgePtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DISK_GEOMETRY_EX)));
                        if (DeviceApi.DeviceIoControl(hndl, IOCTL_DISK_GET_DRIVE_GEOMETRY_EX, IntPtr.Zero, 0, dgePtr, (uint)Marshal.SizeOf(typeof(DISK_GEOMETRY_EX)), ref dummy, IntPtr.Zero))
                        {
                            // I/O-control has been invoked successfully, convert to DISK_GEOMETRY_EX structure and 
                            // create the DriveInfo
                            DISK_GEOMETRY_EX dge = (DISK_GEOMETRY_EX)Marshal.PtrToStructure(dgePtr, typeof(DISK_GEOMETRY_EX));
                            DriveInfo driveInfo = new DriveInfo(i, dge.DiskSize, dge.Geometry);

                            // try to use the I/O-control IOCTL_DISK_GET_DRIVE_LAYOUT to get information about the 
                            // partitions of the current physical drive: use 1K buffer as the I/O-control returns 
                            // variable size data and there is no possibility to get the required buffer size
                            const int DRIVE_LAYOUT_BUFFER_SIZE = 1024;
                            IntPtr driveLayoutPtr = Marshal.AllocHGlobal(DRIVE_LAYOUT_BUFFER_SIZE);
                            if (DeviceApi.DeviceIoControl(hndl, IOCTL_DISK_GET_DRIVE_LAYOUT, IntPtr.Zero, 0, driveLayoutPtr, DRIVE_LAYOUT_BUFFER_SIZE, ref dummy, IntPtr.Zero))
                            {
                                // I/O-control has been invoked successfully, convert to DRIVE_LAYOUT_INFORMATION_EX
                                DRIVE_LAYOUT_INFORMATION driveLayout = (DRIVE_LAYOUT_INFORMATION)Marshal.PtrToStructure(driveLayoutPtr, typeof(DRIVE_LAYOUT_INFORMATION));
                                for (uint p = 0; p < driveLayout.PartitionCount; p++)
                                {
                                    // now there comes some pointer arithmetic part because I have not found a better
                                    // way to handle variable sized structures in C#
                                    Int64 ptr = driveLayoutPtr.ToInt64() + Marshal.SizeOf(typeof(DRIVE_LAYOUT_INFORMATION)) + (p * Marshal.SizeOf(typeof(PARTITION_INFORMATION)));

                                    PARTITION_INFORMATION partInfo = (PARTITION_INFORMATION)Marshal.PtrToStructure(new IntPtr(ptr), typeof(PARTITION_INFORMATION));
                                    if (partInfo.PartitionType != PARTITION_INFORMATION.PARTITION_ENTRY_UNUSED)
                                    {
                                        driveInfo.Partitions.Add(partInfo);
                                    }
                                }
                            }
                            Marshal.FreeHGlobal(driveLayoutPtr);
                            
                            // add the physical drive information to the list
                            driveInfoList.Add(driveInfo);
                        }
                        Marshal.FreeHGlobal(dgePtr);
                        hndl.Close();
                    }
                }
                return driveInfoList; 
            }
        }
        #endregion

        #region Local Types
        public enum EMoveMethod : uint
        {
            Begin = 0,
            Current = 1,
            End = 2
        }

        /// <summary>
        /// Represents the various forms of device media.
        /// </summary>
        public enum MEDIA_TYPE
        {
            Unknown = 0x00,
            F5_1Pt2_512 = 0x01,
            F3_1Pt44_512 = 0x02,
            F3_2Pt88_512 = 0x03,
            F3_20Pt8_512 = 0x04,
            F3_720_512 = 0x05,
            F5_360_512 = 0x06,
            F5_320_512 = 0x07,
            F5_320_1024 = 0x08,
            F5_180_512 = 0x09,
            F5_160_512 = 0x0a,
            RemovableMedia = 0x0b,
            FixedMedia = 0x0c,
            F3_120M_512 = 0x0d,
            F3_640_512 = 0x0e,
            F5_640_512 = 0x0f,
            F5_720_512 = 0x10,
            F3_1Pt2_512 = 0x11,
            F3_1Pt23_1024 = 0x12,
            F5_1Pt23_1024 = 0x13,
            F3_128Mb_512 = 0x14,
            F3_230Mb_512 = 0x15,
            F8_256_128 = 0x16,
            F3_200Mb_512 = 0x17,
            F3_240M_512 = 0x18,
            F3_32M_512 = 0x19
        }

        /// <summary>
        /// Describes the geometry of disk devices and media.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DISK_GEOMETRY
        {
            /// <summary>
            /// The number of cylinders.
            /// </summary>
            public Int64 Cylinders;

            /// <summary>
            /// The type of media. For a list of values, see MEDIA_TYPE.
            /// </summary>
            public MEDIA_TYPE MediaType;

            /// <summary>
            /// The number of tracks per cylinder.
            /// </summary>
            public uint TracksPerCylinder;

            /// <summary>
            /// The number of sectors per track.
            /// </summary>
            public uint SectorsPerTrack;

            /// <summary>
            /// The number of bytes per sector.
            /// </summary>
            public uint BytesPerSector;
        }

        /// <summary>
        /// Describes the extended geometry of disk devices and media.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct DISK_GEOMETRY_EX
        {
            /// <summary>
            /// A DISK_GEOMETRY structure.
            /// </summary>
            public DISK_GEOMETRY Geometry;

            /// <summary>
            /// The disk size, in bytes.
            /// </summary>
            public Int64 DiskSize;

            /// <summary>
            /// Any additional data.
            /// </summary>
            public Byte Data;
        }

        /// <summary>
        /// Contains information about the partitions of a drive.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct DRIVE_LAYOUT_INFORMATION
        {
            /// <summary>
            /// The number of partitions on a drive.
            /// 
            /// On disks with the MBR layout, this value is always a multiple of 4. Any partitions that are unused have a partition type of PARTITION_ENTRY_UNUSED.
            /// </summary>
            public uint PartitionCount;

            /// <summary>
            /// The drive signature value. 
            /// </summary>
            public uint Signature;
        }

        /// <summary>
        /// Contains information about a disk partition.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct PARTITION_INFORMATION
        {
            #region Constants
            /// <summary>
            /// An unused entry partition.
            /// </summary>
            public const byte PARTITION_ENTRY_UNUSED = 0x00;

            /// <summary>
            /// A FAT12 file system partition.
            /// </summary>
            public const byte PARTITION_FAT_12 = 0x01;

            /// <summary>
            /// A FAT16 file system partition.
            /// </summary>
            public const byte PARTITION_FAT_16 = 0x04;

            /// <summary>
            /// An extended partition.
            /// </summary>
            public const byte PARTITION_EXTENDED = 0x05;

            /// <summary>
            /// An IFS partition.
            /// </summary>
            public const byte PARTITION_IFS = 0x07;

            /// <summary>
            /// A FAT32 file system partition.
            /// </summary>
            public const byte PARTITION_FAT32 = 0x0B;

            /// <summary>
            /// A logical disk manager (LDM) partition.
            /// </summary>
            public const byte PARTITION_LDM = 0x42;

            /// <summary>
            /// An NTFT partition.
            /// </summary>
            public const byte PARTITION_NTFT = 0x80;

            /// <summary>
            /// A valid NTFT partition.
            /// 
            /// The high bit of a partition type code indicates that a partition is part of an NTFT mirror or striped array.
            /// </summary>
            public const byte PARTITION_VALID_NTFT = 0xC0;
            #endregion

            /// <summary>
            /// The starting offset of the partition.
            /// </summary>
            public Int64 StartingOffset;

            /// <summary>
            /// The length of the partition, in bytes.
            /// </summary>
            public Int64 PartitionLength;

            /// <summary>
            /// The number of hidden sectors in the partition.
            /// </summary>
            public uint HiddenSectors;

            /// <summary>
            /// The number of the partition (1-based).
            /// </summary>
            public uint PartitionNumber;

            /// <summary>
            /// The type of partition. For a list of values, see Disk Partition Types.
            /// </summary>
            [MarshalAs(UnmanagedType.U1)]
            public byte PartitionType;

            /// <summary>
            /// If this member is TRUE, the partition is bootable.
            /// </summary>
            [MarshalAs(UnmanagedType.I1)]
            public bool BootIndicator;

            /// <summary>
            /// If this member is TRUE, the partition is of a recognized type.
            /// </summary>
            [MarshalAs(UnmanagedType.I1)]
            public bool RecognizedPartition;

            /// <summary>
            /// If this member is TRUE, the partition information has changed. When you change a partition (with 
            /// IOCTL_DISK_SET_DRIVE_LAYOUT), the system uses this member to determine which partitions have changed
            /// and need their information rewritten.
            /// </summary>
            [MarshalAs(UnmanagedType.I1)]
            public bool RewritePartition;
        }

        /// <summary>
        /// Describes a Cylinder, Head, Sector (C/H/S) address.
        /// </summary>
        public struct CylinderHeadSector
        {
            /// <summary>
            /// The number of cylinders.
            /// </summary>
            public Int64 Cylinders;

            /// <summary>
            /// The number of tracks per cylinder.
            /// </summary>
            public uint TracksPerCylinder;

            /// <summary>
            /// The number of sectors per track.
            /// </summary>
            public uint SectorsPerTrack;
        }

        public class DriveInfo
        {
            #region Attributes
            /// <summary>
            /// The volume identifier of the physical drive.
            /// </summary>
            private uint volume;

            /// <summary>
            /// The disk size, in bytes.
            /// </summary>
            private Int64 size;

            /// <summary>
            /// The disk geometry of the physical drive.
            /// </summary>
            private DISK_GEOMETRY geometry;

            /// <summary>
            /// The list of partitions of the physical drive.
            /// </summary>
            private List<PARTITION_INFORMATION> partitions;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the volume identifier of the physical drive.
            /// </summary>
            public uint Volume
            {
                get { return this.volume; }
                set { this.volume = value; }
            }

            /// <summary>
            /// Gets or sets the disk size, in bytes.
            /// </summary>
            public Int64 Size
            {
                get { return this.size; }
                set { this.size = value; }
            }

            /// <summary>
            /// Gets or sets the disk geometry of the physical drive.
            /// </summary>
            public DISK_GEOMETRY Geometry
            {
                get { return this.geometry; }
                set { this.geometry = value; }
            }

            /// <summary>
            /// The list of partitions of the physical drive.
            /// </summary>
            public List<PARTITION_INFORMATION> Partitions
            {
                get { return this.partitions; }
                set { this.partitions = value; }
            }
            #endregion

            #region Constructors
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="volume">The volume identifier of the physical drive.</param>
            /// <param name="size">The disk size, in bytes.</param>
            /// <param name="geometry">The disk geometry of the physical drive.</param>
            public DriveInfo(uint volume, Int64 size, DISK_GEOMETRY geometry)
            {
                this.volume = volume;
                this.size = size;
                this.geometry = geometry;
                this.partitions = new List<PARTITION_INFORMATION>();
            }
            #endregion
        }
        #endregion

        #region Methods
        public static void Read(uint volume, Int64 address, uint length, byte[] data)
        {
            // try to open the current physical drive
            SafeFileHandle hndl = DeviceApi.CreateFile(string.Format("\\\\.\\PhysicalDrive{0}", volume),
                                                       DeviceApi.GENERIC_READ,
                                                       DeviceApi.FILE_SHARE_READ | DeviceApi.FILE_SHARE_WRITE, 
                                                       IntPtr.Zero,
                                                       DeviceApi.OPEN_EXISTING,
                                                       DeviceApi.FILE_ATTRIBUTE_READONLY, 
                                                       IntPtr.Zero);
            if (!hndl.IsInvalid)
            {
                // set the file pointer to the requested address
                if (DeviceApi.SetFilePointerEx(hndl, address, IntPtr.Zero, DeviceApi.EMoveMethod.Begin))
                {
                    // read the requested data from the physical drive
                    uint dummy;
                    if (!DeviceApi.ReadFile(hndl, data, length, out dummy, IntPtr.Zero))
                    {
                        throw new System.IO.IOException("\"ReadFile\" API call failed");
                    }
                }
                else
                {
                    throw new System.IO.IOException("\"SetFilePointerEx\" API call failed");
                }
                hndl.Close();
            }
        }

        /// <summary>
        /// Returns a string containing the best-fitting size and unit of the given size in bytes.
        /// </summary>
        /// <param name="size">Size in bytes that should be converted to the best-fitting unit.</param>
        /// <returns>The string containing the best-fitting size and unit of the given size in bytes.</returns>
        public static string GetAsBestFitSizeUnit(double size)
        {
            string[] units = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB" };

            double nextSize = size / 1000.0;
            int i = 0;
            while (nextSize >= 1.0)
            {
                i++;
                size = nextSize;
                nextSize = size / 1000.0;
            }

            return string.Format("{0:0.00} {1}", size, units[i]);
        }

        public static CylinderHeadSector LbaToChs(Int64 lba, DISK_GEOMETRY geometry)
        {
            CylinderHeadSector chs;
            chs.Cylinders = (lba / (geometry.TracksPerCylinder * geometry.SectorsPerTrack));
            Int64 tmp = lba % (geometry.TracksPerCylinder * geometry.SectorsPerTrack);
            chs.TracksPerCylinder = (uint)(tmp / geometry.SectorsPerTrack);
            chs.SectorsPerTrack = (uint)((tmp % geometry.SectorsPerTrack) + 1);
            return chs;
        }

        public static Int64 ChsToLba(CylinderHeadSector chs, DISK_GEOMETRY geometry)
        {
            return (chs.Cylinders * geometry.TracksPerCylinder * geometry.SectorsPerTrack) +
                   (chs.TracksPerCylinder * geometry.SectorsPerTrack) + 
                   (chs.SectorsPerTrack - 1);
        }
        #endregion
    }
}
