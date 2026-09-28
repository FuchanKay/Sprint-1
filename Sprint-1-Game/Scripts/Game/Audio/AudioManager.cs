using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Audio;
using Scripts.GameComponents;

namespace Scripts.Game;

public class AudioManager : IAudioManager
{
    private readonly Dictionary<string, SoundEffectInstance> SoundMap = [];
    private float VolumeIncrement = 0.1f;

    public void PlaySound(string sound)
    {
        SoundEffectInstance soundEffect = SoundMap[sound];
        soundEffect.Play();
    }

    public void PlaySong(string sound)
    {
        
    }

    public void SoundVolumeUp()
    {
        throw new NotImplementedException();
    }

    public void SoundVolumeDown()
    {
        throw new NotImplementedException();
    }

    public void SongVolumeUp()
    {
        throw new NotImplementedException();
    }

    public void SongVolumeDown()
    {
        throw new NotImplementedException();
    }

    public void MapSound(String sound, SoundEffect soundEffect)
    {
        SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
        if (!SoundMap.TryAdd(sound, soundEffectInstance))
        {
            SoundMap[sound] = soundEffectInstance;
        }
    }

    public void MapSong(string sound, SoundEffectInstance soundEffectInstance)
    {
        throw new NotImplementedException();
    }
}