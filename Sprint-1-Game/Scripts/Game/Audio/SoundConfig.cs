using Microsoft.Xna.Framework.Audio;

namespace Scripts.Game;

public class SoundConfig(SoundEffectInstance soundEffect, float baseVolume)
{
    public SoundEffectInstance SoundEffect { get; } = soundEffect;
    public float Volume { get; } = baseVolume;
}