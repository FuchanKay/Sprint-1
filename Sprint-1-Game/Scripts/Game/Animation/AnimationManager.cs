using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;
public class AnimationManager : IAnimationManager
{
    private readonly HashSet<Animation> Animations;
    private readonly Dictionary<string, Animation> Playables;
    private readonly int Delay;
    private int Elapsed;
    private int CurrentFrameIndex;
    private bool animationFinished;
    private string CurrentAnimationName;
    private string PreviousAnimationName;

    public AnimationManager(int delay)
    {
        Delay = delay;
        Animations = [];
        Playables = [];
        animationFinished = false;
    }

    public void AddAnimation(string name, int numFrames, int rowNumber)
    {
        Animations.Add(new Animation(name, numFrames, rowNumber));
    }

    public void LoadAnimations(int width, int height)
    {
        foreach(var animation in Animations)
        {
            for (int col = 0; col < animation.NumFrames; col++)
            {
                animation.Frames.Add(new Rectangle(width * col, height * animation.Row, width, height));
            }

            Playables.TryAdd(animation.Name, animation);
        }
    }

    public bool IsFinished()
    {
        return animationFinished;
    }

    public Rectangle UpdateSourceRectangle(string currentAnimationName, int dtMs)
    {
        PreviousAnimationName = CurrentAnimationName;
        CurrentAnimationName = currentAnimationName;

        bool multiFrame = Playables[currentAnimationName].Frames.Count > 1;
        if (PreviousAnimationName == CurrentAnimationName && multiFrame)
        {
            UpdateFrameIndex(dtMs);
        }
        else
        {
            CurrentFrameIndex = 0;
        }

        Animation animation = Playables[CurrentAnimationName];
        return animation.Frames[CurrentFrameIndex];
    }

    private void UpdateFrameIndex(int dtMs)
    {
        animationFinished = false;
        Elapsed += dtMs;

        if (Elapsed >= Delay)
        {
            Elapsed -= Delay;
            CurrentFrameIndex++;

            int maxFrameIndex = Playables[CurrentAnimationName].Frames.Count - 1;
            if (CurrentFrameIndex > maxFrameIndex)
            {
                animationFinished = true;
                CurrentFrameIndex = 0;
            }
        }
    }
}