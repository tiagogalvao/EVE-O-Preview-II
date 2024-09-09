using System;
using System.Drawing;

namespace EveOPreview.Services.Interface
{
    public interface IWindowManager
    {
        bool IsCompositionEnabled { get; }

        nint GetForegroundWindowHandle();
        void ActivateWindow(nint handle);
        void MinimizeWindow(nint handle, bool enableAnimation);
        void MoveWindow(nint handle, int left, int top, int width, int height);
        void MaximizeWindow(nint handle);
        (int Left, int Top, int Right, int Bottom) GetWindowPosition(nint handle);
        bool IsWindowMaximized(nint handle);
        bool IsWindowMinimized(nint handle);
        IDwmThumbnail GetLiveThumbnail(nint destination, nint source);
        Image GetStaticThumbnail(nint source);
    }
}