using System;
using System.Runtime.InteropServices;
using EveOPreview.Core.Internal.Interop.Windows;
using EveOPreview.Services.Interface;
using EveOPreview.Services.Interop;

namespace EveOPreview.Services.Implementation;

internal sealed class DwmThumbnail : IDwmThumbnail
{
    private readonly IWindowManager _windowManager;
    private nint _handle;
    private readonly DWM_THUMBNAIL_PROPERTIES _properties;

    public DwmThumbnail(IWindowManager windowManager)
    {
        _windowManager = windowManager ?? throw new ArgumentNullException(nameof(windowManager));
        _handle = nint.Zero;
        _properties = new DWM_THUMBNAIL_PROPERTIES
        {
            dwFlags = DWM_TNP_CONSTANTS.DWM_TNP_VISIBLE
                      | DWM_TNP_CONSTANTS.DWM_TNP_OPACITY
                      | DWM_TNP_CONSTANTS.DWM_TNP_RECTDESTINATION
                      | DWM_TNP_CONSTANTS.DWM_TNP_SOURCECLIENTAREAONLY,
            opacity = 255,
            fVisible = true,
            fSourceClientAreaOnly = true
        };
    }

    public void Register(nint destination, nint source)
    {
        if (!_windowManager.IsCompositionEnabled)
        {
            return;
        }

        try
        {
            _handle = DWM_NATIVE_METHODS.DwmRegisterThumbnail(destination, source);
        }
        catch (ArgumentException)
        {
            // Handle case when source client is already closed
            _handle = nint.Zero;
        }
        catch (COMException)
        {
            // Handle case when DWM becomes unavailable
            _handle = nint.Zero;
        }
    }

    public void Unregister()
    {
        if (!_windowManager.IsCompositionEnabled || _handle == nint.Zero)
        {
            return;
        }

        try
        {
            DWM_NATIVE_METHODS.DwmUnregisterThumbnail(_handle);
        }
        catch (ArgumentException)
        {
            // Handle ArgumentException, but no need to rethrow
        }
        catch (COMException)
        {
            // Handle COMException when DWM becomes unavailable
        }
    }

    public void Move(int left, int top, int right, int bottom)
    {
        _properties.rcDestination = new RECT(left, top, right, bottom);
    }

    public void Update()
    {
        if (!_windowManager.IsCompositionEnabled || _handle == nint.Zero)
        {
            return;
        }

        try
        {
            DWM_NATIVE_METHODS.DwmUpdateThumbnailProperties(_handle, _properties);
        }
        catch (ArgumentException)
        {
            // Handle case when the EVE client disappears while this method runs
        }
        catch (COMException)
        {
            // Handle case when DWM becomes unavailable
        }
    }
}