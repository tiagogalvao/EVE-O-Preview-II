using System;
using System.Drawing;
using System.Runtime.InteropServices;
using EveOPreview.Core.Internal.Interop.Windows;
using EveOPreview.Services.Interface;

namespace EveOPreview.Services.Implementation
{
    internal sealed class WindowManager : IWindowManager
    {
        private const int WINDOW_SIZE_THRESHOLD = 300;

        public WindowManager()
        {
            IsCompositionEnabled = IsCompositionSupported();
        }

        public bool IsCompositionEnabled { get; }

        private bool IsCompositionSupported()
        {
            var osVersion = Environment.OSVersion.Version;
            return (osVersion.Major == 6 && osVersion.Minor >= 2) // Windows 8, 8.1
                   || osVersion.Major >= 10 // Windows 10+
                   || DWM_NATIVE_METHODS.DwmIsCompositionEnabled(); // Windows 7 fallback
        }

        public nint GetForegroundWindowHandle() => USER32_NATIVE_METHODS.GetForegroundWindow();

        public void ActivateWindow(nint handle)
        {
            USER32_NATIVE_METHODS.SetForegroundWindow(handle);
            int style = USER32_NATIVE_METHODS.GetWindowLong(handle, InteropConstants.GWL_STYLE);

            if ((style & InteropConstants.WS_MINIMIZE) != 0)
            {
                USER32_NATIVE_METHODS.ShowWindowAsync(handle, InteropConstants.SW_RESTORE);
            }
        }

        public void MinimizeWindow(nint handle, bool enableAnimation)
        {
            if (enableAnimation)
            {
                USER32_NATIVE_METHODS.SendMessage(handle, InteropConstants.WM_SYSCOMMAND, InteropConstants.SC_MINIMIZE, 0);
            }
            else
            {
                AdjustWindowPlacement(handle, WINDOWPLACEMENT.SW_MINIMIZE);
            }
        }

        public void MaximizeWindow(nint handle)
        {
            USER32_NATIVE_METHODS.ShowWindowAsync(handle, InteropConstants.SW_SHOWMAXIMIZED);
        }

        public void MoveWindow(nint handle, int left, int top, int width, int height)
        {
            USER32_NATIVE_METHODS.MoveWindow(handle, left, top, width, height, true);
        }

        public (int Left, int Top, int Right, int Bottom) GetWindowPosition(nint handle)
        {
            USER32_NATIVE_METHODS.GetWindowRect(handle, out RECT rect);
            return (rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        public bool IsWindowMaximized(nint handle) => USER32_NATIVE_METHODS.IsZoomed(handle);

        public bool IsWindowMinimized(nint handle) => USER32_NATIVE_METHODS.IsIconic(handle);

        public IDwmThumbnail GetLiveThumbnail(nint destination, nint source)
        {
            var thumbnail = new DwmThumbnail(this);
            thumbnail.Register(destination, source);
            return thumbnail;
        }

        public Image? GetStaticThumbnail(nint source)
        {
            nint sourceContext = USER32_NATIVE_METHODS.GetDC(source);

            USER32_NATIVE_METHODS.GetClientRect(source, out RECT windowRect);
            int width = windowRect.Right - windowRect.Left;
            int height = windowRect.Bottom - windowRect.Top;

            if (width < WINDOW_SIZE_THRESHOLD || height < WINDOW_SIZE_THRESHOLD)
            {
                USER32_NATIVE_METHODS.ReleaseDC(source, sourceContext);
                return null;
            }

            nint destContext = GDI32_NATIVE_METHODS.CreateCompatibleDC(sourceContext);
            nint bitmap = GDI32_NATIVE_METHODS.CreateCompatibleBitmap(sourceContext, width, height);

            nint oldBitmap = GDI32_NATIVE_METHODS.SelectObject(destContext, bitmap);
            GDI32_NATIVE_METHODS.BitBlt(destContext, 0, 0, width, height, sourceContext, 0, 0, GDI32_NATIVE_METHODS.SRCCOPY);
            GDI32_NATIVE_METHODS.SelectObject(destContext, oldBitmap);

            GDI32_NATIVE_METHODS.DeleteDC(destContext);
            USER32_NATIVE_METHODS.ReleaseDC(source, sourceContext);

            Image image = Image.FromHbitmap(bitmap);
            GDI32_NATIVE_METHODS.DeleteObject(bitmap); // Properly delete the bitmap handle to avoid leaks
            return image;
        }

        private static void AdjustWindowPlacement(nint handle, int command)
        {
            var placement = new WINDOWPLACEMENT
            {
                length = Marshal.SizeOf<WINDOWPLACEMENT>()
            };

            USER32_NATIVE_METHODS.GetWindowPlacement(handle, ref placement);
            placement.showCmd = command;
            USER32_NATIVE_METHODS.SetWindowPlacement(handle, ref placement);
        }
    }
}