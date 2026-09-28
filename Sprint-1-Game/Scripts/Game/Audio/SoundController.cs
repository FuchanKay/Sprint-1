using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SoundController : IAudioController
{
    private readonly Dictionary<string, SoundEffectInstance> SoundMap = [];
    public float Volume {get; set;} = 1.0f;

    public void Play(string soundName)
    {
        SoundEffectInstance soundEffect = SoundMap[soundName];
        soundEffect.Volume = Volume;
        soundEffect.Play();
    }

    public void MapSound(String soundName, SoundEffect soundEffect)
    {
        SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
        if (!SoundMap.TryAdd(soundName, soundEffectInstance))
        {
            SoundMap[soundName] = soundEffectInstance;
        }
    }
}