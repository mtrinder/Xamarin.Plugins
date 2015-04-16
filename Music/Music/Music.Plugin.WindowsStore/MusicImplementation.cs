using System.Collections.Generic;
using Music.Plugin.Abstractions;
using System;


namespace Music.Plugin
{
  /// <summary>
  /// Implementation for Music
  /// </summary>
  public class MusicImplementation : IMusic
  {
      public event EventHandler<PlaybackStateEventArgs> PlaybackStateChanged;
      public event EventHandler<PlaybackStateEventArgs> PlaybackItemChanged;
      public bool IsPlaying { get; private set; }
      public bool IsPaused { get; private set; }
      public bool IsStopped { get; private set; }
      public MusicTrack PlayingTrack { get; private set; }
      public double PlaybackPosition { get; private set; }
      public List<MusicTrack> Playlist { get; private set; }
      public void Play()
      {
          throw new NotImplementedException();
      }

      public void Pause()
      {
          throw new NotImplementedException();
      }

      public void Stop()
      {
          throw new NotImplementedException();
      }

      public void SkipToNext()
      {
          throw new NotImplementedException();
      }

      public void SkipToPrevious()
      {
          throw new NotImplementedException();
      }

      public void QueuePlaylist(List<MusicTrack> playlist)
      {
          throw new NotImplementedException();
      }

      public void Reset()
      {
          throw new NotImplementedException();
      }

      public List<MusicTrack> GetExistingSongLibrary()
      {
          throw new NotImplementedException();
      }

      public void QueuePlaylist(IEnumerable<MusicTrack> playlist)
      {
          throw new NotImplementedException();
      }
  }
}