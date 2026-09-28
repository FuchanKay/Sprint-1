using System;
using Microsoft.Xna.Framework.Audio;

namespace Scripts.GameComponents;

public interface IAudioManager
{
    void PlaySound(String sound);
    void PlaySong(String song);
    void SoundVolumeUp();
    void SoundVolumeDown();
    void SongVolumeUp();
    void SongVolumeDown();
    void MapSound(String sound, SoundEffect soundEffect);
    void MapSong(String sound, SoundEffectInstance soundEffectInstance);
}