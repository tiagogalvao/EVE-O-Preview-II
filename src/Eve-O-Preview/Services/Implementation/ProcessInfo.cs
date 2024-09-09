using EveOPreview.Services.Interface;

namespace EveOPreview.Services.Implementation;

internal sealed record ProcessInfo(nint Handle, string Title) : IProcessInfo;