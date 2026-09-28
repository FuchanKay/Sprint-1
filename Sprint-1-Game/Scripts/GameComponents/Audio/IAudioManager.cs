using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Scripts.GameComponents;

public interface IAudioManager
{
    void PlaySound(String sound);
    void PlaySong(String song);
    void SoundVolumeUp();
    void SoundVolumeDown();
    void SongVolumeUp();
    void SongVolumeDown();
    void MapSound(String soundName, SoundEffect soundEffect);
    void MapSong(String songName, Song song);
}