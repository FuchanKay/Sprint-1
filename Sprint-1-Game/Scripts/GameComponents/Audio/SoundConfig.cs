using Microsoft.Xna.Framework.Audio;

namespace Scripts.GameComponents;

public class SoundConfig(SoundEffectInstance soundEffect, float baseVolume)
{
    public SoundEffectInstance SoundEffect { get; } = soundEffect;
    public float Volume { get; } = baseVolume;
}