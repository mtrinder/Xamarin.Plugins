using System;

namespace Music.Plugin.Abstractions
{

    public class MusicTrack
    {
        public ulong Id { get; set; }
        public string Filename { get; set; }

        public string Name { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public string Length { get; set; }
        public string Genre { get; set; }

        public double Seconds { get; set; }
        public byte[] Image { get; set; }
    }
}
