using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;
public class TextureAtlas : ITextureAtlas
{
    private const int Delay = 100;
    private readonly Dictionary<string, Rectangle> Regions = [];
    private readonly Dictionary<string, Animation> Animations = [];

    public void AddRegion(string name, int x, int y, int width, int height)
    {
        Regions.TryAdd(name, new Rectangle(x, y, width, height));
    }

    public Rectangle GetRegion(string name)
    {
        return Regions[name];
    }

    public void AddAnimation(string name, int row, int numFrames, int width, int height)
    {
        Animation animation = new Animation(Delay);
        LoadAnimation(name, row, numFrames, width, height, animation);
    }

    public void AddAnimation(string name, int row, int numFrames, int width, int height, int delay)
    {
        Animation animation = new Animation(delay);
        LoadAnimation(name, row, numFrames, width, height, animation);
    }

    public Animation GetAnimation(string animationName)
    {
        return Animations[animationName];
    }

    private void LoadAnimation(string name, int row, int numFrames, int width, int height, Animation animation)
    {
        for (int col = 0; col < numFrames; col++)
        {
            animation.Frames.Add(new Rectangle(width * col, height * row, width, height));
        }

        Animations.TryAdd(name, animation);
    }

}