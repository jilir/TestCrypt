using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace TestCrypt
{
    public class TrueCrypt
    {
        #region Constants
        /// <summary>
        /// Maximum possible password length.
        /// </summary>
        private const int MAX_PASSWORD = 64;

        private const int MAX_EXPANDED_KEY = 0x14CC;

        private const int MASTER_KEYDATA_SIZE = 0x100;

        private const int PKCS5_SALT_SIZE = 0x40;

        /// <summary>
        /// The volume header size;
        /// </summary>
        public const int TC_VOLUME_HEADER_SIZE = 2 * 64 * 1024;
        #endregion

        #region P/Invoke
        [DllImport("TrueCrypt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadVolumeHeader(bool bBoot, byte[] encryptedHeader, ref Password password, IntPtr retInfo, ref CRYPTO_INFO retHeaderCryptoInfo);

        [DllImport("TrueCrypt.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool EncryptionThreadPoolStart(int encryptionFreeCpuCount);
        #endregion

        #region LocalTypes
        [StructLayout(LayoutKind.Sequential)]
        public struct Password
        {
            #region Attributes
            /// <summary>
            /// The length of the password.
            /// </summary>
            public uint Length;

            /// <summary>
            /// The password in ASCII encoding.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.U1, SizeConst=MAX_PASSWORD + 1)] 
            public byte[] Text;

            /// <summary>
            /// Keep 64-bit alignment.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 3)]
            byte[] Pad;
            #endregion

            #region Constructors
            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="password">The password as string.</param>
            public Password(String password)
            {
                this.Length = (uint)password.Length;
                this.Text = new byte[MAX_PASSWORD + 1];
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                Array.Copy(enc.GetBytes(password), this.Text, this.Length);
                this.Pad = new byte[3];
            }
            #endregion
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CRYPTO_INFO
        {
            /// <summary>
            /// Encryption algorithm ID.
            /// </summary>
            public int ea;	
			
            /// <summary>
            /// Mode of operation (e.g., XTS).
            /// </summary>
		    public int mode;
			
            /// <summary>
            /// Primary key schedule (if it is a cascade, it conatins multiple concatenated keys)
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = MAX_EXPANDED_KEY)]
            public byte[] ks;

            /// <summary>
            /// Secondary key schedule (if cascade, multiple concatenated) for XTS mode.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = MAX_EXPANDED_KEY)]
            public byte[] ks2;

            /// <summary>
            /// Indicates whether the volume is mounted/mountable as hidden volume.
            /// </summary>
            public bool hiddenVolume;

            public UInt16 HeaderVersion;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = 0x1800)]
            public byte[] gf_ctx; 

            /// <summary>
            /// This holds the volume header area containing concatenated master key(s) and secondary key(s) (XTS mode). For LRW (deprecated/legacy), it contains the tweak key before the master key(s). For CBC (deprecated/legacy), it contains the IV seed before the master key(s).
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = MASTER_KEYDATA_SIZE)]
            public byte[] master_keydata;

            /// <summary>
            /// For XTS, this contains the secondary key (if cascade, multiple concatenated). For LRW (deprecated/legacy), it contains the tweak key. For CBC (deprecated/legacy), it contains the IV seed.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = MASTER_KEYDATA_SIZE)]
            public byte[] k2;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.U1, SizeConst = PKCS5_SALT_SIZE)]
            public byte[] salt;

            public int noIterations;
            public int pkcs5;

            /// <summary>
            /// Legacy.
            /// </summary>
            public UInt64 volume_creation_time;

            /// <summary>
            /// Legacy.
            /// </summary>
            public UInt64 header_creation_time;

            /// <summary>
            /// Indicates whether the volume contains a hidden volume to be protected against overwriting.
            /// </summary>
            public bool bProtectHiddenVolume;

            /// <summary>
            /// TRUE if a write operation has been denied by the driver in order to prevent the hidden volume from being overwritten (set to FALSE upon volume mount).
            /// </summary>
            public bool bHiddenVolProtectionAction;

            /// <summary>
            /// Absolute position, in bytes, of the first data sector of the volume.
            /// </summary>
            public UInt64 volDataAreaOffset;

            /// <summary>
            /// Size of the hidden volume excluding the header (in bytes). Set to 0 for standard volumes.
            /// </summary>
            public UInt64 hiddenVolumeSize;

            /// <summary>
            /// Absolute position, in bytes, of the first hidden volume data sector within the host volume (provided that there is a hidden volume within). This must be set for all hidden volumes; in case of a normal volume, this variable is only used when protecting a hidden volume within it. 
            /// </summary>
	        public UInt64 hiddenVolumeOffset;

            public UInt64 hiddenVolumeProtectedSize;

            /// <summary>
            /// If TRUE, the volume is a partition located on an encrypted system drive and mounted without pre-boot authentication.
            /// </summary>
            public bool bPartitionInInactiveSysEncScope;

            /// <summary>
            /// First data unit number of the volume. This is 0 for file-hosted and non-system partition-hosted volumes. For partitions within key scope of system encryption this reflects real physical offset within the device (this is used e.g. when such a partition is mounted as a regular volume without pre-boot authentication).
            /// </summary>
            public UInt64 FirstDataUnitNo;
	
            public UInt16 RequiredProgramVersion;

            public bool LegacyVolume;

            public uint SectorSize;

            public UInt64 VolumeSize;
            public UInt64 EncryptedAreaStart;
            public UInt64 EncryptedAreaLength;
            public uint HeaderFlags;
        }
        #endregion
    }
}
