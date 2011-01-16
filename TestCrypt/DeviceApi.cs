using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;

namespace TestCrypt
{
    static class DeviceApi
    {
        #region Constants
        /// <summary>
        /// Read, write, and execute access.
        /// </summary>
        public const uint GENERIC_ALL              = 0x01U << 28;

        /// <summary>
        /// Execute access.
        /// </summary>
        public const uint GENERIC_EXECUTE          = 0x01U << 29;

        /// <summary>
        /// Write access.
        /// </summary>
        public const uint GENERIC_WRITE            = 0x01U << 30;

        /// <summary>
        /// Read access.
        /// </summary>
        public const uint GENERIC_READ             = 0x01U << 31;      


        /// <summary>
        /// Prevents other processes from opening a file or device if they request delete, read, or write access.
        /// </summary>
        public const uint FILE_SHARE_NONE          = 0x00000000U;

        /// <summary>
        /// Enables subsequent open operations on a file or device to request read access.
        /// 
        /// Otherwise, other processes cannot open the file or device if they request read access.
        /// 
        /// If this flag is not specified, but the file or device has been opened for read access, the function fails.
        /// </summary>
        public const uint FILE_SHARE_READ          = 0x00000001U;

        /// <summary>
        /// Enables subsequent open operations on a file or device to request write access.
        /// 
        /// Otherwise, other processes cannot open the file or device if they request write access.
        /// 
        /// If this flag is not specified, but the file or device has been opened for write access or has a file 
        /// mapping with write access, the function fails.
        /// </summary>
        public const uint FILE_SHARE_WRITE         = 0x00000002U;

        /// <summary>
        /// Enables subsequent open operations on a file or device to request delete access.
        ///
        /// Otherwise, other processes cannot open the file or device if they request delete access.
        ///
        /// If this flag is not specified, but the file or device has been opened for delete access, the function fails.
        /// </summary>
        /// <remarks>
        /// Delete access allows both delete and rename operations.
        /// </remarks>
        public const uint FILE_SHARE_DELETE        = 0x00000004U;


        /// <summary>
        /// Creates a new file, only if it does not already exist.
        /// 
        /// If the specified file exists, the function fails and the last-error code is set to ERROR_FILE_EXISTS (80).
        /// 
        /// If the specified file does not exist and is a valid path to a writable location, a new file is created.
        /// </summary>
        public const uint CREATE_NEW               = 1U;

        /// <summary>
        /// Creates a new file, always.
        /// 
        /// If the specified file exists and is writable, the function overwrites the file, the function succeeds, and 
        /// last-error code is set to ERROR_ALREADY_EXISTS (183).
        /// 
        /// If the specified file does not exist and is a valid path, a new file is created, the function succeeds, and
        /// the last-error code is set to zero.
        /// </summary>
        public const uint CREATE_ALWAYS            = 2U;

        /// <summary>
        /// Opens a file or device, only if it exists.
        /// 
        /// If the specified file or device does not exist, the function fails and the last-error code is set to 
        /// ERROR_FILE_NOT_FOUND (2).
        /// </summary>
        public const uint OPEN_EXISTING            = 3U;

        /// <summary>
        /// Opens a file, always.
        /// 
        /// If the specified file exists, the function succeeds and the last-error code is set to 
        /// ERROR_ALREADY_EXISTS (183).
        /// 
        /// If the specified file does not exist and is a valid path to a writable location, the function creates a 
        /// file and the last-error code is set to zero.
        /// </summary>
        public const uint OPEN_ALWAYS              = 4U;

        /// <summary>
        /// Opens a file and truncates it so that its size is zero bytes, only if it exists.
        /// 
        /// If the specified file does not exist, the function fails and the last-error code is set to 
        /// ERROR_FILE_NOT_FOUND (2).
        /// 
        /// The calling process must open the file with the GENERIC_WRITE bit set as part of the dwDesiredAccess 
        /// parameter.
        /// </summary>
        public const uint TRUNCATE_EXISTING        = 5U;


        /// <summary>
        /// The file is read only. Applications can read the file, but cannot write to or delete it.
        /// </summary>
        public const uint FILE_ATTRIBUTE_READONLY  = 0x00000001U;

        /// <summary>
        /// The file is hidden. Do not include it in an ordinary directory listing.
        /// </summary>
        public const uint FILE_ATTRIBUTE_HIDDEN    = 0x00000002U;

        /// <summary>
        /// The file is part of or used exclusively by an operating system.
        /// </summary>
        public const uint FILE_ATTRIBUTE_SYSTEM    = 0x00000004U;

        /// <summary>
        /// The file should be archived. Applications use this attribute to mark files for backup or removal.
        /// </summary>
        public const uint FILE_ATTRIBUTE_ARCHIVE   = 0x00000020U;

        /// <summary>
        /// The file does not have other attributes set. This attribute is valid only if used alone.
        /// </summary>
        public const uint FILE_ATTRIBUTE_NORMAL    = 0x00000080U;

        /// <summary>
        /// The file is being used for temporary storage.
        /// 
        /// For more information, see the Caching Behavior section of this topic.
        /// </summary>
        public const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100U;

        /// <summary>
        /// The data of a file is not immediately available. This attribute indicates that file data is physically
        /// moved to offline storage. This attribute is used by Remote Storage, the hierarchical storage management
        /// software. Applications should not arbitrarily change this attribute.
        /// </summary>
        public const uint FILE_ATTRIBUTE_OFFLINE   = 0x00001000U;

        /// <summary>
        /// The file or directory is encrypted. For a file, this means that all data in the file is encrypted. For a 
        /// directory, this means that encryption is the default for newly created files and subdirectories. For more
        /// information, see File Encryption.
        /// 
        /// This flag has no effect if FILE_ATTRIBUTE_SYSTEM is also specified.
        /// </summary>
        public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000U;

        public const int ERROR_SHARING_VIOLATION   = 32;
        #endregion

        #region P/Invoke
        /// <summary>
        /// The CreateFile function creates or opens a file, file stream, directory, physical disk, volume, console buffer, tape drive,
        /// communications resource, mailslot, or named pipe. The function returns a handle that can be used to access an object.
        /// </summary>
        /// <param name="lpFileName"></param>
        /// <param name="dwDesiredAccess"> access to the object, which can be read, write, or both</param>
        /// <param name="dwShareMode">The sharing mode of an object, which can be read, write, both, or none</param>
        /// <param name="SecurityAttributes">A pointer to a SECURITY_ATTRIBUTES structure that determines whether or not the returned handle can
        /// be inherited by child processes. Can be null</param>
        /// <param name="dwCreationDisposition">An action to take on files that exist and do not exist</param>
        /// <param name="dwFlagsAndAttributes">The file attributes and flags. </param>
        /// <param name="hTemplateFile">A handle to a template file with the GENERIC_READ access right. The template file supplies file attributes
        /// and extended attributes for the file that is being created. This parameter can be null</param>
        /// <returns>If the function succeeds, the return value is an open handle to a specified file. If a specified file exists before the function
        /// all and dwCreationDisposition is CREATE_ALWAYS or OPEN_ALWAYS, a call to GetLastError returns ERROR_ALREADY_EXISTS, even when the function
        /// succeeds. If a file does not exist before the call, GetLastError returns 0 (zero).
        /// If the function fails, the return value is INVALID_HANDLE_VALUE. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern SafeFileHandle CreateFile(
              string lpFileName,
              uint dwDesiredAccess,
              uint dwShareMode,
              IntPtr SecurityAttributes,
              uint dwCreationDisposition,
              uint dwFlagsAndAttributes,
              IntPtr hTemplateFile);

        /// <summary>
        /// Sends a control code directly to a specified device driver, causing the corresponding device to perform the corresponding operation.
        /// </summary>
        /// <param name="hDevice"></param>
        /// <param name="dwIoControlCode"></param>
        /// <param name="lpInBuffer"></param>
        /// <param name="nInBufferSize"></param>
        /// <param name="lpOutBuffer"></param>
        /// <param name="nOutBufferSize"></param>
        /// <param name="lpBytesReturned"></param>
        /// <param name="lpOverlapped"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool DeviceIoControl(
              SafeHandle hDevice,
              uint dwIoControlCode,
              IntPtr lpInBuffer,           
              uint nInBufferSize,
              [Out] IntPtr lpOutBuffer,
              uint nOutBufferSize,
              ref uint lpBytesReturned,
              IntPtr lpOverlapped);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool SetFilePointerEx(
              SafeFileHandle hFile,
              Int64  liDistanceToMove,
              IntPtr lpNewFilePointer,
              EMoveMethod dwMoveMethod);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadFile(
              SafeFileHandle hFile,
              byte[] lpBuffer,
              uint nNumberOfBytesToRead,
              out uint lpNumberOfBytesRead,
              IntPtr lpOverlapped);
        #endregion

        #region Local Types
        public enum EMoveMethod : uint
        {
            Begin = 0,
            Current = 1,
            End = 2
        }
        #endregion
    }
}
