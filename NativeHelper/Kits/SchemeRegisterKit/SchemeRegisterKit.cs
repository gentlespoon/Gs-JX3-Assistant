using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace GsJX3AssistantNativeHelper.Kits
{
    public class SchemeRegisterKit
    {
        private LoggingKit _loggingKit;

        public static string selfPath = "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"";
                
        public static string protocol = "com.gentlespoon.jx3.nh";

        public SchemeRegisterKit(LoggingKit loggingKit)
        {
            _loggingKit = loggingKit;
        }

        public void registerSchemeHandler()
        {
            _loggingKit.Info("Registering scheme handler: com.gentlespoon.jx3.nh");
            try
            {

                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH;
                HKCR_GS3JX3NH = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(protocol);
                HKCR_GS3JX3NH.SetValue("", "URL:com.gentlespoon.jx3assistant.nativehelper");
                HKCR_GS3JX3NH.SetValue("URL Protocol", "");


                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH_DEFAULTICON;
                HKCR_GS3JX3NH_DEFAULTICON = HKCR_GS3JX3NH.CreateSubKey("DefaultIcon");
                HKCR_GS3JX3NH_DEFAULTICON.SetValue("", selfPath + ",1");


                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH_SHELL;
                HKCR_GS3JX3NH_SHELL = HKCR_GS3JX3NH.CreateSubKey("shell");

                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH_SHELL_OPEN;
                HKCR_GS3JX3NH_SHELL_OPEN = HKCR_GS3JX3NH_SHELL.CreateSubKey("open");

                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH_SHELL_OPEN_COMMAND;
                HKCR_GS3JX3NH_SHELL_OPEN_COMMAND = HKCR_GS3JX3NH_SHELL_OPEN.CreateSubKey("command");
                HKCR_GS3JX3NH_SHELL_OPEN_COMMAND.SetValue("", selfPath + " %1");

            }
            catch (Exception ex)
            {
                _loggingKit.Error(ex.ToString());
            }
                
        }
    }
}
