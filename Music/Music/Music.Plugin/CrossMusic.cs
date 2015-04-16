using Music.Plugin.Abstractions;
using System;

namespace Music.Plugin
{
  /// <summary>
  /// Cross platform Music implemenations
  /// </summary>
  public class CrossMusic
  {
    static Lazy<IMusic> Implementation = new Lazy<IMusic>(() => CreateMusic(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IMusic Current
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

    static IMusic CreateMusic()
    {
#if PORTABLE
        return null;
#else
        return new MusicImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
