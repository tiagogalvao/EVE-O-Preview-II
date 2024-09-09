using System.Runtime.InteropServices;

namespace EveOPreview.Core.Internal.Interop.Windows
{
	public static class GDI32_NATIVE_METHODS
	{
		public const int SRCCOPY = 13369376;

		[DllImport("gdi32.dll")]
		public static extern nint CreateCompatibleDC(nint hdc);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteDC(nint hdc);

		[DllImport("gdi32.dll")]
		public static extern nint CreateCompatibleBitmap(nint hdc, int width, int height);

		[DllImport("gdi32.dll")]
		public static extern nint SelectObject(nint hdc, nint hObject);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(nint hObject);

		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(nint hObject, int nXDest, int nYDest, int nWidth, int nHeight, nint hObjectSource, int nXSrc, int nYSrc, int dwRop);
	}
}
