using System;
using System.Runtime.InteropServices;

namespace EveOPreview.Hotkey
{
    internal static class HotkeyHandlerNativeMethods
    {
        // Hotkey Modifiers
        public const uint MOD_ALT = 0x1;
        public const uint MOD_CONTROL = 0x2;
        public const uint MOD_SHIFT = 0x4;
        public const uint MOD_WIN = 0x8;

        // Windows Message for Hotkey
        public const uint WM_HOTKEY = 0x0312;
        
        // Error code for already registered hotkeys
        public const uint ERROR_HOTKEY_ALREADY_REGISTERED = 1409;

        /// <summary>
        /// Registers a system-wide hotkey.
        /// </summary>
        /// <param name="hWnd">Handle to the window that will receive WM_HOTKEY messages.</param>
        /// <param name="id">ID of the hotkey.</param>
        /// <param name="modifiers">Modifier keys (ALT, CONTROL, SHIFT, WIN).</param>
        /// <param name="vk">Virtual-key code for the hotkey.</param>
        /// <returns>True if registration succeeded, otherwise false.</returns>
        public static bool RegisterHotKey(nint hWnd, int id, uint modifiers, uint vk)
        {
            return NativeMethods.RegisterHotKey(hWnd, id, modifiers, vk);
        }

        /// <summary>
        /// Unregisters a system-wide hotkey.
        /// </summary>
        /// <param name="hWnd">Handle to the window that registered the hotkey.</param>
        /// <param name="id">ID of the hotkey to unregister.</param>
        /// <returns>True if unregistration succeeded, otherwise false.</returns>
        public static bool UnregisterHotKey(nint hWnd, int id)
        {
            return NativeMethods.UnregisterHotKey(hWnd, id);
        }

        // Internal class to manage PInvoke calls to the user32.dll
        private static class NativeMethods
        {
            // Registers a system-wide hotkey (Windows-specific)
            [DllImport("user32.dll")]
            public static extern bool RegisterHotKey(nint hWnd, int id, uint fsModifiers, uint vk);

            // Unregisters a system-wide hotkey (Windows-specific)
            [DllImport("user32.dll")]
            public static extern bool UnregisterHotKey(nint hWnd, int id);
        }
    }
}