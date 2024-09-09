using System.Drawing;
using System.Runtime.InteropServices;

namespace EveOPreview.Core.Internal.Interop.Windows
{
    public static class DWM_NATIVE_METHODS
    {
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableBlurBehindWindow(nint hWnd, DWM_BLURBEHIND pBlurBehind);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmExtendFrameIntoClientArea(nint hWnd, MARGINS pMargins);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmGetColorizationColor(out int pcrColorization, [MarshalAs(UnmanagedType.Bool)] out bool pfOpaqueBlend);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmEnableComposition(bool bEnable);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern nint DwmRegisterThumbnail(nint dest, nint source);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUnregisterThumbnail(nint hThumbnail);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmUpdateThumbnailProperties(nint hThumbnail, DWM_THUMBNAIL_PROPERTIES props);

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern void DwmQueryThumbnailSourceSize(nint hThumbnail, out Size size);
    }
}