using System;
using EveOPreview.Services.Interface;

namespace EveOPreview.Services.Implementation
{
	internal sealed class ProcessInfo : IProcessInfo
	{
		public ProcessInfo(IntPtr handle, string title)
		{
			Handle = handle;
			Title = title;
		}

		public IntPtr Handle { get; }
		public string Title { get; }
	}
}