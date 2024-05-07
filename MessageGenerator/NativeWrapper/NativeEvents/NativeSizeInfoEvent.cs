using System;
using Windows.Graphics;
using static Native.NativeMethods;
using static Native.NativeMethodsHelper;

namespace Native.NativeEvents
{
    internal class NativeSizeInfoEvent : INativeEvent
    {
        private readonly POINT windowMinSize = new();
        private readonly POINT windowMaxSize = new();
        private readonly Action<IntPtr, IntPtr> _SetWindowTrackSize;

        public NativeSizeInfoEvent(SizeInt32 minSize)
        {
            windowMinSize.x = minSize.Width;
            windowMinSize.y = minSize.Height;

            _SetWindowTrackSize = (hWnd, lParam) =>
            SetWindowMinSize(
                hWnd,
                lParam,
                windowMinSize
                );
        }

        public NativeSizeInfoEvent(SizeInt32 minSize, SizeInt32 maxSize)
        {
            windowMinSize.x = minSize.Width;
            windowMinSize.y = minSize.Height;

            windowMaxSize.x = maxSize.Width;
            windowMaxSize.y = maxSize.Height;

            _SetWindowTrackSize = (hWnd, lParam) =>
            {
                SetWindowMinSize(
                    hWnd,
                    lParam,
                    windowMinSize
                );

                SetWindowMaxSize(
                    hWnd,
                    lParam,
                windowMaxSize
                );
            };
        }

        public void InvokeEvent(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            _SetWindowTrackSize(hWnd, lParam);
        }
    }
}
