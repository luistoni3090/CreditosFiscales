/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 MouseHook.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// Para el control del puntero del ratón

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Px_Controles.Helpers
{
    /// <summary>
    /// Operaciones globales del ratón
    /// </summary>
    public static class MouseHook
    {
        /// <summary>
        /// wm mousemove
        /// </summary>
        private const int WM_MOUSEMOVE = 0x200;
        /// <summary>
        /// wm lbuttondown
        /// </summary>
        private const int WM_LBUTTONDOWN = 0x201;
        /// <summary>
        /// wm rbuttondown
        /// </summary>
        private const int WM_RBUTTONDOWN = 0x204;
        /// <summary>
        /// wm mbuttondown
        /// </summary>
        private const int WM_MBUTTONDOWN = 0x207;
        /// <summary>
        /// wm lbuttonup
        /// </summary>
        private const int WM_LBUTTONUP = 0x202;
        /// <summary>
        /// wm rbuttonup
        /// </summary>
        private const int WM_RBUTTONUP = 0x205;
        /// <summary>
        /// wm mbuttonup
        /// </summary>
        private const int WM_MBUTTONUP = 0x208;
        /// <summary>
        /// wm lbuttondblclk
        /// </summary>
        private const int WM_LBUTTONDBLCLK = 0x203;
        /// <summary>
        /// wm rbuttondblclk
        /// </summary>
        private const int WM_RBUTTONDBLCLK = 0x206;
        /// <summary>
        /// wm mbuttondblclk
        /// </summary>
        private const int WM_MBUTTONDBLCLK = 0x209;

        /// <summary>
        /// punto
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            /// <summary>
            /// x
            /// </summary>
            public int x;
            /// <summary>
            /// y
            /// </summary>
            public int y;
        }

        /// <summary>
        /// Estructura
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            /// <summary>
            /// pt
            /// </summary>
            public POINT pt;
            /// <summary>
            /// h WND
            /// </summary>
            public int hWnd;
            /// <summary>
            /// w hit test code
            /// </summary>
            public int wHitTestCode;
            /// <summary>
            /// dw extra information
            /// </summary>
            public int dwExtraInfo;
        }


        // Eventos globales del mouse
        /// <summary>
        /// Occurs when [on mouse activity].
        /// </summary>
        public static event MouseEventHandler OnMouseActivity;


        /// <summary>
        /// h mouse hook
        /// </summary>
        private static int _hMouseHook = 0;
        /// <summary>
        /// Enganchamiento global del ratón
        /// </summary>
        /// <exception cref="System.Exception">SetWindowsHookEx failed.</exception>
        /// <exception cref="Exception">SetWindowsHookEx failed.</exception>
        public static void Start()
        {
            if (_hMouseHook != 0)
            {
                Stop();
            }

            WindowsHook.HookMsgChanged += WindowsHook_HookMsgChanged;
            _hMouseHook = WindowsHook.StartHook(HookType.WH_MOUSE_LL);

            if (_hMouseHook == 0)
            {
                Stop();
            }
        }

        static void WindowsHook_HookMsgChanged(string strHookName, int nCode, IntPtr msg, IntPtr lParam)
        {
            // Suponiendo una ejecución normal y el usuario quiere monitorear los mensajes del mouse
            if (nCode >= 0 && OnMouseActivity != null)
            {
                MouseButtons button = MouseButtons.None;
                int clickCount = 0;

                switch ((int)msg)
                {
                    case WM_LBUTTONDOWN:
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case WM_LBUTTONUP:
                        button = MouseButtons.Left;
                        clickCount = 1;
                        break;
                    case WM_LBUTTONDBLCLK:
                        button = MouseButtons.Left;
                        clickCount = 2;
                        break;
                    case WM_RBUTTONDOWN:
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case WM_RBUTTONUP:
                        button = MouseButtons.Right;
                        clickCount = 1;
                        break;
                    case WM_RBUTTONDBLCLK:
                        button = MouseButtons.Right;
                        clickCount = 2;
                        break;
                }
                if (button != MouseButtons.None && clickCount > 0)
                {
                    // Obtener información del mouse desde la función de devolución de llamada
                    MouseHookStruct MyMouseHookStruct = (MouseHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseHookStruct));
                    MouseEventArgs e = new MouseEventArgs(button, clickCount, MyMouseHookStruct.pt.x, MyMouseHookStruct.pt.y, 0);
                    OnMouseActivity(null, e);
                }
            }
        }

        /// <summary>
        /// Deneter evetnos
        /// </summary>
        /// <exception cref="System.Exception">UnhookWindowsHookEx failed.</exception>
        /// <exception cref="Exception">UnhookWindowsHookEx failed.</exception>
        public static void Stop()
        {
            bool retMouse = true;

            if (_hMouseHook != 0)
            {
                retMouse = WindowsHook.StopHook(_hMouseHook);
                _hMouseHook = 0;
            }

            if (!(retMouse))
                throw new Exception("Error al desenganchar WindowsHookEx.");
        }


    }
}
