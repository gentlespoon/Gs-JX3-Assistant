using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using GsJX3AssistantNativeHelper.Kits.ConnectionKit;
using Serilog;

namespace GsJX3AssistantNativeHelper.Kits.UIKit
{
    public class NotifyIconKit: IHttpPortChangedObserver
    {
        private ILogger log;
        private Icon icon;

        private NotifyIcon nIcon;
        private ContextMenu contextMenu;
        private UILauncher uiLauncher;

        private int httpPort = 0;

        public NotifyIconKit(ILogger log)
        {
            this.log = log;
            // Create notify icon
            nIcon = new NotifyIcon();

            // Context menu
            contextMenu = new ContextMenu();
            nIcon.BalloonTipClicked += ShowUI;
            nIcon.ContextMenu = contextMenu;
            SetupContextMenu();

            // Create icon
            using FileStream fsIcon = new FileStream(Path.Combine(AppPaths.path_AppDirectory, "Icons", "icon.ico"), FileMode.Open);
            icon = new Icon(fsIcon);

            Show();
        }

        public void RegisterUILauncher(UILauncher uiLauncher)
        {
            this.uiLauncher = uiLauncher;
        }

        private void SetupContextMenu()
        {
            contextMenu.MenuItems.Clear();

            var mi_Title = new MenuItem();
            mi_Title.Text = "GsJX3大助手 - 显示界面";
            mi_Title.Click += ShowUI;
            contextMenu.MenuItems.Add(mi_Title);

            contextMenu.MenuItems.Add("-");

            var mi_CurrentPort = new MenuItem();
            mi_CurrentPort.Text = $"当前监听端口: {httpPort} - 点击复制";
            mi_CurrentPort.Click += (object sender, EventArgs e) =>
            {
                Clipboard.SetText(httpPort.ToString());
            };
            contextMenu.MenuItems.Add(mi_CurrentPort);

            contextMenu.MenuItems.Add("-");

            // Exit
            var mi_Exit = new MenuItem();
            mi_Exit.Text = "退出";
            mi_Exit.Click += ExitClicked;
            contextMenu.MenuItems.Add(mi_Exit);
        }

        public void UpdateIconTitle(string message = "")
        {
            string title = "GsJX3大助手\n";
            title += $"连接端口 - {httpPort}\n";
            if (message != "") title += "\n" + message;
            nIcon.Text = title;
        }

        public void ShowLaunchUIPrompt()
        {
            ShowBalloonTip(5, "GsJX3大助手", $"正在监听端口 {httpPort} \n端口号已复制到剪贴板", System.Windows.Forms.ToolTipIcon.Info);
        }

        public void ShowBalloonTip(int timeout, string tipTitle, string tipText, ToolTipIcon tipIcon)
        {
            nIcon.ShowBalloonTip(timeout, tipTitle, tipText, tipIcon);
        }

        public void Show()
        {
            nIcon.Icon = icon;
            nIcon.Visible = true;
        }

        public void Hide()
        {
            nIcon.Visible = false;
        }


        

        // Event handlers
        public void ShowUI(object sender, EventArgs e)
        {
            if (uiLauncher == null)
            {
                log.Warning("[NotifyIconKit] Failed to launch UI: UILauncher is not registered");
                return;
            }
            uiLauncher.LaunchWebUI();
        }

        public void ExitClicked(object sender, EventArgs e)
        {
            (App.Current as App).Terminate();
        }

        public void HttpPortChanged(int port)
        {
            log.Debug("[NotifyIconKit] Changing port to {0}", port);
            httpPort = port;
            SetupContextMenu();
        }
    }
}
