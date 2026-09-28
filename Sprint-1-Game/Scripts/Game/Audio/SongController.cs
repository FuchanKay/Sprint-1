using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;
using Scripts.GameComponents;
using Microsoft.Xna.Framework.Media;

namespace Scripts.Game;

public class SongController : IAudioController
{
    private readonly Dictionary<string, Song> SongMap = [];
    public float Volume {get; set;} = 1.0f;

    public void Play(string songName)
    {
        Song song = SongMap[songName];
        MediaPlayer.Volume = Volume;
        MediaPlayer.Play(song);
    }

    public void MapSong(String songName, Song song)
    {
        if (!SongMap.TryAdd(songName, song))
        {
            SongMap[songName] = song;
        }
    }
}