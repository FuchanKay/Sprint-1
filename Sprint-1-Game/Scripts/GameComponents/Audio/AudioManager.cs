using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Scripts.GameComponents;

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
        SoundController.Volume += VolumeIncrement;
        if (SoundController.Volume > 1.0f) SoundController.Volume = 1.0f;
    }

    public void SoundVolumeDown()
    {
        SoundController.Volume -= VolumeIncrement;
        if (SoundController.Volume < 0.0f) SoundController.Volume = 0.0f;
    }

    public void SongVolumeUp()
    {
        SongController.Volume += VolumeIncrement;
        if (SongController.Volume > 1.0f) SongController.Volume = 1.0f;
    }

    public void SongVolumeDown()
    {
        SongController.Volume -= VolumeIncrement;
        if (SongController.Volume < 0.0f) SongController.Volume = 0.0f;
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