using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOS.Common;
using CoralPOSClient.Properties;

namespace CoralPOSClient.Common
{
    public class ClientSetting : IBaseClientSetting
    {
        private static ClientSetting clientSetting = null;
        private ClientSetting()
        {
            
        }
        public static ClientSetting Instance
        {
            get
            {
                if(clientSetting == null)
                {
                    clientSetting = new ClientSetting();
                }
                return clientSetting;
            }
        }
        public bool IsClient()
        {
            return string.IsNullOrEmpty(Settings.Default.IsClient)
                       ? false
                       : Settings.Default.IsClient.Equals("1");           
        }

        public bool IsServer()
        {
            return string.IsNullOrEmpty(Settings.Default.IsClient)
                       ? false
                       : Settings.Default.IsClient.Equals("0");
        }
        public void Save()
        {
            Settings.Default.Save();
            Settings.Default.Upgrade();
        }
        public void Reload()
        {
            Settings.Default.Reload();
        }

        public string SyncImportPath
        {
            get
            {
                return Settings.Default.SyncImportPath;
            }
            set
            {
                Settings.Default.SyncImportPath = value;
            }
        }
        public string SyncSuccessPath
        {
            get
            {
                return Settings.Default.SyncSuccessPath;
            }
            set
            {
                Settings.Default.SyncSuccessPath = value;
            }
        }
        public string SyncErrorPath
        {
            get
            {
                return Settings.Default.SyncErrorPath;
            }
            set
            {
                Settings.Default.SyncErrorPath = value;
            }
        }
        public string SyncExportPath
        {
            get
            {
                return Settings.Default.SyncExportPath;
            }
            set
            {
                Settings.Default.SyncExportPath = value;
            }
        }
        public string PrinterName
        {
            get
            {
                return Settings.Default.PrinterName;
            }
            set
            {
                Settings.Default.PrinterName = value;
            }
        }
        public string MySQLDumpPath
        {
            get
            {
                return Settings.Default.MySQLDump;
            }
            set
            {
                Settings.Default.MySQLDump = value;                
            }
        }
        public string DBBackupPath
        {
            get
            {
                return Settings.Default.DBBackupPath;
            }
            set
            {
                Settings.Default.DBBackupPath = value;
            }
        }

        public void Reset()
        {
            Settings.Default.Reset();
        }
    }
}