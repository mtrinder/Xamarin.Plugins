using System;
using Android.App;
using Android.Content.PM;

namespace Version.Plugin
{
    internal class PackageReader
    {
        public static string GetPackageVersion()
        {
            try
            {
                return Application.Context.PackageManager.GetPackageInfo(
                    Application.Context.PackageName, PackageInfoFlags.MetaData).VersionName;
            }
            catch
            {
                return String.Empty;
            }
        }
    }
}