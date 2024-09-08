﻿using System.Runtime.InteropServices;

namespace EveOPreview.Core.Internal.Interop.Windows
{
	public static class GDI32_NATIVE_METHODS
	{
		public const int SRCCOPY = 13369376;

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteDC(IntPtr hdc);

		[DllImport("gdi32.dll")]
		public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int width, int height);

		[DllImport("gdi32.dll")]
		public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hObject);

		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr hObject);

		[DllImport("gdi32.dll")]
		public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);
	}
}
