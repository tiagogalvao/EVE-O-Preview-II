using System.Runtime.InteropServices;

namespace EveOPreview.Core.Internal.Interop.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public class DWM_BLURBEHIND
    {
        public uint dwFlags;
        public IntPtr hRegionBlur;
        
        [MarshalAs(UnmanagedType.Bool)] 
        public bool fEnable;

        [MarshalAs(UnmanagedType.Bool)] 
        public bool fTransitionOnMaximized;

        public const uint DWM_BB_ENABLE = 0x00000001;
        public const uint DWM_BB_BLURREGION = 0x00000002;
        public const uint DWM_BB_TRANSITIONONMAXIMIZED = 0x00000004;
    }
}