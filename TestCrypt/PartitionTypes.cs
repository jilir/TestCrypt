using System;
using System.Collections.Generic;
using System.Text;

namespace TestCrypt
{
    /// <summary>
    /// Class to map partition type numbers to a description of the partition type.
    /// </summary>
    class PartitionTypes
    {
        #region Attributes
        /// <summary>
        /// The hashtable which maps a partition type number to a description of the partition type.
        /// </summary>
        private static System.Collections.Hashtable partitionTypes;
        #endregion

        #region Constructors
        /// <summary>
        /// Static Constructor.
        /// </summary>
        static PartitionTypes()
        {
            partitionTypes = new System.Collections.Hashtable();
            partitionTypes.Add(0x00U, "Unused");
            partitionTypes.Add(0x01U, "FAT12");
            partitionTypes.Add(0x04U, "FAT16 <32M");
            partitionTypes.Add(0x05U, "Extended");
            partitionTypes.Add(0x06U, "FAT16 >32M");
            partitionTypes.Add(0x07U, "HPFS/NTFS");
            partitionTypes.Add(0x0AU, "OS/2 Boot Manager");
            partitionTypes.Add(0x0BU, "FAT32");
            partitionTypes.Add(0x0CU, "FAT32 LBA");
            partitionTypes.Add(0x0EU, "FAT16 LBA");
            partitionTypes.Add(0x0FU, "Extended LBA");
            partitionTypes.Add(0x11U, "hid. FAT12");
            partitionTypes.Add(0x12U, "Compaq Diagnostics");
            partitionTypes.Add(0x14U, "hid. FAT16 <32M");
            partitionTypes.Add(0x16U, "hid. FAT16 >32M");
            partitionTypes.Add(0x17U, "hid. HPFS/NTFS");
            partitionTypes.Add(0x1BU, "hid. FAT32");
            partitionTypes.Add(0x1CU, "hid. FAT32 LBA");
            partitionTypes.Add(0x1EU, "hid. FAT16 LBA");
            partitionTypes.Add(0x24U, "NEC MS-DOS 3.x");
            partitionTypes.Add(0x27U, "Windows RE(store)");
            partitionTypes.Add(0x3CU, "PMagic recovery");
            partitionTypes.Add(0x41U, "PPC PReP Boot");
            partitionTypes.Add(0x42U, "W2K Dynamic/SFS");
            partitionTypes.Add(0x55U, "EZ-Drive");
            partitionTypes.Add(0x64U, "NetWare 286");
            partitionTypes.Add(0x65U, "NetWare 3.11+");
            partitionTypes.Add(0x67U, "Novell");
            partitionTypes.Add(0x68U, "Novell");
            partitionTypes.Add(0x69U, "Novell");
            partitionTypes.Add(0x82U, "Linux Swap");
            partitionTypes.Add(0x83U, "Linux");
            partitionTypes.Add(0x8EU, "Linux LVM");
            partitionTypes.Add(0xA5U, "OpenBSD");
            partitionTypes.Add(0xA6U, "OpenBSD");
            partitionTypes.Add(0xA8U, "Darwin UFS");
            partitionTypes.Add(0xA9U, "NetBSD");
            partitionTypes.Add(0xABU, "Darwin boot");
            partitionTypes.Add(0xAFU, "HFS");
            partitionTypes.Add(0xBCU, "Acronis");
            partitionTypes.Add(0xBEU, "Solaris boot");
            partitionTypes.Add(0xBFU, "Solaris");
            partitionTypes.Add(0xDEU, "Dell Utility");
            partitionTypes.Add(0xE3U, "DOS RO");
            partitionTypes.Add(0xEEU, "EFI GPT");
            partitionTypes.Add(0xEFU, "EFI (FAT-12/16/32");
            partitionTypes.Add(0xF2U, "DOS secondary");
            partitionTypes.Add(0xFDU, "Linux RAID");
        }

        /// <summary>
        /// Private Constructor.
        /// </summary>
        private PartitionTypes()
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets the description of the partition type for the given partition type number.
        /// </summary>
        /// <param name="type">The partition type number.</param>
        /// <returns>The description of the partition type, null if no description is available.</returns>
        public static string GetPartitionType(byte type)
        {
            return (string)partitionTypes[(uint)type];
        }
        #endregion
    }
}
