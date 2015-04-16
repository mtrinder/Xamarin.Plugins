using Music.Plugin.Abstractions;
using System;
using MediaPlayer;
using System.Collections.Generic;
using System.Linq;
using CoreGraphics;


namespace Music.Plugin
{
    /// <summary>
    /// Implementation for Music
    /// </summary>
    public class MusicImplementation : IMusic
    {
        List<MPMediaItem> _mediaMusic;

        readonly List<MusicTrack> _playlist = new List<MusicTrack> ();

        public event EventHandler<PlaybackStateEventArgs> PlaybackStateChanged;

        public event EventHandler<PlaybackStateEventArgs> PlaybackItemChanged;

        public List<MusicTrack> Playlist
        {
            get
            {
                return _playlist;
            }
        }

        public bool IsPlaying
        {
            get
            {
                var item = MPMusicPlayerController.ApplicationMusicPlayer.NowPlayingItem;
                var rate = MPMusicPlayerController.ApplicationMusicPlayer.CurrentPlaybackRate;
                var state = MPMusicPlayerController.ApplicationMusicPlayer.PlaybackState;

                return item != null && state == MPMusicPlaybackState.Playing && Math.Round (rate) >= 1f;
            }
        }

        public bool IsStopped
        {
            get
            {
                var item = MPMusicPlayerController.ApplicationMusicPlayer.NowPlayingItem;
                var rate = MPMusicPlayerController.ApplicationMusicPlayer.CurrentPlaybackRate;
                var state = MPMusicPlayerController.ApplicationMusicPlayer.PlaybackState;

                return item == null || state == MPMusicPlaybackState.Stopped || Math.Round (rate).Equals (0);
            }
        }

        public bool IsPaused
        {
            get
            {
                var rate = MPMusicPlayerController.ApplicationMusicPlayer.CurrentPlaybackRate;
                var state = MPMusicPlayerController.ApplicationMusicPlayer.PlaybackState;

                return state == MPMusicPlaybackState.Paused && Math.Round (rate).Equals (0);
            }
        }

        public MusicTrack PlayingTrack
        {
            get
            {
                MusicTrack track = null;

                var item = MPMusicPlayerController.ApplicationMusicPlayer.NowPlayingItem;
                if (item != null)
                {
                    track = item.ToTrack ();
                }

                return track;
            }
        }

        public double PlaybackPosition
        {
            get
            {
                double position = 0d;

                var item = MPMusicPlayerController.ApplicationMusicPlayer.NowPlayingItem;
                if (item != null)
                {
                    position = MPMusicPlayerController.ApplicationMusicPlayer.CurrentPlaybackTime;
                }

                return position;
            }
        }

        public void Play ()
        {
            if (_playlist.Count > 0)
            {
                MPMusicPlayerController.ApplicationMusicPlayer.Play ();
            }
        }

        public void Pause ()
        {
            if (_playlist.Count > 0)
            {
                MPMusicPlayerController.ApplicationMusicPlayer.Pause ();
            }
        }

        public void Stop ()
        {
            if (_playlist.Count > 0)
            {
                MPMusicPlayerController.ApplicationMusicPlayer.Stop ();
            }
        }

        public void SkipToNext ()
        {
            var track = PlayingTrack;
            if (track != null && _playlist.Count > 0)
            {
                var index = _playlist.FindIndex (i => i.Id.Equals (track.Id));
                if (index < _playlist.Count - 1)
                {
                    MPMusicPlayerController.ApplicationMusicPlayer.SkipToNextItem ();
                }
            }
        }

        public void SkipToPrevious ()
        {
            var track = PlayingTrack;
            if (track != null && _playlist.Count > 0)
            {
                var index = _playlist.FindIndex (i => i.Id.Equals (track.Id));
                if (index > 0)
                {
                    MPMusicPlayerController.ApplicationMusicPlayer.SkipToPreviousItem ();
                }
            }
        }

        public void QueuePlaylist(List<MusicTrack> playlist)
        {
            _playlist.Clear ();
            _playlist.AddRange (playlist);

            var mediaItemList = new List<MPMediaItem> ();

            playlist.ToList ().ForEach (delegate (MusicTrack i) {
                var m = GetAllSongs().FirstOrDefault (s => s.PersistentID.Equals (i.Id));
                if (m != null)
                {
                    mediaItemList.Add (m);
                }
            });

            MPMusicPlayerController.ApplicationMusicPlayer.SetQueue (new MPMediaItemCollection (mediaItemList.ToArray ()));
        }

        public List<MusicTrack> GetExistingSongLibrary()
        {
            return GetAllSongs ().Select (s => s.ToTrack ()).ToList ();
        }

        public void Reset ()
        {
            Stop ();

            _playlist.Clear ();

            MPMusicPlayerController.ApplicationMusicPlayer.NowPlayingItem = null;
        }

        IEnumerable<MPMediaItem> GetAllSongs()
        {
            if (_mediaMusic == null)
            {
                if (MPMediaQuery.SongsQuery.Items != null
                    && MPMediaQuery.SongsQuery.Items.Length > 0)
                {
                    _mediaMusic = MPMediaQuery.SongsQuery.Items.ToList ();
                }
                else
                {
                    _mediaMusic = new List<MPMediaItem> ();
                }
            }

            return _mediaMusic;
        }
    }

    public static class Extensions
    {
        public static MusicTrack ToTrack (this MPMediaItem item)
        {
            var track = new MusicTrack
            {
                Id = item.PersistentID,
                Filename = item.AssetURL != null ? item.AssetURL.AbsoluteString : string.Empty,
                Name = item.Title,
                Artist = item.Artist,
                Album = item.AlbumTitle,
                Seconds = item.PlaybackDuration
            };

            var ts = TimeSpan.FromSeconds (track.Seconds);
            track.Length = string.Format("{0}:{1} min", ts.Minutes.ToString("D2"), ts.Seconds.ToString("D2"));

            if (item.Artwork != null)
            {
                var thumb = item.Artwork.ImageWithSize (new CGSize (60, 60));
                track.Image = thumb.AsPNG ().ToArray ();
            }

            return track;
        }
    }
}