using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using WindowsFirewallHelper;
using WindowsFirewallHelper.FirewallAPIv1;

namespace GsJX3AssistantNativeHelper.Kits.General
{
    public class EnvironmentSetupKit
    {
        private ILogger log;
                
        public string protocolName = "com.gentlespoon.jx3.nh";
        public string firewallRuleIdentifier = "GsJX3NH_TemporaryRules";

        public EnvironmentSetupKit(ILogger logger)
        {
            log = logger;
        }

        public void RegisterSchemeHandler()
        {
            log.Information("[EnvironmentSetupKit] Registering scheme handler: com.gentlespoon.jx3.nh");
            try
            {

                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH;
                HKCR_GS3JX3NH = Microsoft.Win32.Registry.ClassesRoot.CreateSubKey(protocolName);
                HKCR_GS3JX3NH.SetValue("", "URL:com.gentlespoon.jx3assistant.nativehelper");
                HKCR_GS3JX3NH.SetValue("URL Protocol", "");


                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH_DEFAULTICON;
                HKCR_GS3JX3NH_DEFAULTICON = HKCR_GS3JX3NH.CreateSubKey("DefaultIcon");
                HKCR_GS3JX3NH_DEFAULTICON.SetValue("\"", AppPaths.path_AppPath + "\",1");


                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH_SHELL;
                HKCR_GS3JX3NH_SHELL = HKCR_GS3JX3NH.CreateSubKey("shell");

                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH_SHELL_OPEN;
                HKCR_GS3JX3NH_SHELL_OPEN = HKCR_GS3JX3NH_SHELL.CreateSubKey("open");

                Microsoft.Win32.RegistryKey HKCR_GS3JX3NH_SHELL_OPEN_COMMAND;
                HKCR_GS3JX3NH_SHELL_OPEN_COMMAND = HKCR_GS3JX3NH_SHELL_OPEN.CreateSubKey("command");
                HKCR_GS3JX3NH_SHELL_OPEN_COMMAND.SetValue("\"", AppPaths.path_AppPath + "\" %1");

            }
            catch (Exception ex)
            {
                log.Error(ex.ToString());
            }
                
        }

        private bool CheckFirewallSupport()
        {
            if (!FirewallManager.Instance.IsSupported)
            {
                log.Warning("[EnvironmentSetupKit] Firewall is not supported on this system.");
                return false;
            }
            return true;
        }

        public void SetupFirewallPortRule(ushort port)
        {
            RemoveFirewallPortRule();

            log.Information("[EnvironmentSetupKit] Adding firewall rule");
            var inbound = FirewallManager.Instance.CreatePortRule(
                FirewallProfiles.Public | FirewallProfiles.Private,
                firewallRuleIdentifier,
                port
            );
            inbound.Direction = FirewallDirection.Inbound;
            FirewallManager.Instance.Rules.Add(inbound);
        }

        public void RemoveFirewallPortRule()
        {
            if (!CheckFirewallSupport())
            {
                return;
            }

            log.Information("[EnvironmentSetupKit] Removing all previous rules.");
            var existingRules = FirewallManager.Instance.Rules.SingleOrDefault(r => r.Name == firewallRuleIdentifier);
            if (existingRules != null)
            {
                FirewallManager.Instance.Rules.Remove(existingRules);
            }
        }
    }
}
