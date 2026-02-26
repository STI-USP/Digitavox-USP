using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Digitavox.Platforms.Windows
{
    public class WinAPI
    {
        public delegate void HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetCurrentThreadId();

        public const int WH_Keyboard = 2;

        public const int KF_Up = 0x8000;

        internal static int HIWord(IntPtr wParam)
        {
            return (int)((wParam.ToInt64() >> 16) & 0xffff);
        }
    }
}
