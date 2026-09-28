using System;
using Microsoft.Xna.Framework.Audio;

namespace Scripts.GameComponents;

public interface ISoundController
{
    void Play(String sound);
    void MapSound(String sound, SoundEffect soundEffect);
}