using System;

namespace Version.Plugin.Abstractions
{
  /// <summary>
  /// Interface for Version
  /// </summary>
  public interface IVersion
  {
      /// <summary>
      /// Current App Version
      /// </summary>
      string Version { get; }
  }
}
