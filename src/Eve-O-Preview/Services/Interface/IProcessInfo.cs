using System;

namespace EveOPreview.Services.Interface
{
	public interface IProcessInfo
	{
		nint Handle { get; }
		string Title { get; }
	}
}