using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoralPOS.Common
{
    public interface IBaseClientSetting
    {
        bool IsClient();
        bool IsServer();
        string SyncImportPath { get; set; }
        string SyncSuccessPath { get; set; }
        string SyncErrorPath { get; set; }
        string SyncExportPath { get; set; }
        string MySQLDumpPath { get; set; }
        string DBBackupPath { get; set; }
    }
}
