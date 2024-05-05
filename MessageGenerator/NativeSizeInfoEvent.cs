using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics;

namespace MessageGenerator
{
    internal class NativeSizeInfoEvent : INativeEvent
    {
        private readonly NativeMethods.POINT windowMinSize = new();
        public NativeSizeInfoEvent(SizeInt32 minSize)
        {
            windowMinSize.x = minSize.Width;
            windowMinSize.y = minSize.Height;
        }

        public void InvokeEvent(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            NativeMethods.SetWindowMinSize(hWnd, lParam, windowMinSize);
        }
    }
}
