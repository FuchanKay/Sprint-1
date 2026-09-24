using System;
using Microsoft.Xna.Framework.Audio;

namespace Scripts.GameComponents;

public interface IAudioManager
{
    void Play(String sound);
}