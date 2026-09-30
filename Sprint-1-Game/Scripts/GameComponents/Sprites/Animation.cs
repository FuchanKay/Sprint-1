using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Scripts.GameComponents;
public class Animation
{
    public int Delay { get; set; }
    public List<Rectangle> Frames { get; set; }

    public Animation(int delay)
    {
        Delay = delay;
        Frames = [];
    }
}