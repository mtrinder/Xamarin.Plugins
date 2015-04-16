using System;
using CoreGraphics;
using MediaPlayer;
using Music.Plugin.Abstractions;

namespace Music.Plugin
{

    public static class Extensions
    {
        public static PlayerState ToPlayerState (this MPMusicPlaybackState state)
        {
            switch (state)
            {
                case MPMusicPlaybackState.Playing:
                    return PlayerState.Playing;
                case MPMusicPlaybackState.Paused:
                    return PlayerState.Paused;
                case MPMusicPlaybackState.Stopped:
                    return PlayerState.Stopped;
                case MPMusicPlaybackState.SeekingBackward:
                    return PlayerState.SeekingBack;
                case MPMusicPlaybackState.SeekingForward:
                    return PlayerState.SeekingNext;
            }    
            return PlayerState.Unknown;
        }

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