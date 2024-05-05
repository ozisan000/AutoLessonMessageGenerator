using System;
using System.Runtime.InteropServices;

namespace MessageGenerator
{
    public class NativeMethods
    {
        public const int BASE_DPI = 96; //Windowsでは100%解像度では96DPIが基準となっている
        public const int WM_GETMINMAXINFO = 0x0024;
        public const int GWL_WNDPROC = -4;

        //C++のLONG型は32bit想定で書かれているためInt32で直接指定してあげる
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public System.Int32 x;
            public System.Int32 y;
        }

        //構造体をメンバに持つ場合はStructLayoutでフィールドをメモリ上でどのようにレイアウト、すなわち
        //データを管理するかを指定してあげる必要があり,これを「アライメント」と呼ぶ。マーシャル前提の構造体の場合
        //LayoutKind.Sequential(シーケンシャル)を指定してあげる必要がある、これはメモリ上にフィールドのデータを
        //順番通りにレイアウトすることを指す。Explicitで地震でそれらを定義することもできる。
        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        public delegate IntPtr WinProc(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        //[DllImport("user32")]
        //public static extern IntPtr SetWindowLong(IntPtr hWnd, PInvoke.User32.WindowLongIndexFlags nIndex, WinProc newProc);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, WinProc dwNewLong);
        [DllImport("user32.dll")]
        public static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int GetDpiForWindow(IntPtr hwnd);


        //
        public static void SetWindowMinSize(IntPtr hWnd, IntPtr lParam, POINT minSize)
        {
            // ウインドウのサイズが変わるたびにここを通る
            var dpi = GetDpiForWindow(hWnd);
            float scalingFactor = (float)dpi / BASE_DPI;

            // ここで、最上の大きさをMINMAXINFOに入れて指定する
            MINMAXINFO minMaxInfo = Marshal.PtrToStructure<MINMAXINFO>(lParam);
            minMaxInfo.ptMinTrackSize.x = (int)(minSize.x * scalingFactor);
            minMaxInfo.ptMinTrackSize.y = (int)(minSize.y * scalingFactor);
            Marshal.StructureToPtr(minMaxInfo, lParam, true);
        }
    }
}

