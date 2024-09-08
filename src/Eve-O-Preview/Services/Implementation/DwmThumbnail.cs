using System;
using System.Runtime.InteropServices;
using EveOPreview.Core.Internal.Interop.Windows;
using EveOPreview.Services.Interface;
using EveOPreview.Services.Interop;

namespace EveOPreview.Services.Implementation
{
    internal sealed class DwmThumbnail : IDwmThumbnail
    {
        #region Private fields

        private readonly IWindowManager _windowManager;
        private IntPtr _handle;
        private DWM_THUMBNAIL_PROPERTIES _properties;

        #endregion

        public DwmThumbnail(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _handle = IntPtr.Zero;
        }

        public void Register(IntPtr destination, IntPtr source)
        {
            _properties = new DWM_THUMBNAIL_PROPERTIES();
            _properties.dwFlags = DWM_TNP_CONSTANTS.DWM_TNP_VISIBLE
                                       + DWM_TNP_CONSTANTS.DWM_TNP_OPACITY
                                       + DWM_TNP_CONSTANTS.DWM_TNP_RECTDESTINATION
                                       + DWM_TNP_CONSTANTS.DWM_TNP_SOURCECLIENTAREAONLY;
            _properties.opacity = 255;
            _properties.fVisible = true;
            _properties.fSourceClientAreaOnly = true;

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
                // This exception is raised if the source client is already closed
                // Can happen on a really slow CPU's that the window is still being
                // listed in the process list yet it already cannot be used as
                // a thumbnail source
                _handle = IntPtr.Zero;
            }
            catch (COMException)
            {
                // This exception is raised if DWM is suddenly not available
                // (f.e. when switching between Windows user accounts)
                _handle = IntPtr.Zero;
            }
        }

        public void Unregister()
        {
            if ((!_windowManager.IsCompositionEnabled) || (_handle == IntPtr.Zero))
            {
                return;
            }

            try
            {
                DWM_NATIVE_METHODS.DwmUnregisterThumbnail(_handle);
            }
            catch (ArgumentException)
            {
            }
            catch (COMException)
            {
                // This exception is raised when DWM is not available for some reason
            }
        }

        public void Move(int left, int top, int right, int bottom)
        {
            _properties.rcDestination = new RECT(left, top, right, bottom);
        }

        public void Update()
        {
            if ((!_windowManager.IsCompositionEnabled) || (_handle == IntPtr.Zero))
            {
                return;
            }

            try
            {
                DWM_NATIVE_METHODS.DwmUpdateThumbnailProperties(_handle, _properties);
            }
            catch (ArgumentException)
            {
                // This exception will be thrown if the EVE client disappears while this method is running
            }
            catch (COMException)
            {
                // This exception is raised when DWM is not available for some reason
            }
        }
    }
}