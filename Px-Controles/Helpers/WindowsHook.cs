/// 	 Wero MX
/// Autor: 	 Marco Antonio Acuña Rosas
/// Nombre: 	 HookType.cs
/// Creación: 	 2024.07.12
/// Ult Mod: 	 2024.07.12
/// Descripción:
/// tipos de anclajes

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Px_Controles.Helpers
{
    /// <summary>
    /// Tipos de anclajes
    /// </summary>
    public enum HookType : int
    {
        /// <summary>
        /// Instale un procedimiento de enlace que monitorea los mensajes generados como resultado de eventos de entrada en cuadros de diálogo, cuadros de mensajes, menús o barras de desplazamiento.
        /// Para obtener más información, consulte el proceso de enlace MessageProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644987(v=vs.85)).
        /// </summary>
        WH_MSGFILTER = -1,
        /// <summary>
        /// Instale un proceso de enlace que registre los mensajes de entrada publicados en la cola de mensajes del sistema. Este enlace es útil para grabar macros.
        /// Para obtener más información, consulte el proceso de enlace JournalRecordProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644983(v=vs.85)).
        /// </summary>
        WH_JOURNALRECORD = 0,
        /// <summary>
        /// Instalar un procedimiento de enlace que publica mensajes previamente grabados por el procedimiento de enlace WH_JOURNALRECORD.
        /// Para obtener más información, consulte el proceso de enlace JournalPlaybackProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644982(v=vs.85)).
        /// </summary>
        WH_JOURNALPLAYBACK = 1,
        /// <summary>
        /// Instalar un programa de enlace que monitorea los mensajes de pulsación de teclas.
        /// Consulte el proceso de enlace KeyboardProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644984(v=vs.85)) para obtener más información.
        /// </summary>
        WH_KEYBOARD = 2,
        /// <summary>
        /// Instalar un procedimiento de enlace para monitorear los mensajes publicados en la cola de mensajes.
        /// Para obtener más información, consulte el proceso de enlace GetMsgProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644981(v=vs.85)).
        /// </summary>
        WH_GETMESSAGE = 3,
        /// <summary>
        /// Instale un procedimiento de enlace que monitorea los mensajes antes de que el sistema los envíe al procedimiento de la ventana de destino.
        /// Para obtener más información, consulte el proceso de enlace CallWndProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644975(v=vs.85)).
        /// </summary>
        WH_CALLWNDPROC = 4,
        /// <summary>
        /// Instale un programa de enlace para recibir notificaciones útiles para aplicaciones CBT.
        /// Para obtener más información, consulte el proceso de enlace CBTProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644977(v=vs.85)).
        /// </summary>
        WH_CBT = 5,
        /// <summary>
        /// Instale un procedimiento de enlace que monitorea los mensajes generados como resultado de eventos de entrada en cuadros de diálogo, cuadros de mensajes, menús o barras de desplazamiento.
        /// El procedimiento de enlace monitorea todas las aplicaciones en el mismo escritorio que el hilo de llamada para estos mensajes.
        /// Para obtener más información, consulte el proceso de enlace SysMsgProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644992(v=vs.85)).
        /// </summary>
        WH_SYSMSGFILTER = 6,
        /// <summary>
        /// Instalar el proceso de enlace para monitorear los mensajes del mouse.
        /// Para obtener más información, consulte el proceso de enlace de MouseProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644988(v=vs.85)).
        /// </summary>
        WH_MOUSE = 7,
        /// <summary>
        /// Instalar un proceso de enlace para depurar otros procesos de enlace.
        /// Para obtener más información, consulte el proceso de enlace DebugProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644978(v=vs.85)).
        /// </summary>
        WH_DEBUG = 9,
        /// <summary>
        /// Instalar un proceso de enlace para recibir notificaciones útiles para las aplicaciones de shell.
        /// Consulte ShellProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644991(v=vs.85)) para obtener más información.
        /// </summary>
        WH_SHELL = 10,
        /// <summary>
        /// Instale un procedimiento de enlace que se llamará cuando el hilo de primer plano de la aplicación esté a punto de quedar inactivo.
        /// Este enlace es útil para ejecutar tareas de baja prioridad cuando está inactivo.
        /// Para obtener más información, consulte el proceso de enlace ForegroundIdleProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644980(v=vs.85)).
        /// </summary>
        WH_FOREGROUNDIDLE = 11,
        /// <summary>
        /// Instale un procedimiento de enlace que monitorea el procedimiento de la ventana de destino después de haber procesado el mensaje.
        /// Para obtener más información, consulte el procedimiento de enlace CallWndRetProc (https://docs.microsoft.com/windows/desktop/api/winuser/nc-winuser-hookproc).
        /// </summary>
        WH_CALLWNDPROCRET = 12,
        /// <summary>
        /// Instalar un procedimiento de enlace que monitorea eventos de entrada de teclado de bajo nivel. Para obtener más información,
        /// Consulte el proceso de enlace LowLevelKeyboardProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644985(v=vs.85)).
        /// </summary>
        WH_KEYBOARD_LL = 13,
        /// <summary>
        /// Instalar un procedimiento de enlace que monitorea eventos de entrada del mouse de bajo nivel. Para obtener más información,
        /// Consulte el proceso de enlace LowLevelMouseProc (https://docs.microsoft.com/previous-versions/windows/desktop/legacy/ms644986(v=vs.85)).
        /// </summary>
        WH_MOUSE_LL = 14,
    }
    public class WindowsHook
    {
        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);
        // Función de gancho del dispositivo
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, int hInstance, int threadId);

        // Función de desenganchar
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        // La siguiente función enganchada
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        /// <summary>
        /// Delegate HookMsgHandler
        /// </summary>
        /// <param name="strHookName">nombre del gancho</param>
        /// <param name="msg">valor del mensaje</param>
        public delegate void HookMsgHandler(string strHookName, int nCode, IntPtr msg, IntPtr lParam);
        /// <summary>
        /// evento de mensaje de gancho
        /// </summary>
        public static event HookMsgHandler HookMsgChanged;
        /// <summary>
        /// iniciar un gancho
        /// </summary>
        /// <param name="hookType">tipo de gancho</param>
        /// <param name="wParam">Identificador del módulo, si está vacío, es el módulo actual</param>
        /// <param name="pid">Identificador del proceso, el valor predeterminado es 0, lo que significa el proceso actual</param>
        /// <param name="strHookName">nombre del gancho</param>
        /// <returns>Mango de gancho (requerido al consumir el gancho)</returns>
        /// <exception cref="Exception">SetWindowsHookEx failed.</exception>
        public static int StartHook(HookType hookType, int wParam = 0, int pid = 0, string strHookName = "")
        {
            int _hHook = 0;
            // Genere una instancia de HookProc.
            var _hookProcedure = new HookProc((nCode, msg, lParam) =>
            {

                if (HookMsgChanged != null)
                {
                    try
                    {
                        HookMsgChanged(strHookName, nCode, msg, lParam);
                    }
                    catch { }
                }

                int inext = CallNextHookEx(_hHook, nCode, msg, lParam);
                return inext;
            });
            if (pid == 0)
                pid = AppDomain.GetCurrentThreadId();
            _hHook = SetWindowsHookEx((int)hookType, _hookProcedure, wParam, pid);

            //Detener el gancho suponiendo que el dispositivo falle
            if (_hHook == 0)
            {
                StopHook(_hHook);
            }
            return _hHook;
        }

        /// <summary>
        /// gancho de parada
        /// </summary>
        /// <param name="_hHook">El identificador de gancho devuelto por la función StartHook</param>
        /// <returns><c>true</c> si la detención es exitosa, <c>false</c> en caso contrario.</returns>
        public static bool StopHook(int _hHook)
        {
            bool ret = true;

            if (_hHook != 0)
            {
                ret = UnhookWindowsHookEx(_hHook);
            }

            // Supongamos que falla el desenganche
            if (!ret)
                return false;
            return true;
        }
    }
}
