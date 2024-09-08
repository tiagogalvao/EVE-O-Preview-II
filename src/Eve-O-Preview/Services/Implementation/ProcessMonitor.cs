using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using EveOPreview.Services.Interface;

namespace EveOPreview.Services.Implementation
{
    internal sealed class ProcessMonitor : IProcessMonitor
    {
        private const string DEFAULT_PROCESS_NAME = "ExeFile";
        private readonly ConcurrentDictionary<IntPtr, string> _processCache;
        private IProcessInfo _currentProcessInfo;

        public ProcessMonitor()
        {
            _processCache = new ConcurrentDictionary<IntPtr, string>();
            _currentProcessInfo = new ProcessInfo(IntPtr.Zero, string.Empty);
        }

        private bool IsMonitoredProcess(string processName) =>
            string.Equals(processName, DEFAULT_PROCESS_NAME, StringComparison.OrdinalIgnoreCase);

        private IProcessInfo GetCurrentProcessInfo()
        {
            using var currentProcess = Process.GetCurrentProcess();
            return new ProcessInfo(currentProcess.MainWindowHandle, currentProcess.MainWindowTitle);
        }

        public IProcessInfo GetMainProcess()
        {
            if (_currentProcessInfo.Handle == IntPtr.Zero)
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
            foreach (var entry in _processCache)
            {
                result.Add(new ProcessInfo(entry.Key, entry.Value));
            }

            return result;
        }

        public void GetUpdatedProcesses(out ICollection<IProcessInfo> addedProcesses, out ICollection<IProcessInfo> updatedProcesses, out ICollection<IProcessInfo> removedProcesses)
        {
            addedProcesses = new List<IProcessInfo>();
            updatedProcesses = new List<IProcessInfo>();
            removedProcesses = new List<IProcessInfo>();

            var knownProcesses = new HashSet<IntPtr>(_processCache.Keys);
            foreach (var process in Process.GetProcesses())
            {
                if (!IsMonitoredProcess(process.ProcessName) || process.MainWindowHandle == IntPtr.Zero)
                {
                    continue;
                }

                var mainWindowHandle = process.MainWindowHandle;
                var mainWindowTitle = process.MainWindowTitle;

                if (_processCache.TryGetValue(mainWindowHandle, out var cachedTitle))
                {
                    if (!string.Equals(cachedTitle, mainWindowTitle))
                    {
                        _processCache[mainWindowHandle] = mainWindowTitle;
                        updatedProcesses.Add(new ProcessInfo(mainWindowHandle, mainWindowTitle));
                    }

                    knownProcesses.Remove(mainWindowHandle);
                }
                else
                {
                    _processCache.TryAdd(mainWindowHandle, mainWindowTitle);
                    addedProcesses.Add(new ProcessInfo(mainWindowHandle, mainWindowTitle));
                }
            }

            foreach (var handle in knownProcesses)
            {
                if (_processCache.TryRemove(handle, out var title))
                {
                    removedProcesses.Add(new ProcessInfo(handle, title));
                }
            }
        }
    }
}