using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using NMG.Core;
using NMG.Core.Domain;

namespace NHibernateMappingGenerator
{
    [Serializable]
    public class ApplicationSettings
    {
        public string ConnectionString { get; set; }
        public ServerType ServerType { get; set; }
        public string NameSpace { get; set; }
        public string AssemblyName { get; set; }
        

        public ApplicationSettings()
        {
        }

        public ApplicationSettings(string connectionString, ServerType serverType, string nameSpace, string assemblyName)
        {
            ConnectionString = connectionString;
            ServerType = serverType;
            NameSpace = nameSpace;
            AssemblyName = assemblyName;
        }

        public void Save()
        {
            var streamWriter = new StreamWriter(Application.LocalUserAppDataPath + @"\nmg.config", false);
            XmlSerializer xmlSerializer;
            using (streamWriter)
            {
                xmlSerializer = new XmlSerializer(typeof (ApplicationSettings));
                xmlSerializer.Serialize(streamWriter, this);
            }
        }

        public static ApplicationSettings Load()
        {
            ApplicationSettings appSettings = null;
            var xmlSerializer = new XmlSerializer(typeof (ApplicationSettings));
            var fi = new FileInfo(Application.LocalUserAppDataPath + @"\nmg.config");
            if (fi.Exists)
            {
                using (FileStream fileStream = fi.OpenRead())
                {
                    appSettings = (ApplicationSettings) xmlSerializer.Deserialize(fileStream);
                }
            }
            return appSettings;
        }
    }
}