using System;

namespace Native
{
    public interface INativeEvent
    {
        public void InvokeEvent(IntPtr hWnd, IntPtr wParam, IntPtr lParam);
    }
}
