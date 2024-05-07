using Native.NativeEvents;
using Windows.Graphics;
using static Native.NativeMethods;

namespace Native
{
    public class NativeControlFactory
    {
        public static WindowNativeControl CreateWindowNativeControl(object hwnd, SizeInt32 createSize, SizeInt32 minSize)
        {
            var nativeControl = new WindowNativeControl(hwnd, createSize);

            //ウィンドウの最小サイズを設定する処理を追加
            nativeControl.RegistEvent(
                WM_GETMINMAXINFO,
                new NativeSizeInfoEvent(minSize)
                );

            return nativeControl;
        }

        public static WindowNativeControl CreateWindowNativeControl(object hwnd, SizeInt32 createSize, SizeInt32 minSize, SizeInt32 maxSize)
        {
            var nativeControl = new WindowNativeControl(hwnd, createSize);

            //ウィンドウの最小最大サイズを設定する処理を追加
            nativeControl.RegistEvent(
                WM_GETMINMAXINFO,
                new NativeSizeInfoEvent(minSize, maxSize)
                );

            return nativeControl;
        }
    }
}
