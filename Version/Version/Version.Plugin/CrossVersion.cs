using Version.Plugin.Abstractions;
using System;

namespace Version.Plugin
{
    /// <summary>
    /// Cross platform Version implemenations
    /// </summary>
    public class CrossVersion
    {
        static readonly Lazy<IVersion> Implementation = new Lazy<IVersion>(() =>
            #if PORTABLE
                null,
            #else
                new VersionImplementation(),
            #endif
              System.Threading.LazyThreadSafetyMode.PublicationOnly);

        public static IVersion Current
        {
            get
            {
                var version = Implementation.Value;
                if (version == null)
                {
                    throw new NotImplementedException("Don't use CrossVersion from your PCL without first installing the NuGet package in your main project.");
                }
                return version;
            }
        }
    }
}
