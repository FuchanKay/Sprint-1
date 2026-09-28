using System;
using Microsoft.Xna.Framework.Audio;

namespace Scripts.GameComponents;

public interface IAudioController
{
    void Play(String sound);
}