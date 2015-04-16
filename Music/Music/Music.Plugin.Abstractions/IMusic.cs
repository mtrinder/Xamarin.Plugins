using System;
using System.Collections.Generic;

namespace Music.Plugin.Abstractions
{
    public class PlaybackStateEventArgs : EventArgs { }

    public class PlaybackItemChangedEventArgs : EventArgs { }

    /// <summary>
    /// Interface for Music
    /// </summary>
    public interface IMusic
    {
        /// <summary>
        /// Occurs when playback state changes.
        /// </summary>
        event EventHandler<PlaybackStateEventArgs> PlaybackStateChanged;

        /// <summary>
        /// Occurs when playback item changes.
        /// </summary>
        event EventHandler<PlaybackStateEventArgs> PlaybackItemChanged;

        /// <summary>
        /// Gets a value indicating whether this instance is playing.
        /// </summary>
        /// <value><c>true</c> if this instance is playing; otherwise, <c>false</c>.</value>
        bool IsPlaying { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is paused.
        /// </summary>
        /// <value><c>true</c> if this instance is paused; otherwise, <c>false</c>.</value>
        bool IsPaused  { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is stopped.
        /// </summary>
        /// <value><c>true</c> if this instance is stopped; otherwise, <c>false</c>.</value>
        bool IsStopped { get; }

        /// <summary>
        /// Gets or sets a value indicating whether playback and state events will be fired.
        /// </summary>
        /// <value><c>true</c> if fire events; otherwise, <c>false</c>.</value>
        bool FireEvents { get; set; }

        /// <summary>
        /// Gets the playing track.
        /// </summary>
        /// <value>The playing track.</value>
        MusicTrack PlayingTrack { get; set; }

        /// <summary>
        /// Gets the playback position.
        /// </summary>
        /// <value>The playback position.</value>
        double PlaybackPosition { get; set; }

        /// <summary>
        /// Gets or sets the playback speed between 0 and 1.
        /// </summary>
        /// <value>The playback speed.</value>
        float PlaybackSpeed { get; set; }

        /// <summary>
        /// Gets the state of the playback.
        /// </summary>
        /// <value>The state of the playback.</value>
        PlayerState PlaybackState { get; }

        /// <summary>
        /// Gets the current playlist.
        /// </summary>
        /// <value>The playlist.</value>
        List<MusicTrack> Playlist { get; }

        void Initialize (object context);

        /// <summary>
        /// Play the current or first playlist track.
        /// </summary>
        void Play();

        /// <summary>
        /// Pause the current track.
        /// </summary>
        void Pause();

        /// <summary>
        /// Stop the current track.
        /// </summary>
        void Stop();

        /// <summary>
        /// Skips to the next playlist track.
        /// </summary>
        void SkipToNext();

        /// <summary>
        /// Skips to the previous playlist.
        /// </summary>
        void SkipToPrevious();

        /// <summary>
        /// Queues a playlist ready to be played.
        /// </summary>
        /// <param name="playlist">Playlist.</param>
        void QueuePlaylist (List<MusicTrack> playlist);

        /// <summary>
        /// Stop playing and clear playlist.
        /// </summary>
        void Reset ();

        /// <summary>
        /// Gets the existing song library, if one exists.
        /// </summary>
        /// <returns>The existing song library.</returns>
        List<MusicTrack> GetPlatformMusicLibrary ();
    }
}
