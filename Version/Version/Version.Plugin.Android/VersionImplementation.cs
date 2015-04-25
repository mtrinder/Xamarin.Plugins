using Version.Plugin.Abstractions;

namespace Version.Plugin
{
    /// <summary>
    /// Implementation for Version
    /// </summary>
    public class VersionImplementation : IVersion
    {
        /// <summary>
        /// Returns the app version
        /// </summary>
        public string Version
        {
            get { return PackageReader.GetPackageVersion(); }
        }
    }
}