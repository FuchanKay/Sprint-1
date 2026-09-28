using Microsoft.Xna.Framework.Media;

namespace Scripts.Game;

public class SongConfig(Song song, float baseVolume)
{
    public Song Song { get; } = song;
    public float Volume { get; } = baseVolume;
}