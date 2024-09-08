using System.Runtime.InteropServices;

namespace EveOPreview.Core.Internal.Interop.Windows
{
	[StructLayout(LayoutKind.Sequential)]
	public class MARGINS
	{
		public int cxLeftWidth;
		public int cxRightWidth;
		public int cyTopHeight;
		public int cyBottomHeight;

		public MARGINS(int left, int top, int right, int bottom)
		{
			cxLeftWidth = left;
			cyTopHeight = top;
			cxRightWidth = right;
			cyBottomHeight = bottom;
		}
	}
}