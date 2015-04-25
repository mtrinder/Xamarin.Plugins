using System;
using System.Xml;

namespace Version.Plugin
{
    internal class ManifestReader
    {
        static XmlReaderSettings _xmlReaderSettings;

        public static string GetAppVersion()
        {
            if (_xmlReaderSettings == null)
            {
                _xmlReaderSettings = new XmlReaderSettings
                {
                    XmlResolver = new XmlXapResolver()
                };
            }

            try
            {
                using (var xmlReader = XmlReader.Create("WMAppManifest.xml", _xmlReaderSettings))
                {
                    xmlReader.ReadToDescendant("App");
                    return xmlReader.GetAttribute("Version");
                }
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}