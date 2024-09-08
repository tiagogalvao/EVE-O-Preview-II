using System;

namespace EveOPreview.Services.Interface
{
	public interface IProcessInfo
	{
		IntPtr Handle { get; }
		string Title { get; }
	}
}