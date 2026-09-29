using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Scripts.GameComponents;

public class SoundController : IAudioController
{
    private readonly Dictionary<string, SoundConfig> SoundMap = [];
    public float Volume {get; set;} = 1.0f;

    public void Play(string soundName)
    {
        SoundConfig soundConfig = SoundMap[soundName];
        SoundEffectInstance soundEffectInstance = soundConfig.SoundEffect;
        float VolumeMultiplier = soundConfig.Volume;
        soundEffectInstance.Volume = Volume * VolumeMultiplier;
        soundEffectInstance.Play();
    }

    public void MapSound(string soundName, SoundEffect soundEffect, float baseVolume)
    {
        SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
        SoundConfig soundConfig = new SoundConfig(soundEffectInstance, baseVolume);
        if (!SoundMap.TryAdd(soundName, soundConfig))
        {
            SoundMap[soundName] = soundConfig;
        }
    }
}