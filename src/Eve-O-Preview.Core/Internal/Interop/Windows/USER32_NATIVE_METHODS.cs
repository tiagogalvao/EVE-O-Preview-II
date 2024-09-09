using System.Runtime.InteropServices;

namespace EveOPreview.Core.Internal.Interop.Windows
{
    public static class USER32_NATIVE_METHODS
    {
        [DllImport("user32.dll")]
        public static extern nint GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(nint window);

        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(nint hWnd, int nCmdShow);

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(nint hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(nint hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int GetWindowRect(nint hWnd, out RECT rect);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(nint hWnd, out RECT rect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowPlacement(nint hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(nint hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool MoveWindow(nint hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(nint hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsZoomed(nint hWnd);

        [DllImport("user32.dll")]
        public static extern nint GetWindowDC(nint hWnd);

        [DllImport("user32.dll")]
        public static extern nint GetDC(nint hWnd);

        [DllImport("user32.dll")]
        public static extern nint ReleaseDC(nint hWnd, nint hdc);
    }
}