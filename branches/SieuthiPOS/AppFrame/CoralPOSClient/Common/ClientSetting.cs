using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoralPOSClient.Properties;
using AFCSetting = CoralPOSClient.Properties.Settings;

namespace CoralPOS.Common
{
    public class ClientSetting
    {   
        public static bool IsClient()
        {
            return string.IsNullOrEmpty(Settings.Default.IsClient)
                       ? false
                       : Settings.Default.IsClient.Equals("1");           
        }

        public static bool IsServer()
        {
            return string.IsNullOrEmpty(Settings.Default.IsClient)
                       ? false
                       : Settings.Default.IsClient.Equals("0");
        }
        public static void Save()
        {
            Settings.Default.Save();
            Settings.Default.Upgrade();
        }
        public static void Reload()
        {
            Settings.Default.Reload();
        }

        public static string SyncImportPath
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
        public static string SyncSuccessPath
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
        public static string SyncErrorPath
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
        public static string SyncExportPath
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
        public static string PrinterName
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
        public static string MySQLDumpPath
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
        public static string DBBackupPath
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

        public static void Reset()
        {
            Settings.Default.Reset();
        }
    }
}
