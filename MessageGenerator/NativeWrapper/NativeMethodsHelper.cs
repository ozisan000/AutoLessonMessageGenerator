using static Native.NativeMethods;
using System;
using System.Runtime.InteropServices;

namespace Native
{

    public static class NativeMethodsHelper
    {
        public static void SetWindowMinSize(IntPtr hWnd, IntPtr lParam, POINT minSize)
        {
            // ウインドウのサイズが変わるたびにここを通る
            float scalingFactor = GetDpiScalingFactor(hWnd);

            // ここで、最上の大きさをMINMAXINFOに入れて指定する
            MINMAXINFO minMaxInfo = Marshal.PtrToStructure<MINMAXINFO>(lParam);
            minMaxInfo.ptMinTrackSize.x = (int)(minSize.x * scalingFactor);
            minMaxInfo.ptMinTrackSize.y = (int)(minSize.y * scalingFactor);
            Marshal.StructureToPtr(minMaxInfo, lParam, true);
        }

        public static void SetWindowMaxSize(IntPtr hWnd, IntPtr lParam, POINT maxSize)
        {
            // ウインドウのサイズが変わるたびにここを通る
            float scalingFactor = GetDpiScalingFactor(hWnd);

            // ここで、最上の大きさをMINMAXINFOに入れて指定する
            MINMAXINFO minMaxInfo = Marshal.PtrToStructure<MINMAXINFO>(lParam);
            minMaxInfo.ptMaxTrackSize.x = (int)(maxSize.x * scalingFactor);
            minMaxInfo.ptMaxTrackSize.y = (int)(maxSize.y * scalingFactor);
            Marshal.StructureToPtr(minMaxInfo, lParam, true);
        }

        public static float GetDpiScalingFactor(IntPtr hwnd)
        {
            return (float)GetDpiForWindow(hwnd) / BASE_DPI;
        }
    }
}

