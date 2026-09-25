using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace Scripts.Game;
public class Animation
{
    public string Name { get; set; }
    public int NumFrames { get; set; }
    public int Row { get; set; }
    public List<Rectangle> Frames { get; set; }

    public Animation(string name, int numFrames, int rowNumber)
    {
        Name = name;
        NumFrames = numFrames;
        Frames = [];
        Row = rowNumber;
    }
}