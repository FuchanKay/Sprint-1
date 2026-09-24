using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;
public interface IAnimationManager
{
    bool AnimationDone(string currentAnimationName);
    Rectangle UpdateSourceRectangle(string currentSet, GameTime gametime);
}