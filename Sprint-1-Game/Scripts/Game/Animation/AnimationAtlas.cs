using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Scripts.Game;
public class AnimationAtlas
{
    private readonly Dictionary<string, Animation> Animations = [];

    public void AddAnimation(string name, int row, int numFrames)
    {
        int width = 64;
        int height = 64;
        int delay = 100;
        Animation animation = new Animation(delay);
        LoadAnimation(name, row, numFrames, width, height, animation);
    }

    public void AddAnimation(string name, int row, int numFrames, int width, int height)
    {
        int delay = 100;
        Animation animation = new Animation(delay);
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

    public AnimatedSprite CreateAnimatedSprite(string animationName)
    {
        return new AnimatedSprite(GetAnimation(animationName));
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