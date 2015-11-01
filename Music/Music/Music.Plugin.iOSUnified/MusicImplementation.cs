using System;
using System.Collections.Generic;
using System.Linq;
using Music.Plugin.Abstractions;
using CoreGraphics;

#if __IOSUNIFIED__
using MediaPlayer;
using Foundation;
#else
using MonoTouch.MediaPlayer;
using MonoTouch.Foundation;
#endif

namespace Music.Plugin
{
    /// <summary>
    /// Implementation for Music
    /// </summary>
    public class MusicImplementation : IMusic
    {
        bool _fireEvents;

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

            set
            {
                var item = _mediaMusic.FirstOrDefault (m => m.PersistentID.Equals (value.Id));
                if (item != null)
                {
                    MPMusicPlayerController.ApplicationMusicPlayer.NowPlayingItem = item;
                }
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

            set
            {
                MPMusicPlayerController.ApplicationMusicPlayer.CurrentPlaybackTime = value;
            }
        }

        public float PlaybackSpeed
        {
            get
            {
                return MPMusicPlayerController.ApplicationMusicPlayer.CurrentPlaybackRate;
            }
            set
            {
                MPMusicPlayerController.ApplicationMusicPlayer.CurrentPlaybackRate = value;
            }
        }

        public PlayerState PlaybackState
        {
            get
            {
                return MPMusicPlayerController.ApplicationMusicPlayer.PlaybackState.ToPlayerState ();
            }
        }

        public bool FireEvents
        {
            get
            {
                return _fireEvents;
            }

            set
            {
                _fireEvents = value;

                if (_fireEvents)
                {
                    MPMusicPlayerController.ApplicationMusicPlayer.BeginGeneratingPlaybackNotifications ();
                }
                else
                {
                    MPMusicPlayerController.ApplicationMusicPlayer.EndGeneratingPlaybackNotifications ();
                }
            }
        }

        public void Initialize (object context)
        {
            NSNotificationCenter.DefaultCenter.AddObserver (MPMusicPlayerController.NowPlayingItemDidChangeNotification,
                delegate (NSNotification n) {
                    if (PlaybackItemChanged != null && _fireEvents)
                    {
                        PlaybackItemChanged (this, new PlaybackStateEventArgs ());
                    }
                });

            NSNotificationCenter.DefaultCenter.AddObserver (MPMusicPlayerController.PlaybackStateDidChangeNotification,
                delegate (NSNotification n) {
                    if (PlaybackStateChanged != null && _fireEvents)
                    {
                        PlaybackStateChanged (this, new PlaybackStateEventArgs ());
                    }
                });

            FireEvents = true;
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

        public List<MusicTrack> GetPlatformMusicLibrary()
        {
            return GetAllSongs().Where(s => s != null).Select(s => s.ToTrack()).ToList();
        }

        public void Reset ()
        {
            Stop ();

            _playlist.Clear ();

            MPMusicPlayerController.ApplicationMusicPlayer.NowPlayingItem = null;
        }

        public byte[] GetTrackImage(ulong id)
        {
            var imageBytes = new byte[0];
            var item = _mediaMusic.FirstOrDefault(x => x.PersistentID.Equals(id));
            if (item != null)
            {
                if (item.Artwork != null)
                {
                    var thumb = item.Artwork.ImageWithSize (new CGSize (60, 60));
                    if (thumb != null)
                    {
                        return thumb.AsPNG ().ToArray ();
                    }
                }
            }
            return imageBytes;
        }

        IEnumerable<MPMediaItem> GetAllSongs()
        {
            if (_mediaMusic == null)
            {
                MPMediaItem[] items;
                #if __IOSUNIFIED__
                items = MPMediaQuery.SongsQuery.Items;
                #else
                items = MPMediaQuery.songsQuery.Items;
                #endif

                if (items != null && items.Length > 0)
                {
                    _mediaMusic = items.ToList ();
                }
                else
                {
                    _mediaMusic = new List<MPMediaItem> ();
                }
            }

            return _mediaMusic;
        }
    }
}