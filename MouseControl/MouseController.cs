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

        public void moveMouse(int pointX, int pointY)
        {
            SetCursorPos(pointX, pointY);
        }
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
    }
}
