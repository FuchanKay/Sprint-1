using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;
public class AnimationAtlas : IAnimationAtlas
{
    private const int Delay = 100;
    private readonly Dictionary<string, Animation> Animations = [];

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