using Windows.ApplicationModel;

namespace Version.Plugin
{
    public class PackageReader
    {
        public static string GetAppVersion()
        {
            var pv = Package.Current.Id.Version;
            return string.Format("{0}.{1}.{2}.{3}", pv.Major, pv.Minor, pv.Revision, pv.Build);
        }
    }
}