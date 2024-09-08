using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace EveOPreview.Hotkey
{
    public class HotkeyHandler : IMessageFilter, IDisposable
    {
        private static int _currentId;
        private const int MAX_ID = 0xBFFF;

        private readonly int _hotkeyId;
        private readonly IntPtr _hotkeyTarget;

        public event HandledEventHandler Pressed;
        
        public bool IsRegistered { get; private set; }
        public Keys KeyCode { get; private set; }

        public HotkeyHandler(IntPtr target, Keys hotkey)
        {
            if (hotkey == Keys.None)
                throw new ArgumentException("Error: Trying to use empty configuration for key.", nameof(hotkey));
            
            _hotkeyId = _currentId;
            _currentId = (_currentId + 1) & MAX_ID;
            _hotkeyTarget = target;

            KeyCode = hotkey;
        }
        
        private bool OnPressed()
        {
            // Fire the event if we can and return whether it was handled
            var args = new HandledEventArgs(false);
            Pressed?.Invoke(this, args);
            return args.Handled;
        }
        
        public bool CanRegister()
        {
            // Attempt to register and immediately unregister to test if the hotkey can be used
            if (Register())
            {
                Unregister();
                return true;
            }
            
            return false;
        }

        public bool Register()
        {
            if (IsRegistered || KeyCode == Keys.None)
                return false;

            // Extract key and modifiers
            var (key, modifiers) = GetKeyAndModifiers(KeyCode);

            if (!HotkeyHandlerNativeMethods.RegisterHotKey(_hotkeyTarget, _hotkeyId, modifiers, key))
                return false;

            Application.AddMessageFilter(this);
            IsRegistered = true;

            return true;
        }

        public void Unregister()
        {
            if (!IsRegistered) return;

            Application.RemoveMessageFilter(this);
            HotkeyHandlerNativeMethods.UnregisterHotKey(_hotkeyTarget, _hotkeyId);
            IsRegistered = false;
        }
        
        private static (uint key, uint modifiers) GetKeyAndModifiers(Keys keyCode)
        {
            uint key = (uint)keyCode & ~(uint)(Keys.Alt | Keys.Control | Keys.Shift);
            uint modifiers = (keyCode.HasFlag(Keys.Alt) ? HotkeyHandlerNativeMethods.MOD_ALT : 0)
                             | (keyCode.HasFlag(Keys.Control) ? HotkeyHandlerNativeMethods.MOD_CONTROL : 0)
                             | (keyCode.HasFlag(Keys.Shift) ? HotkeyHandlerNativeMethods.MOD_SHIFT : 0);
            return (key, modifiers);
        }

        #region IMessageFilter

        public bool PreFilterMessage(ref Message message)
        {
            return IsRegistered
                   && message.Msg == HotkeyHandlerNativeMethods.WM_HOTKEY
                   && message.WParam.ToInt32() == _hotkeyId
                   && OnPressed();
        }

        #endregion
        
        public void Dispose()
        {
            Unregister();
            GC.SuppressFinalize(this);
        }

        ~HotkeyHandler()
        {
            Dispose();
        }
    }
}