using System;
using System.Collections.Generic;

using Music.Plugin.Abstractions;

namespace Music.Plugin
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class MusicImplementation : IMusic
    {
        public event EventHandler<PlaybackStateEventArgs> PlaybackStateChanged;

        public event EventHandler<PlaybackStateEventArgs> PlaybackItemChanged;

        public bool IsPlaying
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsPaused
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsStopped
        {
            get { throw new NotImplementedException(); }
        }

        public MusicTrack PlayingTrack
        {
            get { throw new NotImplementedException(); }
        }

        public double PlaybackPosition
        {
            get { throw new NotImplementedException(); }
        }

        public List<MusicTrack> Playlist
        {
            get { throw new NotImplementedException(); }
        }

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
    }
}