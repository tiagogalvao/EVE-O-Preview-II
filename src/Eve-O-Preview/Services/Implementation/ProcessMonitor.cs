using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EveOPreview.Services.Interface;

namespace EveOPreview.Services.Implementation;

internal sealed class ProcessMonitor : IProcessMonitor
{
    private const string DEFAULT_PROCESS_NAME = "ExeFile";
    private readonly ConcurrentDictionary<nint, string> _processCache = new();
    private IProcessInfo _currentProcessInfo = new ProcessInfo(nint.Zero, string.Empty);

    private static bool IsMonitoredProcess(string processName) =>
        string.Equals(processName, DEFAULT_PROCESS_NAME, StringComparison.OrdinalIgnoreCase);

    private static IProcessInfo GetCurrentProcessInfo()
    {
        using var currentProcess = Process.GetCurrentProcess();
        return new ProcessInfo(currentProcess.MainWindowHandle, currentProcess.MainWindowTitle);
    }

    public IProcessInfo GetMainProcess()
    {
        if (_currentProcessInfo.Handle == nint.Zero)
        {
            var processInfo = GetCurrentProcessInfo();
            if (!string.IsNullOrEmpty(processInfo.Title))
            {
                _currentProcessInfo = processInfo;
            }
        }

        return _currentProcessInfo;
    }

    public ICollection<IProcessInfo> GetAllProcesses()
    {
        var result = new List<IProcessInfo>(_processCache.Count);
        foreach (var (handle, title) in _processCache)
        {
            result.Add(new ProcessInfo(handle, title));
        }

        return result;
    }

    public void GetUpdatedProcesses(out ICollection<IProcessInfo> addedProcesses, out ICollection<IProcessInfo> updatedProcesses, out ICollection<IProcessInfo> removedProcesses)
    {
        addedProcesses = new List<IProcessInfo>();
        updatedProcesses = new List<IProcessInfo>();
        removedProcesses = new List<IProcessInfo>();

        var currentProcesses = Process.GetProcesses()
            .Where(p => IsMonitoredProcess(p.ProcessName) && p.MainWindowHandle != nint.Zero);

        var knownHandles = new HashSet<nint>(_processCache.Keys);

        foreach (var process in currentProcesses)
        {
            var handle = process.MainWindowHandle;
            var title = process.MainWindowTitle;

            if (_processCache.TryGetValue(handle, out var cachedTitle))
            {
                if (!string.Equals(cachedTitle, title, StringComparison.Ordinal))
                {
                    _processCache[handle] = title;
                    updatedProcesses.Add(new ProcessInfo(handle, title));
                }

                knownHandles.Remove(handle);
            }
            else
            {
                _processCache.TryAdd(handle, title);
                addedProcesses.Add(new ProcessInfo(handle, title));
            }
        }

        foreach (var handle in knownHandles)
        {
            if (_processCache.TryRemove(handle, out var title))
            {
                removedProcesses.Add(new ProcessInfo(handle, title));
            }
        }
    }
}