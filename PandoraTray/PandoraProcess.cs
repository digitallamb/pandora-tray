using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace PandoraTray
{
    public static class PandoraProcess
    {
        #region Win32 Platform Invoke

        /// <summary>
        /// Posts the message.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="Msg">The MSG.</param>
        /// <param name="wParam">The w param.</param>
        /// <param name="lParam">The l param.</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Finds the window.
        /// </summary>
        /// <param name="lpClassName">Name of the lp class.</param>
        /// <param name="lpWindowName">Name of the lp window.</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Shows the window.
        /// </summary>
        /// <param name="hWnd">The h WND.</param>
        /// <param name="nCmdShow">The n CMD show.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Determines whether the specified window is iconic (minimized).
        /// </summary>
        /// <param name="hWnd">The window.</param>
        /// <returns>
        /// 	<c>true</c> if the specified window is iconic; otherwise, <c>false</c>.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);

        // Left mouse button down
        private const uint WM_LBUTTONDOWN = 0x0201;
        // Left mouse button up
        private const uint WM_LBUTTONUP = 0x0202;
        // Key Down
        private const uint WM_KEYDOWN = 0x0100;
        // Key Up
        private const uint WM_KEYUP = 0x0101;
        // Restore window
        private const int SW_RESTORE = 9;

        /// <summary>
        /// Makes a single parameter from two integers
        /// </summary>
        /// <param name="LoWord">The lo word.</param>
        /// <param name="HiWord">The hi word.</param>
        /// <returns></returns>
        private static IntPtr MakeLParam(int LoWord, int HiWord)
        {
            return (IntPtr)((HiWord << 16) | (LoWord & 0xffff));
        }

        #endregion

        private const string PANDORA_CLASS_NAME = "ApolloRuntimeContentWindow";
        private const string PANDORA_WINDOW_NAME = "Pandora";

        /// <summary>
        /// Determines whether Pandora is running.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if this instance is running; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsRunning()
        {
            bool pandoraRunning = false;

            if (GetPointer() != IntPtr.Zero) { pandoraRunning = true; }

            return pandoraRunning;
        }

        /// <summary>
        /// Sends the key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="shiftEnabled">if set to <c>true</c> [shift enabled].</param>
        public static void SendKey(Keys key, bool shiftEnabled = false)
        {
            RestoreWindow();
            IntPtr hWnd = GetPointer();

            // Post the KeyDown events
            PostMessage(hWnd, WM_KEYDOWN, (IntPtr)key, IntPtr.Zero);
            if (shiftEnabled) { PostMessage(hWnd, WM_KEYDOWN, (IntPtr)Keys.Shift, IntPtr.Zero); };

            // Post the KeyUp events
            PostMessage(hWnd, WM_KEYUP, (IntPtr)key, IntPtr.Zero);
            if (shiftEnabled) { PostMessage(hWnd, WM_KEYUP, (IntPtr)Keys.Shift, IntPtr.Zero); };
        }

        /// <summary>
        /// Gets the pandora pointer
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetPointer()
        {
            return FindWindow(PANDORA_CLASS_NAME, PANDORA_WINDOW_NAME);
        }

        /// <summary>
        /// Restores the window.
        /// </summary>
        private static void RestoreWindow()
        {
            IntPtr hWnd = GetPointer();

            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SW_RESTORE);
                int cTimeout = 0;
                do
                {
                    Thread.Sleep(100);
                    cTimeout++;
                    if (cTimeout > 10) break; // 1sec max
                } while (IsIconic(hWnd));
            }
        }
    }
}
