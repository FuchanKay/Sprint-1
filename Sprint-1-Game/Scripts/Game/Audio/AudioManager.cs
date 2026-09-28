using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;
using Scripts.GameComponents;
using Microsoft.Xna.Framework.Media;

namespace Scripts.Game;

public class AudioManager : IAudioManager
{
    private readonly SoundController soundController = new SoundController();
    private readonly SongController songController = new SongController();
    private float VolumeIncrement = 0.1f;

    public void PlaySound(string soundName)
    {
        soundController.Play(soundName);
    }

    public void PlaySong(string songName)
    {
        songController.Play(songName);
    }

    public void SoundVolumeUp()
    {
        if (VolumeIncrement + soundController.Volume <= 1.0f)
        {
            soundController.Volume += VolumeIncrement;
        } 
        else
        {
            soundController.Volume = 1.0f;
        }
    }

    public void SoundVolumeDown()
    {
        if (soundController.Volume - VolumeIncrement >= 0.0f)
        {
            soundController.Volume -= VolumeIncrement;
        }
        else
        {
            soundController.Volume = 0.0f;
        }
    }

    public void SongVolumeUp()
    {
        if (VolumeIncrement + songController.Volume <= 1.0f)
        {
            songController.Volume += VolumeIncrement;
        }
        else
        {
            songController.Volume = 1.0f;
        }
    }

    public void SongVolumeDown()
    {
        if (songController.Volume - VolumeIncrement >= 0.0f)
        {
            songController.Volume -= VolumeIncrement;
        }
        else
        {
            songController.Volume = 0.0f;
        }
    }

    public void MapSound(String sound, SoundEffect soundEffect)
    {
        soundController.MapSound(sound, soundEffect);
    }

    public void MapSong(string sound, Song song)
    {
        songController.MapSong(sound, song);
    }
}