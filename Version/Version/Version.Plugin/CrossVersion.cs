using Version.Plugin.Abstractions;
using System;

namespace Version.Plugin
{
  /// <summary>
  /// Cross platform Version implemenations
  /// </summary>
  public class CrossVersion
  {
    static Lazy<IVersion> Implementation = new Lazy<IVersion>(() => CreateVersion(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IVersion Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IVersion CreateVersion()
    {
#if PORTABLE
        return null;
#else
        return new VersionImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
