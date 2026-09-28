using Microsoft.Xna.Framework.Audio;
using Scripts.GameComponents;
using Microsoft.Xna.Framework.Media;

namespace Scripts.Game;

public class AudioManager : IAudioManager
{
    private readonly SoundController SoundController = new();
    private readonly SongController SongController = new();
    private readonly float VolumeIncrement = 0.1f;

    public void PlaySound(string soundName)
    {
        SoundController.Play(soundName);
    }

    public void PlaySong(string songName)
    {
        SongController.Play(songName);
    }

    public void SoundVolumeUp()
    {
        if (VolumeIncrement + SoundController.Volume <= 1.0f)
        {
            SoundController.Volume += VolumeIncrement;
        } 
        else
        {
            SoundController.Volume = 1.0f;
        }
    }

    public void SoundVolumeDown()
    {
        if (SoundController.Volume - VolumeIncrement >= 0.0f)
        {
            SoundController.Volume -= VolumeIncrement;
        }
        else
        {
            SoundController.Volume = 0.0f;
        }
    }

    public void SongVolumeUp()
    {
        if (VolumeIncrement + SongController.Volume <= 1.0f)
        {
            SongController.Volume += VolumeIncrement;
        }
        else
        {
            SongController.Volume = 1.0f;
        }
    }

    public void SongVolumeDown()
    {
        if (SongController.Volume - VolumeIncrement >= 0.0f)
        {
            SongController.Volume -= VolumeIncrement;
        }
        else
        {
            SongController.Volume = 0.0f;
        }
    }

    public void MapSound(string soundName, SoundEffect soundEffect, float baseVolume)
    {
        SoundController.MapSound(soundName, soundEffect, baseVolume);
    }

    public void MapSong(string songName, Song song, float baseVolume)
    {
        SongController.MapSong(songName, song, baseVolume);
    }
}