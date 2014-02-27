using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Runtime.InteropServices;
using WindowsInput;

namespace MouseControl
{

    // Controls mouse movement
    static class MouseController
    {
        // Move mouse position
        public static void moveMouse(int pointX, int pointY)
        {
            SetCursorPos(pointX, pointY);
        }
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        // Get Mouse Position
        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };
        public static int[] GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            int[] pos = new int[2];
            pos[0] = w32Mouse.X;
            pos[1] = w32Mouse.Y;
            return pos;
        }

        // Do mouse clicks
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;

        // Mouse left click
        public static void MouseLeft(bool isDbl)
        {
            int[] pos = GetMousePosition();
            uint uX = (uint)(int)pos[0];
            uint uY = (uint)(int)pos[1];
            UIntPtr ptr = new UIntPtr();
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, uX, uY, 0, ptr);
            
            // Is it a double click
            if (isDbl)
            {
                System.Threading.Thread.Sleep(200);
                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, uX, uY, 0, ptr);
            }
        }

        // Mouse right click
        public static void MouseRight()
        {
            int[] pos = GetMousePosition();
            uint uX = (uint)(int)pos[0];
            uint uY = (uint)(int)pos[1];
            UIntPtr ptr = new UIntPtr();
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, uX, uY, 0, ptr);
        }

        // Keyboard input
        public static void SendKey(string KeyCode)
        {
            int KeyCodeInt = Convert.ToInt32(KeyCode);
            InputSimulator.SimulateKeyPress((VirtualKeyCode)KeyCodeInt);            
        }
    }
}
