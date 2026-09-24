using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SoundManager : IAudioManager
{
    private readonly Dictionary<string, SoundEffectInstance> SoundMap = [];
    public float Volume {get; set;} = 1.0f;

    public void Play(string sound)
    {
        SoundEffectInstance soundEffect = SoundMap[sound];
        soundEffect.Volume = Volume;
        soundEffect.Play();
    }

    public void MapSound(String sound, SoundEffect soundEffect)
    {
        SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
        if (!SoundMap.TryAdd(sound, soundEffectInstance))
        {
            SoundMap[sound] = soundEffectInstance;
        }
    }
}