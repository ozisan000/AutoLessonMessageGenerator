using Microsoft.UI.Windowing;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Windows.Graphics;
using System.Collections;

namespace MessageGenerator
{
    public class WindowNativeControl
    {
        private NativeMethods.WinProc _newWndProc = null;
        private IntPtr _oldWndProc = IntPtr.Zero;

        private readonly Dictionary<int, INativeEvent> _checkEvents = new Dictionary<int, INativeEvent>();

        public WindowNativeControl(object hwnd)
        {
            // ウインドウのハンドルを取ってくる
            InitWindowProc(hwnd);
        }

        public WindowNativeControl(object hwnd, SizeInt32 craeteSize, bool isResizable = true)
        {
            var intPtrhwnd = InitWindowProc(hwnd);

            var mainWindowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(intPtrhwnd);
            AppWindow thisWindow = AppWindow.GetFromWindowId(mainWindowId);

            //// ウィンドウサイズを指定する
            thisWindow.Resize(craeteSize);

            // ウィンドウサイズを変更不能にする
            var overlayPresenter = thisWindow.Presenter as OverlappedPresenter;
            overlayPresenter.IsResizable = isResizable;
        }

        public void RegistEvent(int msg, INativeEvent registEvent)
        {
            _checkEvents.Add(msg, registEvent);
        }

        private IntPtr InitWindowProc(object hwnd)
        {
            // ウインドウのハンドルを取ってくる
            var intPtrhwnd = WinRT.Interop.WindowNative.GetWindowHandle(hwnd);

            _newWndProc = new NativeMethods.WinProc(NewWindowProc);
            _oldWndProc = NativeMethods.SetWindowLongPtr64(intPtrhwnd, NativeMethods.GWL_WNDPROC, _newWndProc);
            return intPtrhwnd;
        }

        /// <summary>
        /// 黒魔術横取りウィンドウプロシージャ
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        private IntPtr NewWindowProc(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam)
        {
            foreach (var checkEvent in _checkEvents)
            {
                if (checkEvent.Key == Msg) checkEvent.Value.InvokeEvent(hWnd, wParam, lParam);
            }
            // WM_GETMINMAXINFO以外の、自分で処理したくないMsgは、もとのWndProcに任せる
            return NativeMethods.CallWindowProc(_oldWndProc, hWnd, Msg, wParam, lParam);
        }
    }
}
