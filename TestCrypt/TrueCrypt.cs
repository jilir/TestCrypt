using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;

namespace TestCrypt
{
    public class TrueCrypt
    {
        #region Constants
        /// <summary>
        /// Maximum possible password length.
        /// </summary>
        private const int MAX_PASSWORD = 64;

        private const int TC_MAX_PATH = 260;

        private const int MAX_EXPANDED_KEY = 0x14CC;

        private const int MASTER_KEYDATA_SIZE = 0x100;

        private const int PKCS5_SALT_SIZE = 0x40;

        private const string WIN32_ROOT_PREFIX = "\\\\.\\TestCrypt";

        private const uint TC_IOCTL_MOUNT_VOLUME = 0x0022200CU;

        private const uint TC_IOCTL_DISMOUNT_ALL_VOLUMES = 0x00222014U;

        private const uint TC_IOCTL_GET_MOUNTED_VOLUMES = 0x00222018U;
        
        /// <summary>
        /// Includes STANDARD_RIGHTS_REQUIRED, in addition to all access rights in this table.
        /// </summary>
        private const uint SC_MANAGER_ALL_ACCESS = 0xF003FU;

        /// <summary>
        /// Includes STANDARD_RIGHTS_REQUIRED in addition to all access rights in this table.
        /// </summary>
        private const uint SERVICE_ALL_ACCESS = 0xF01FFU;

        /// <summary>
        /// Driver service.
        /// </summary>
        private const uint SERVICE_KERNEL_DRIVER = 0x00000001U;

        /// <summary>
        /// A service started by the service control manager when a process calls the StartService function. For more
        /// information, see Starting Services on Demand.
        /// </summary>
        private const uint SERVICE_DEMAND_START = 0x00000003U;

        /// <summary>
        /// The startup program logs the error in the event log but continues the startup operation.
        /// </summary>
        private const uint SERVICE_ERROR_NORMAL = 0x00000001U;


        /// <summary>
        /// System detected a new device.
        /// </summary>
        private const uint DBT_DEVICEARRIVAL = 0x8000U;

        /// <summary>
        /// Wants to remove, may fail.
        /// </summary>
        private const uint DBT_DEVICEQUERYREMOVE = 0x8001U;
        
        /// <summary>
        /// Removal aborted.
        /// </summary>
        private const uint DBT_DEVICEQUERYREMOVEFAILED = 0x8002U;
        
        /// <summary>
        /// About to remove, still avail.
        /// </summary>
        private const uint DBT_DEVICEREMOVEPENDING = 0x8003U;
        
        /// <summary>
        /// Device is gone
        /// </summary>
        private const uint DBT_DEVICEREMOVECOMPLETE = 0x8004U;

        /// <summary>
        /// Type specific event.
        /// </summary>
        private const uint DBT_DEVICETYPESPECIFIC = 0x8005U;


        /// <summary>
        /// OEM-defined device type.
        /// </summary>
        private const uint DBT_DEVTYP_OEM = 0x00000000U;

        /// <summary>
        /// Devnode number.
        /// </summary>
        private const uint DBT_DEVTYP_DEVNODE = 0x00000001U;
        
        /// <summary>
        /// Logical volume.
        /// </summary>
        private const uint DBT_DEVTYP_VOLUME = 0x00000002U;

        /// <summary>
        /// Serial, parallel.
        /// </summary>
        private const uint DBT_DEVTYP_PORT = 0x00000003U; 
       
        /// <summary>
        /// Network resource.
        /// </summary>
        private const uint DBT_DEVTYP_NET = 0x00000004U; 


        private const uint SHCNE_RENAMEITEM = 0x00000001U;
        private const uint SHCNE_CREATE = 0x00000002U;
        private const uint SHCNE_DELETE = 0x00000004U;
        private const uint SHCNE_MKDIR = 0x00000008U;
        private const uint SHCNE_RMDIR = 0x00000010U;
        private const uint SHCNE_MEDIAINSERTED = 0x00000020U;
        private const uint SHCNE_MEDIAREMOVED = 0x00000040U;
        private const uint SHCNE_DRIVEREMOVED = 0x00000080U;
        private const uint SHCNE_DRIVEADD = 0x00000100U;
        private const uint SHCNE_NETSHARE = 0x00000200U;
        private const uint SHCNE_NETUNSHARE = 0x00000400U;
        private const uint SHCNE_ATTRIBUTES = 0x00000800U;
        private const uint SHCNE_UPDATEDIR = 0x00001000U;
        private const uint SHCNE_UPDATEITEM = 0x00002000U;
        private const uint SHCNE_SERVERDISCONNECT = 0x00004000U;
        private const uint SHCNE_UPDATEIMAGE = 0x00008000U;
        private const uint SHCNE_DRIVEADDGUI = 0x00010000U;
        private const uint SHCNE_RENAMEFOLDER = 0x00020000U;
        private const uint SHCNE_FREESPACE = 0x00040000U;


        // Flags
        // uFlags & SHCNF_TYPE is an ID which indicates what dwItem1 and dwItem2 mean
        private const uint SHCNF_IDLIST = 0x0000U;      // LPITEMIDLIST
        private const uint SHCNF_PATHA = 0x0001U;       // path name
        private const uint SHCNF_PRINTERA = 0x0002U;    // printer friendly name
        private const uint SHCNF_DWORD = 0x0003U;       // DWORD
        private const uint SHCNF_PATHW = 0x0005U;       // path name
        private const uint SHCNF_PRINTERW = 0x0006U;    // printer friendly name
        private const uint SHCNF_TYPE = 0x00FFU;
        private const uint SHCNF_FLUSH = 0x1000U;
        private const uint SHCNF_FLUSHNOWAIT = 0x3000U; // includes SHCNF_FLUSH


        private const uint SMTO_ABORTIFHUNG = 2;
        private const uint WM_DEVICECHANGE = 0x0219U;
        private const uint HWND_BROADCAST = 0xFFFFU;

        /// <summary>
        /// The volume header size;
        /// </summary>
        public const int TC_VOLUME_HEADER_SIZE = 64 * 1024;

        /// <summary>
        /// The volume header group size (including normal and hidden volume header);
        /// </summary>
        public const int TC_VOLUME_HEADER_GROUP_SIZE = 2 * TC_VOLUME_HEADER_SIZE;

        public const int TC_VOLUME_HEADER_SIZE_LEGACY = 512;

        public const int TC_SECTOR_SIZE_LEGACY = 512;

        /// <summary>
        /// The offset, in bytes, of the legacy hidden volume header position from the end of the file (a positive value).
        /// </summary>
        public const int TC_HIDDEN_VOLUME_HEADER_OFFSET_LEGACY = (TC_VOLUME_HEADER_SIZE_LEGACY + TC_SECTOR_SIZE_LEGACY * 2);
        #endregion

        #region P/Invoke
        [DllImport("TrueCrypt.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int ReadVolumeHeader(bool bBoot, byte[] encryptedHeader, ref Password password, IntPtr retInfo, ref CRYPTO_INFO retHeaderCryptoInfo);

        [DllImport("TrueCrypt.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern bool EncryptionThreadPoolStart(int encryptionFreeCpuCount);

        [DllImport("TrueCrypt.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr KeyFileAdd(IntPtr firstKeyFile, IntPtr keyFile);

        [DllImport("TrueCrypt.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void KeyFileRemoveAll(ref IntPtr firstKeyFile);

        [DllImport("TrueCrypt.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr KeyFileClone(ref KeyFile keyFile);

        [DllImport("TrueCrypt.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern bool KeyFilesApply(ref Password password, IntPtr firstKeyFile);

        [DllImport("Mpr.dll", CharSet = CharSet.Unicode)]
        private static extern int WNetGetConnection(string lpLocalName, IntPtr lpRemoteName, ref uint lpnLength);

        [DllImport("advapi32.dll", EntryPoint = "OpenSCManagerW", ExactSpelling = true, CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr OpenSCManager(string machineName, string databaseName, uint dwAccess);

        /// <summary>
        /// The OpenSCManager function establishes a connection to the service control manager on the specified 
        /// computer and opens the specified service control manager database.
        /// </summary>
        /// <param name="hSCManager"></param>
        /// <param name="lpServiceName"></param>
        /// <param name="dwDesiredAccess"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr OpenService(IntPtr hSCManager, string lpServiceName, uint dwDesiredAccess);

        /// <summary>
        /// The CloseServiceHandle function closes a handle to a service control manager or service object.
        /// </summary>
        /// <param name="hSCObject"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseServiceHandle(IntPtr hSCObject);

        /// <summary>
        /// Creates an NT service object and adds it to the specified service control manager database.
        /// </summary>
        /// <param name="hSCManager"></param>
        /// <param name="lpServiceName"></param>
        /// <param name="lpDisplayName"></param>
        /// <param name="dwDesiredAccess"></param>
        /// <param name="dwServiceType"></param>
        /// <param name="dwStartType"></param>
        /// <param name="dwErrorControl"></param>
        /// <param name="lpBinaryPathName"></param>
        /// <param name="lpLoadOrderGroup"></param>
        /// <param name="lpdwTagId"></param>
        /// <param name="lpDependencies"></param>
        /// <param name="lpServiceStartName"></param>
        /// <param name="lpPassword"></param>
        /// <returns></returns>
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr CreateService(IntPtr hSCManager, string lpServiceName, string lpDisplayName, 
                                           uint dwDesiredAccess, uint dwServiceType, uint dwStartType,
                                           uint dwErrorControl, string lpBinaryPathName, string lpLoadOrderGroup,
                                           string lpdwTagId, string lpDependencies, string lpServiceStartName, string lpPassword);

        /// <summary>
        /// The StartService function starts a service.
        /// </summary>
        /// <param name="hService"></param>
        /// <param name="dwNumServiceArgs"></param>
        /// <param name="lpServiceArgVector"></param>
        /// <returns></returns>
        [DllImport("advapi32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool StartService(IntPtr hService, int dwNumServiceArgs, string[] lpServiceArgVector);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DeleteService(IntPtr hService);

        [DllImport("shell32.dll")]
        static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, uint wParam, IntPtr lParam, uint fuFlags, uint uTimeout, out uint lpdwResult);

        [DllImport("kernel32.dll")]
        static extern uint GetLogicalDrives();
        #endregion

        #region LocalTypes
        public class TrueCryptException : Exception
        {
            #region Local Types
            public enum ExceptionCause
            {
                DriverLoadFailed,
                DriverOpenFailed,
                DriverIoControlFailed,
                NoDriveLetterAvailable
            }
            #endregion

            #region Attributes
            private ExceptionCause cause;
            private int errorCode;
            private string ioControl;
            #endregion

            #region Properties
            public ExceptionCause Cause
            {
                get { return cause; }
            }

            public int ErrorCode
            {
                get { return errorCode; }
            }

            public string IoControl
            {
                get { return ioControl; }
            }
            #endregion

            #region Constructors
            public TrueCryptException(ExceptionCause cause, int errorCode, string ioControl, string message) :
                base(message)
            {
                this.cause = cause;
                this.errorCode = errorCode;
                this.ioControl = ioControl;
            }

            public TrueCryptException(ExceptionCause cause, int errorCode, string message) :
                this(cause, errorCode, null, message)
            {
            }

            public TrueCryptException(ExceptionCause cause, string message) :
                this(cause, 0, message)
            {
            }
            public TrueCryptException(ExceptionCause cause) :
                this(cause, null)
            {
            }
            #endregion
        }        

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

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        private struct MOUNT_STRUCT
        {
            /// <summary>
            /// Return code back from driver.
            /// </summary>
            [FieldOffset(0)]
            public int nReturnCode;

            [FieldOffset(4)]
            public bool FilesystemDirty;
            [FieldOffset(8)]
            public bool VolumeMountedReadOnlyAfterAccessDenied;
            [FieldOffset(12)]
            public bool VolumeMountedReadOnlyAfterDeviceWriteProtected;

            [FieldOffset(16)]
            public long DiskOffset;
            [FieldOffset(24)]
            public long DiskLength;		
            
            /// <summary>
            /// Volume to be mounted.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = TC_MAX_PATH)]
            [FieldOffset(32)]
            public string wszVolume;

            /// <summary>
            /// User password.
            /// </summary>
            [FieldOffset(552)]
            public Password VolumePassword;

            /// <summary>
            /// Cache passwords in driver.
            /// </summary>
            [FieldOffset(624)]
            public bool bCache;

            /// <summary>
            /// Drive number to mount.
            /// </summary>
            [FieldOffset(628)]
            public int nDosDriveNo;
            [FieldOffset(632)]
            public uint BytesPerSector;
            
            /// <summary>
            /// Mount volume in read-only mode.
            /// </summary>
            [FieldOffset(636)]
            public bool bMountReadOnly;

            /// <summary>
            /// Mount volume as removable media.
            /// </summary>
            [FieldOffset(640)]
            public bool bMountRemovable;

            /// <summary>
            /// Open host file/device in exclusive access mode.
            /// </summary>
            [FieldOffset(644)]
            public bool bExclusiveAccess;

            /// <summary>
            /// Announce volume to mount manager.
            /// </summary>
            [FieldOffset(648)]
            public bool bMountManager;

            /// <summary>
            /// Preserve file container timestamp.
            /// </summary>
            [FieldOffset(652)]
            public bool bPreserveTimestamp;

            /// <summary>
            /// If TRUE, we are to attempt to mount a partition located on an encrypted system drive without pre-boot 
            /// authentication.
            /// </summary>
            [FieldOffset(656)]
            public bool bPartitionInInactiveSysEncScope;

            /// <summary>
            /// If bPartitionInInactiveSysEncScope is TRUE, this contains the drive number of the system drive on which
            /// the partition is located.
            /// </summary>
            [FieldOffset(660)]
            public int nPartitionInInactiveSysEncScopeDriveNo;

            [FieldOffset(664)]
            public bool SystemFavorite;

            /// <summary>
            /// Hidden volume protection.
            /// 
            /// TRUE if the user wants the hidden volume within this volume to be protected against being overwritten 
            /// (damaged).
            /// </summary>
            [FieldOffset(668)]
            public bool bProtectHiddenVolume;

            /// <summary>
            /// Password to the hidden volume to be protected against overwriting.
            /// </summary>
            [FieldOffset(672)]
            public Password ProtectedHidVolPassword;
            [FieldOffset(744)]
            public bool UseBackupHeader;
            [FieldOffset(748)]
            public bool RecoveryMode;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct UMOUNT_STRUCT
        {
            /// <summary>
            /// Drive letter to unmount.
            /// </summary>
            public int nDosDriveNo;
            public bool ignoreOpenFiles;
            public bool HiddenVolumeProtectionTriggered;
            /// <summary>
            /// Return code back from driver.
            /// </summary>
            public int nReturnCode;
        }

        [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
        private struct MOUNT_LIST_STRUCT
        {
            /// <summary>
            /// Bitfield of all mounted drive letters.
            /// </summary>
            [FieldOffset(0)]
	        public uint ulMountedDrives;

            /// <summary>
            /// Volume names of mounted volumes.
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = TC_MAX_PATH * 26)]
            [FieldOffset(4)]
	        public string wszVolume;

            [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.U8, SizeConst = 26)]
            [FieldOffset(13524)]
	        public ulong[] diskLength;
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.I4, SizeConst = 26)]
            [FieldOffset(13732)]
	        public int[] ea;

            /// <summary>
            /// Volume type (e.g. PROP_VOL_TYPE_OUTER, PROP_VOL_TYPE_OUTER_VOL_WRITE_PREVENTED, etc.).
            /// </summary>
            [MarshalAs(UnmanagedType.ByValArray, ArraySubType=UnmanagedType.I4, SizeConst = 26)]
            [FieldOffset(13836)]
	        public int[] volumeType;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct DEV_BROADCAST_VOLUME
        {
            public uint dbcv_size;
            public uint dbcv_devicetype;
            public uint dbcv_reserved;
            public uint dbcv_unitmask;
            public ushort dbcv_flags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KeyFile
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = TC_MAX_PATH)]
            public string FileName;

            private IntPtr Next;
        }
        #endregion

        #region Methods
        public static bool KeyFilesApply(ref Password password, IEnumerable<string> keyfiles)
        {
            // prepare a list in native memory with all keyfiles
            KeyFile keyfileStruct = new KeyFile();
            IntPtr firstKeyfilePtr = IntPtr.Zero;
            foreach (string keyfile in keyfiles)
            {
                keyfileStruct.FileName = keyfile;
                IntPtr keyfilePtr = KeyFileClone(ref keyfileStruct);
                firstKeyfilePtr = KeyFileAdd(firstKeyfilePtr, keyfilePtr);
            }

            // apply the keyfiles to the password
            bool retval = KeyFilesApply(ref password, firstKeyfilePtr);

            // free the memory used by the keyfiles
            KeyFileRemoveAll(ref firstKeyfilePtr);

            return retval;
        }

        /// <summary>
        /// Loads and starts the TestCrypt driver required to mount volumes.
        /// </summary>
        private static void LoadDriver()
        {
            IntPtr hManager = IntPtr.Zero;
            IntPtr hService = IntPtr.Zero;
            try
            {
                // try to locate the correct driver file (amd64 or i386) to load
                FileInfo driverFile = new FileInfo(Wow.Is64BitOperatingSystem ? "testcrypt-x64.sys" : "testcrypt.sys");
                if (!driverFile.Exists)
                {
                    // the driver file is missing
                    Win32Exception ex = new Win32Exception(2 /*ERROR_FILE_NOT_FOUND*/);
                    throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverLoadFailed, ex.ErrorCode, ex.Message);
                }

                // resolve a network driver path to a UNC path name in order to be able to load the driver
                string driverFilePath = driverFile.FullName;
                if (driverFilePath.Substring(1).StartsWith(":\\"))
                {
                    uint length = TC_MAX_PATH * 2;
                    IntPtr pathPtr = Marshal.AllocHGlobal((int)length);
                    if (0 == WNetGetConnection(driverFilePath.Substring(0, 2), pathPtr, ref length))
                    {
                        driverFilePath = string.Format("{0}{1}", Marshal.PtrToStringUni(pathPtr), driverFilePath.Substring(2));
                    }
                    Marshal.FreeHGlobal(pathPtr);
                }

                hManager = OpenSCManager(null, null, SC_MANAGER_ALL_ACCESS);
                if (hManager == IntPtr.Zero)
                {
                    Win32Exception ex = new Win32Exception();
                    throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverLoadFailed, ex.ErrorCode, ex.Message);
                }
                hService = OpenService(hManager, "testcrypt", SERVICE_ALL_ACCESS);
                if (hService != IntPtr.Zero)
                {
                    // Remove stale service (driver is not loaded but service exists)
                    DeleteService(hService);
                    CloseServiceHandle(hService);
                    System.Threading.Thread.Sleep(500);
                }

                hService = CreateService(hManager, "testcrypt", "testcrypt", SERVICE_ALL_ACCESS, SERVICE_KERNEL_DRIVER,
                                         SERVICE_DEMAND_START, SERVICE_ERROR_NORMAL, driverFilePath, null, null, null,
                                         null, null);
                if (hService == IntPtr.Zero)
                {
                    Win32Exception ex = new Win32Exception();
                    throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverLoadFailed, ex.ErrorCode, ex.Message); 
                }

                if (!StartService(hService, 0, null))
                {
                    Win32Exception ex = new Win32Exception();
                    throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverLoadFailed, ex.ErrorCode, ex.Message); 
                }
            }
            finally
            {
                if (hManager != IntPtr.Zero)
                {
                    CloseServiceHandle(hManager);
                }
                if (hService != IntPtr.Zero)
                {
                    DeleteService(hService);
                    CloseServiceHandle(hService);
                }
            }
        }

        /// <summary>
        /// Broadcasts a device change notification to inform the operating system about a device arrival/removal.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="nDosDriveNo"></param>
        /// <param name="driveMap"></param>
        private static void BroadcastDeviceChange(uint message, int nDosDriveNo, uint driveMap)
        {
            uint eventId = 0;
            if (message == DBT_DEVICEARRIVAL)
                eventId = SHCNE_DRIVEADD;
            else if (message == DBT_DEVICEREMOVECOMPLETE)
                eventId = SHCNE_DRIVEREMOVED;
            else if (Wow.IsOSAtLeast(Wow.OSVersion.WIN_7) && message == DBT_DEVICEREMOVEPENDING) // Explorer on Windows 7 holds open handles of all drives when 'Computer' is expanded in navigation pane. SHCNE_DRIVEREMOVED must be used as DBT_DEVICEREMOVEPENDING is ignored.
                eventId = SHCNE_DRIVEREMOVED;

            if (driveMap == 0)
                driveMap = (1U << nDosDriveNo);

            if (eventId != 0)
            {
                for (int i = 0; i < 26; i++)
                {
                    if (0 != (driveMap & (1U << i)))
                    {
                        IntPtr root = Marshal.StringToHGlobalAnsi(string.Format("{0}:\\", Char.ToString((char)('A' + i))));
                        SHChangeNotify(eventId, SHCNF_PATHA, root, IntPtr.Zero);
                        Marshal.FreeHGlobal(root);
                    }
                }
            }

            DEV_BROADCAST_VOLUME dbv = new DEV_BROADCAST_VOLUME();
            dbv.dbcv_size = (uint)Marshal.SizeOf(typeof(DEV_BROADCAST_VOLUME)); 
	        dbv.dbcv_devicetype = DBT_DEVTYP_VOLUME; 
	        dbv.dbcv_reserved = 0;
	        dbv.dbcv_unitmask = driveMap;
	        dbv.dbcv_flags = 0;

            uint timeOut = 1000;

            // SHChangeNotify() works on Vista, so the Explorer does not require WM_DEVICECHANGE
            if (System.Environment.OSVersion.Version.Major >= 6)
                timeOut = 100;

            uint dwResult;
            IntPtr dbvPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(DEV_BROADCAST_VOLUME)));
            Marshal.StructureToPtr(dbv, dbvPtr, true);
            SendMessageTimeout((IntPtr)HWND_BROADCAST, WM_DEVICECHANGE, message, dbvPtr, SMTO_ABORTIFHUNG, timeOut, out dwResult);

            // Explorer prior Vista sometimes fails to register a new drive
            if (System.Environment.OSVersion.Version.Major < 6 && message == DBT_DEVICEARRIVAL)
                SendMessageTimeout((IntPtr)HWND_BROADCAST, WM_DEVICECHANGE, message, dbvPtr, SMTO_ABORTIFHUNG, 200, out dwResult);
            Marshal.FreeHGlobal(dbvPtr);
        }

        /// <summary>
        /// Gets the first available drive letter.
        /// </summary>
        /// <returns>The first available drive letter (as number starting from 0 for "A:") or -1 if no drive letter is available.</returns>
        private static int GetFirstAvailableDrive()
        {
            uint dwUsedDrives = GetLogicalDrives();
            int i;

            for (i = 3; i < 26; i++)
            {
                if (0 == (dwUsedDrives & (1U << i)))
                    return i;
            }

            return -1;
        }

        public static SafeFileHandle OpenDriver()
        {
            SafeFileHandle hDriver = DeviceApi.CreateFile(WIN32_ROOT_PREFIX,
                                                          0,
                                                          DeviceApi.FILE_SHARE_READ | DeviceApi.FILE_SHARE_WRITE,
                                                          IntPtr.Zero,
                                                          DeviceApi.OPEN_EXISTING,
                                                          0,
                                                          IntPtr.Zero);
            if (hDriver.IsInvalid)
            {
                LoadDriver();
                hDriver = DeviceApi.CreateFile(WIN32_ROOT_PREFIX,
                                               0,
                                               DeviceApi.FILE_SHARE_READ | DeviceApi.FILE_SHARE_WRITE,
                                               IntPtr.Zero,
                                               DeviceApi.OPEN_EXISTING,
                                               0,
                                               IntPtr.Zero);
                if (hDriver.IsInvalid)
                {
                    Win32Exception ex = new Win32Exception();
                    throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverOpenFailed, ex.ErrorCode, ex.Message);
                }
            }

            return hDriver;
        }

        /// <summary>
        /// Mounts a volume using the TestCrypt driver.
        /// </summary>
        /// <param name="password">The password of the TrueCrypt volume.</param>
        /// <param name="useBackupHeader">True if the embedded backup header should be used to mount the volume, otherwise false.</param>
        /// <param name="deviceNo">The device which contains the TrueCrypt volume.</param>
        /// <param name="diskOffset">The start offset in bytes of the TrueCrypt volume on the devie.</param>
        /// <param name="diskLength">The length of the TrueCrypt volume in bytes.</param>
        /// <returns>The drive letter (as number starting from 0 for "A:") which has been used to mount the volume.</returns>
        public static int Mount(Password password, bool useBackupHeader, uint deviceNo, long diskOffset, long diskLength)
        {
            IntPtr mountPtr = IntPtr.Zero;
            int dosDriveNo;

            try
            {
                // try to get a available drive letter for the volume
                dosDriveNo = GetFirstAvailableDrive();
                if (-1 != dosDriveNo)
                {
                    // open/load the TestCrypt driver first
                    SafeFileHandle hDriver = OpenDriver();
                    MOUNT_STRUCT mount = new MOUNT_STRUCT();
                    mount.VolumePassword = password;
                    mount.ProtectedHidVolPassword.Length = 0;
                    mount.wszVolume = string.Format("\\Device\\Harddisk{0}\\Partition0", deviceNo);
                    mount.DiskOffset = diskOffset;
                    mount.DiskLength = diskLength;
                    mount.nDosDriveNo = dosDriveNo;
                    mount.UseBackupHeader = useBackupHeader;

                    // without enabling the mount manager, the mounted volume won't be visible
                    mount.bMountManager = true;

                    // due to security reasons, volumes will be mounted read-only
                    mount.bMountReadOnly = true;

                    uint result = 0;
                    mountPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MOUNT_STRUCT)));
                    Marshal.StructureToPtr(mount, mountPtr, true);
                    if (DeviceApi.DeviceIoControl(hDriver, TC_IOCTL_MOUNT_VOLUME, mountPtr, (uint)Marshal.SizeOf(typeof(MOUNT_STRUCT)),
                                                    mountPtr, (uint)Marshal.SizeOf(typeof(MOUNT_STRUCT)), ref result, IntPtr.Zero))
                    {
                        mount = (MOUNT_STRUCT)Marshal.PtrToStructure(mountPtr, typeof(MOUNT_STRUCT));
                        if (mount.nReturnCode != 0)
                        {
                            throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverIoControlFailed, mount.nReturnCode, "TC_IOCTL_MOUNT_VOLUME", null);
                        }
                        else
                        {
                            // inform the operating system about new device arrival
                            BroadcastDeviceChange(DBT_DEVICEARRIVAL, dosDriveNo, 0);
                        }
                    }
                    else
                    {
                        Win32Exception ex = new Win32Exception();
                        throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverIoControlFailed, ex.ErrorCode, "TC_IOCTL_MOUNT_VOLUME", ex.Message);
                    }
                    hDriver.Close();
                }
                else
                {
                    throw new TrueCryptException(TrueCryptException.ExceptionCause.NoDriveLetterAvailable);
                }
            }
            finally
            {
                if (mountPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(mountPtr);
                }
            }

            return dosDriveNo;
        }
       
        /// <summary>
        /// Dismounts all volumes mounted by the TestCrypt driver.
        /// </summary>
        public static void DismountAll()
        {
            IntPtr mountListPtr = IntPtr.Zero;
            IntPtr umountPtr = IntPtr.Zero;

            try
            {
                // open/load the TestCrypt driver first
                SafeFileHandle hDriver = OpenDriver();

                // retrieve a list of all volumes currently mounted by the TestCrypt driver
                uint result = 0;
                mountListPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MOUNT_LIST_STRUCT)));
                if (DeviceApi.DeviceIoControl(hDriver, TC_IOCTL_GET_MOUNTED_VOLUMES, mountListPtr, (uint)Marshal.SizeOf(typeof(MOUNT_LIST_STRUCT)),
                                                mountListPtr, (uint)Marshal.SizeOf(typeof(MOUNT_LIST_STRUCT)), ref result, IntPtr.Zero))
                {
                    MOUNT_LIST_STRUCT mountList = (MOUNT_LIST_STRUCT)Marshal.PtrToStructure(mountListPtr, typeof(MOUNT_LIST_STRUCT));
                    if (mountList.ulMountedDrives != 0)
                    {
                        // inform the operating system about pending device removal
                        BroadcastDeviceChange(DBT_DEVICEREMOVEPENDING, 0, mountList.ulMountedDrives);

                        UMOUNT_STRUCT umount = new UMOUNT_STRUCT();
                        umount.ignoreOpenFiles = true;

                        umountPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(UMOUNT_STRUCT)));
                        Marshal.StructureToPtr(umount, umountPtr, true);
                        if (DeviceApi.DeviceIoControl(hDriver, TC_IOCTL_DISMOUNT_ALL_VOLUMES, umountPtr, (uint)Marshal.SizeOf(typeof(UMOUNT_STRUCT)),
                                                        umountPtr, (uint)Marshal.SizeOf(typeof(UMOUNT_STRUCT)), ref result, IntPtr.Zero))
                        {
                            umount = (UMOUNT_STRUCT)Marshal.PtrToStructure(umountPtr, typeof(UMOUNT_STRUCT));
                            if (umount.nReturnCode != 0)
                            {
                                throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverIoControlFailed, umount.nReturnCode, "TC_IOCTL_DISMOUNT_ALL_VOLUMES", null);
                            }
                            else
                            {
                                // inform the operating system about completed device removal
                                BroadcastDeviceChange(DBT_DEVICEREMOVECOMPLETE, 0, mountList.ulMountedDrives);
                            }
                        }
                        else
                        {
                            Win32Exception ex = new Win32Exception();
                            throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverIoControlFailed, ex.ErrorCode, "TC_IOCTL_DISMOUNT_ALL_VOLUMES", ex.Message);
                        }
                    }
                    else
                    {
                        // there is currently no volume mounted by the TestCrypt driver
                    }
                }
                else
                {
                    Win32Exception ex = new Win32Exception();
                    throw new TrueCryptException(TrueCryptException.ExceptionCause.DriverIoControlFailed, ex.ErrorCode, "TC_IOCTL_GET_MOUNTED_VOLUMES", ex.Message);
                }
                hDriver.Close();
            }
            finally
            {
                if (mountListPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(mountListPtr);
                }
                if (umountPtr != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(umountPtr);
                }
            }
        }
        #endregion
    }
}
