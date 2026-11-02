using Microsoft.Xna.Framework.Audio;

namespace Scripts.GameComponents;

public class SoundConfig(SoundEffectInstance soundEffect, float baseVolume, float pitch)
{
    public SoundEffectInstance SoundEffect { get; } = soundEffect;
    public float Volume { get; } = baseVolume;
    public float Pitch { get; set; } = pitch;
}