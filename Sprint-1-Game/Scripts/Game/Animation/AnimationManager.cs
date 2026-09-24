using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;
public class AnimationManager
{
    private readonly TimeSpan Delay;
    private readonly OrderedDictionary<string, int> SetNamesAndNumFrames;
    private readonly Dictionary<string, Animation> AnimationSets;

    private TimeSpan Elapsed;
    private int CurrentFrameIndex;
    private string CurrentAnimationName;
    private string PreviousAnimationName;

    public AnimationManager(TimeSpan delay)
    {
        Delay = delay;
        SetNamesAndNumFrames = new();
        AnimationSets = new();
    }

    public void AddNameAndFrames(string name, int numFrames)
    {
        if(!SetNamesAndNumFrames.TryAdd(name, numFrames))
        {
            SetNamesAndNumFrames[name] = numFrames;
        }
    }

    public void LoadAnimationSets(Texture2D texture, int width, int height)
    {
        for(int row = 0; row < SetNamesAndNumFrames.Count; row++)
        {
            List<Rectangle> frames = [];
            string name = SetNamesAndNumFrames.Keys.ElementAt(row);
            int numFrames = SetNamesAndNumFrames[name];

            for(int col = 0; col < numFrames; col++)
            {
                frames.Add(new Rectangle(width * col, height * row, width, height));
            }
            AnimationSets.Add(name, new Animation(frames, Delay));
        }
    }

    public bool IsFinished(string currentAnimationName)
    {
        return CurrentFrameIndex >= SetNamesAndNumFrames[currentAnimationName];
    }

    public Rectangle UpdateSourceRectangle(string currentAnimationName, GameTime gameTime)
    {
        PreviousAnimationName = CurrentAnimationName;
        CurrentAnimationName = currentAnimationName;

        if (PreviousAnimationName == CurrentAnimationName && SetNamesAndNumFrames[CurrentAnimationName] > 1)
        {
            UpdateFrameIndex(gameTime);
        }
        else
        {
            CurrentFrameIndex = 0;
        }

        Animation animation = AnimationSets[CurrentAnimationName];
        return animation.Frames[CurrentFrameIndex];
    }

    private void UpdateFrameIndex(GameTime gameTime)
    {
        Elapsed += gameTime.ElapsedGameTime;

        if (Elapsed >= Delay)
        {
            Elapsed -= Delay;
            CurrentFrameIndex++;

            int maxFrameIndex = SetNamesAndNumFrames[CurrentAnimationName];
            if (CurrentFrameIndex > maxFrameIndex)
            {
                CurrentFrameIndex = 0;
            }
        }
    }
}