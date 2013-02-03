using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PandoraOneMediaKeys;
using System.Drawing;

namespace PandoraTray
{
    class SystemTrayApplicationContext : ApplicationContext
    {
        private NotifyIcon _NotifyIcon;
        private System.ComponentModel.Container _Components;
        private readonly UserActivityHook _UserActivityHook = new UserActivityHook(false, true);

        /// <summary>
        /// Initializes a new instance of the <see cref="SystemTrayApplicationContext"/> class.
        /// </summary>
        public SystemTrayApplicationContext()
        {
            InitializeContext();
            InitializeHooks();
            BuildContextMenu();
        }

        /// <summary>
        /// Builds the context menu.
        /// </summary>
        private void BuildContextMenu()
        {
            _NotifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("&Exit", null, exitItem_Click));
        }

        /// <summary>
        /// Initializes the hooks.
        /// </summary>
        private void InitializeHooks()
        {
            _UserActivityHook.KeyDown += new UserActivityHook.KeyInfoEventHandler(UserActivityHook_KeyDown);
        }

        /// <summary>
        /// Handles the user activity key down event
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="KeyCode">The key code.</param>
        /// <param name="ctrl">if set to <c>true</c> [CTRL].</param>
        private void UserActivityHook_KeyDown(object sender, Keys KeyCode, bool ctrl)
        {
            switch (KeyCode)
            {
                case Keys.MediaNextTrack:
                    PandoraController.Next();
                    break;
                case Keys.MediaPlayPause:
                    PandoraController.PlayPause();
                    break;
                case Keys.MediaPreviousTrack:
                    break;
                case Keys.MediaStop:
                    break;
            }
        }

        /// <summary>
        /// Initializes the context.
        /// </summary>
        private void InitializeContext()
        {
            string DefaultTooltip = "Pandora Tray";

            _Components = new System.ComponentModel.Container();
            _NotifyIcon = new NotifyIcon(_Components)
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = Properties.Resources.Metro_Keyboard_Blue,
                BalloonTipIcon = ToolTipIcon.Info,
                BalloonTipText = "Control PandoraOne with the media keys on your keyboard!",
                BalloonTipTitle = DefaultTooltip,
                Text = DefaultTooltip,
                
                Visible = true
            };

            _NotifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
            _NotifyIcon.ShowBalloonTip(1000);
        }

        /// <summary>
        /// Handles the DoubleClick event of the notifyIcon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            // Attempt to launch PandoraOne if it's not already running, otherwise show it
            if (!PandoraProcess.IsRunning())
            {
                // TODO: Go forth and find PandoraOne and launch it
            }
            else
            {

            }
        }

        /// <summary>
        /// Handles the Click event of the exitItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void exitItem_Click(object sender, EventArgs e)
        {
            ExitThread();
        }

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ApplicationContext"/> and optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && _Components != null) { _Components.Dispose(); }
        }

        /// <summary>
        /// Terminates the message loop of the thread.
        /// </summary>
        protected override void ExitThreadCore()
        {
            _NotifyIcon.Visible = false; // should remove lingering tray icon!
            base.ExitThreadCore();
        }
    }
}