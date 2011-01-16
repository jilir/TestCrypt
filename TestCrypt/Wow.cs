using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

namespace TestCrypt
{
    /// <summary>
    /// Class to check for 64-bit Windows Operating System.
    /// 
    /// http://social.msdn.microsoft.com/forums/en-US/csharpgeneral/thread/24792cdc-2d8e-454b-9c68-31a19892ca53/
    /// </summary>
    public static class Wow
    {
        #region LocalTypes
        public enum OSVersion
        {
            // IMPORTANT: If you add a new item here, update IsOSAtLeast().
            WIN_UNKNOWN = 0,
            WIN_31,
            WIN_95,
            WIN_98,
            WIN_ME,
            WIN_NT3,
            WIN_NT4,
            WIN_2000,
            WIN_XP,
            WIN_XP64,
            WIN_SERVER_2003,
            WIN_VISTA,
            WIN_SERVER_2008,
            WIN_7,
            WIN_SERVER_2008_R2
        }
        #endregion

        #region Methods
        public static bool Is64BitProcess
        {
            get { return IntPtr.Size == 8; }
        }

        public static bool Is64BitOperatingSystem
        {
            get
            {
                // Clearly if this is a 64-bit process we must be on a 64-bit OS.
                if (Is64BitProcess)
                    return true;
                // Ok, so we are a 32-bit process, but is the OS 64-bit?
                // If we are running under Wow64 than the OS is 64-bit.
                bool isWow64;
                return ModuleContainsFunction("kernel32.dll", "IsWow64Process") && IsWow64Process(GetCurrentProcess(), out isWow64) && isWow64;
            }
        }

        public static bool IsOSAtLeast(OSVersion reqMinOS)
        {
            uint major = 0, minor = 0;

            switch (reqMinOS)
            {
                case OSVersion.WIN_2000: major = 5; minor = 0; break;
                case OSVersion.WIN_XP: major = 5; minor = 1; break;
                case OSVersion.WIN_SERVER_2003: major = 5; minor = 2; break;
                case OSVersion.WIN_VISTA: major = 6; minor = 0; break;
                case OSVersion.WIN_7: major = 6; minor = 1; break;

                default:
                    System.Diagnostics.Debug.Assert(false);
                    break;
            }

            return ((Environment.OSVersion.Version.Major << 16 | Environment.OSVersion.Version.Minor << 8) >= (major << 16 | minor << 8));
        }

        static bool ModuleContainsFunction(string moduleName, string methodName)
        {
            IntPtr hModule = GetModuleHandle(moduleName);
            if (hModule != IntPtr.Zero)
                return GetProcAddress(hModule, methodName) != IntPtr.Zero;
            return false;
        }
        #endregion

        #region P/Invoke
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        extern static bool IsWow64Process(IntPtr hProcess, [MarshalAs(UnmanagedType.Bool)] out bool isWow64);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        extern static IntPtr GetCurrentProcess();
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        extern static IntPtr GetModuleHandle(string moduleName);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        extern static IntPtr GetProcAddress(IntPtr hModule, string methodName);
        #endregion
    }
}
