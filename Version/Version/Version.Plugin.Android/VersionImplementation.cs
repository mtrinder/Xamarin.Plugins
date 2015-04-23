using Android.App;
using Android.Content.PM;
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
          get { return GetPackageVersion(); }
      }

      static string GetPackageVersion()
      {
          try
          {
              return Application.Context.PackageManager.GetPackageInfo(
                  Application.Context.PackageName, PackageInfoFlags.MetaData).VersionName;
          }
          catch
          {
              return string.Empty;
          }
      }
  }
}