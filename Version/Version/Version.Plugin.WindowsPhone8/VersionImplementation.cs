using Version.Plugin.Abstractions;

namespace Version.Plugin
{
    /// <summary>
    /// Implementation for Version
    /// </summary>
    public class VersionImplementation : IVersion
    {
        /// <summary>
        /// Returns the app version from the manifest
        /// </summary>
        public string Version
        {
            get { return ManifestReader.GetAppVersion(); }
        }
    }
}