using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation
{
    public List<Rectangle> Frames { get; set; }

    public TimeSpan Delay { get; set; }
    public Animation(List<Rectangle> frames, TimeSpan delay)
    {
        Frames = frames;
        Delay = delay;
    }
}