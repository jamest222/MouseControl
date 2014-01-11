using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace MouseControl
{

    // Controls mouse movement
    class MouseController
    {
        Cursor currCursor;

        public MouseController()
        {
            //
        }

        public static void moveMouse(int pointX, int pointY)
        {
            SetCursorPos(pointX, pointY);
        }
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        // get mouse pos
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
    }
}
