using System.Collections.Generic;
using Scripts.GameComponents;
using Microsoft.Xna.Framework.Media;

namespace Scripts.Game;

public class SongController : IAudioController
{
    private readonly Dictionary<string, Song> SongMap = [];
    public float Volume {get; set;} = 0.15f;

    public void Play(string songName)
    {
        Song song = SongMap[songName];
        MediaPlayer.Volume = Volume;
        MediaPlayer.Play(song);
    }

    public void MapSong(string songName, Song song)
    {
        if (!SongMap.TryAdd(songName, song))
        {
            SongMap[songName] = song;
        }
    }
}