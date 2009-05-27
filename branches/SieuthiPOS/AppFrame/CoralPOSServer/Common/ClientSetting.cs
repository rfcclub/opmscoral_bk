using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOSServer.Properties;
using AFCSetting = CoralPOSServer.Properties.Settings;

namespace CoralPOS.Common
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
                if (clientSetting == null)
                {
                    clientSetting = new ClientSetting();
                }
                return clientSetting;
            }
        }

        public bool IsClient()
        {
            return string.IsNullOrEmpty(AFCSetting.Default.IsClient)
                       ? false
                       : Settings.Default.IsClient.Equals("1");           
        }

        public bool IsServer()
        {
            return string.IsNullOrEmpty(AFCSetting.Default.IsClient)
                       ? false
                       : Settings.Default.IsClient.Equals("0");
        }
        public void Save()
        {
            AFCSetting.Default.Save();
            Settings.Default.Upgrade();
        }
        public void Reload()
        {
            AFCSetting.Default.Reload();
        }

        public string SyncImportPath
        {
            get
            {
                return AFCSetting.Default.SyncImportPath;
            }
            set
            {
                AFCSetting.Default.SyncImportPath = value;
            }
        }
        public string SyncSuccessPath
        {
            get
            {
                return AFCSetting.Default.SyncSuccessPath;
            }
            set
            {
                AFCSetting.Default.SyncSuccessPath = value;
            }
        }
        public string SyncErrorPath
        {
            get
            {
                return AFCSetting.Default.SyncErrorPath;
            }
            set
            {
                AFCSetting.Default.SyncErrorPath = value;
            }
        }
        public string SyncExportPath
        {
            get
            {
                return AFCSetting.Default.SyncExportPath;
            }
            set
            {
                AFCSetting.Default.SyncExportPath = value;
            }
        }
        public string PrinterName
        {
            get
            {
                return AFCSetting.Default.PrinterName;
            }
            set
            {
                AFCSetting.Default.PrinterName = value;
            }
        }
        public string MySQLDumpPath
        {
            get
            {
                return AFCSetting.Default.MySQLDump;
            }
            set
            {
                AFCSetting.Default.MySQLDump = value;                
            }
        }
        public string DBBackupPath
        {
            get
            {
                return AFCSetting.Default.DBBackupPath;
            }
            set
            {
                AFCSetting.Default.DBBackupPath = value;
            }
        }

        public void Reset()
        {
            AFCSetting.Default.Reset();
        }
    }
}
