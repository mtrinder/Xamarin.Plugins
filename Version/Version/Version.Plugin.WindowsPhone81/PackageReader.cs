using System;
using Windows.ApplicationModel;

namespace Version.Plugin
{
    internal class PackageReader
    {
        public static string GetAppVersion()
        {
            try
            {
                var pv = Package.Current.Id.Version;
                return string.Format("{0}.{1}.{2}.{3}", pv.Major, pv.Minor, pv.Build, pv.Revision);
            }
            catch
            {
                return string.Empty;    
            }
        }
    }
}