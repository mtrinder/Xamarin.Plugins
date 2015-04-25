using System;
using MonoTouch.Foundation;
using Version.Plugin.Abstractions;

namespace Version.Plugin
{
  /// <summary>
  /// Implementation for Version
  /// </summary>
  public class VersionImplementation : IVersion
  {
      readonly NSString _buildKey;
      readonly NSString _versionKey;

      /// <summary>
      /// 
      /// </summary>
      public VersionImplementation()
      {
          _buildKey = new NSString("CFBundleVersion");
          _versionKey = new NSString("CFBundleShortVersionString");
      }

      /// <summary>
      /// Returns the app version with build number appended
      /// </summary>
      public string Version
      {
          get
          {
              try
              {
                  var build = NSBundle.MainBundle.InfoDictionary.ValueForKey(_buildKey);
                  var version = NSBundle.MainBundle.InfoDictionary.ValueForKey(_versionKey);
                  return string.Format("{0}.{1}", version, build);
              }
              catch
              {
                  return string.Empty;
              }
          }
      }
  }
}