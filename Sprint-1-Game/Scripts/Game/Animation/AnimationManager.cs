using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;
public class AnimationManager : IAnimationManager
{
    private readonly TimeSpan Delay;
    private readonly HashSet<AnimationSet> AnimationSets;
    private readonly Dictionary<string, Animation> Playables;

    private TimeSpan Elapsed;
    private int CurrentFrameIndex;
    private string CurrentAnimationName;
    private string PreviousAnimationName;

    public AnimationManager(TimeSpan delay)
    {
        Delay = delay;
        AnimationSets = [];
        Playables = [];
    }

    public void AddAnimationSet(string name, int numFrames, int rowNumber)
    {
        AnimationSets.Add(new AnimationSet(name, numFrames, rowNumber));
    }

    public void LoadAnimationSets(Texture2D texture, int width, int height)
    {
        foreach(var animationSet in AnimationSets)
        {
            List<Rectangle> frames = [];

            for (int col = 0; col < animationSet.NumFrames; col++)
            {
                frames.Add(new Rectangle(width * col, height * animationSet.Row, width, height));
            }

            Playables.TryAdd(animationSet.Name, new Animation(frames, Delay));
        }
    }

    public bool IsFinished(string animationName)
    {
        return CurrentFrameIndex >= Playables[animationName].Frames.Count;
    }

    public Rectangle UpdateSourceRectangle(string currentAnimationName, GameTime gameTime)
    {
        PreviousAnimationName = CurrentAnimationName;
        CurrentAnimationName = currentAnimationName;

        if (PreviousAnimationName == CurrentAnimationName && Playables[currentAnimationName].Frames.Count > 1)
        {
            UpdateFrameIndex(gameTime);
        }
        else
        {
            CurrentFrameIndex = 0;
        }

        Animation animation = Playables[CurrentAnimationName];
        return animation.Frames[CurrentFrameIndex];
    }

    private void UpdateFrameIndex(GameTime gameTime)
    {
        Elapsed += gameTime.ElapsedGameTime;

        if (Elapsed >= Delay)
        {
            Elapsed -= Delay;
            CurrentFrameIndex++;

            int maxFrameIndex = Playables[CurrentAnimationName].Frames.Count;
            if (CurrentFrameIndex > maxFrameIndex)
            {
                CurrentFrameIndex = 0;
            }
        }
    }
}