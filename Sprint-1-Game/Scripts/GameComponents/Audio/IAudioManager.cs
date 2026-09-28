using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Scripts.GameComponents;

public interface IAudioManager
{
    void PlaySound(string sound);
    void PlaySong(string song);
    void SoundVolumeUp();
    void SoundVolumeDown();
    void SongVolumeUp();
    void SongVolumeDown();
    void MapSound(string soundName, SoundEffect soundEffect);
    void MapSong(string songName, Song song);
}