using System.Runtime.InteropServices;

namespace EveOPreview.Core.Internal.Interop.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public class DWM_THUMBNAIL_PROPERTIES
    {
        public uint dwFlags;
        public RECT rcDestination;
        public RECT rcSource;
        public byte opacity;
        
        [MarshalAs(UnmanagedType.Bool)] 
        public bool fVisible;
        
        [MarshalAs(UnmanagedType.Bool)] 
        public bool fSourceClientAreaOnly;
    }
}