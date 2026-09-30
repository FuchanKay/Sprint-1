using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace Scripts.GameComponents;

public class SongController : IAudioController
{
    private readonly Dictionary<string, SongConfig> SongMap = [];
    public float Volume { get; set; } = 1.0f;

    public void Play(string songName)
    {
        SongConfig songConfig = SongMap[songName];
        Song song = songConfig.Song;
        float VolumeMultiplier = songConfig.Volume;
        MediaPlayer.Volume = Volume * VolumeMultiplier;
        MediaPlayer.Play(song);
    }

    public void MapSong(string songName, Song song, float baseVolume)
    {
        SongConfig songConfig = new SongConfig(song, baseVolume);
        if (!SongMap.TryAdd(songName, songConfig))
        {
            SongMap[songName] = songConfig;
        }
    }
}