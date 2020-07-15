using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GsJX3AssistantNativeHelper.Kits
{
    public class NotifyIconKit
    {

        private System.Drawing.Icon iconNormal;
        private System.Drawing.Icon iconWarning;
        private System.Drawing.Icon iconDanger;

        private System.Windows.Forms.NotifyIcon nIcon;

        public NotifyIconKit()
        {

            // Create notify icon
            nIcon = new System.Windows.Forms.NotifyIcon();

            // Create icons

            // icon - normal
            using FileStream fsNormal = new FileStream(Path.Combine(AppPaths.path_AppDirectory, "Icons", "icon.ico"), FileMode.Open);
            iconNormal = new System.Drawing.Icon(fsNormal);

            // icon - warning
            using FileStream fsWarning = new FileStream(Path.Combine(AppPaths.path_AppDirectory, "Icons", "icon-warning.ico"), FileMode.Open);
            iconWarning = new System.Drawing.Icon(fsWarning);

            // icon - danger
            using FileStream fsDanger = new FileStream(Path.Combine(AppPaths.path_AppDirectory, "Icons", "icon-danger.ico"), FileMode.Open);
            iconDanger = new System.Drawing.Icon(fsDanger);
        }

        

        public void Normal()
        {
            nIcon.Icon = iconNormal;
            nIcon.Visible = true;
        }

        public void Warning()
        {
            nIcon.Icon = iconWarning;
            nIcon.Visible = true;
        }

        public void Danger()
        {
            nIcon.Icon = iconDanger;
            nIcon.Visible = true;
        }

        public void Hide()
        {
            nIcon.Visible = false;
        }
    }
}
