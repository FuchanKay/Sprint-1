using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class AnimationManager
{
    private readonly TimeSpan Delay;
    private readonly Dictionary<String, int> SetNamesAndNumFrames;
    private Dictionary<String, Animation> AnimationSets;

    private TimeSpan Elapsed;
    private int CurrentFrameIndex;
    private string CurrentAnimationName;
    private string PreviousAnimationName;

    public AnimationManager(Texture2D texture, int resolution, Dictionary<string, int> SetNamesAndNumFrames)
    {
        Delay = TimeSpan.FromMilliseconds(70);
        this.SetNamesAndNumFrames = SetNamesAndNumFrames;

        LoadAnimationSets(texture, resolution);
    }

    public AnimationManager(Texture2D texture, int resolution, Dictionary<string, int> SetNamesAndNumFrames, TimeSpan delay)
    {
        Delay = delay;
        this.SetNamesAndNumFrames = SetNamesAndNumFrames;

        LoadAnimationSets(texture, resolution);
    }

    public bool AnimationDone(string currentAnimationName)
    {
        return CurrentFrameIndex >= SetNamesAndNumFrames[currentAnimationName];
    }

    public Rectangle UpdateSourceRectangle(string currentAnimationName, GameTime gameTime)
    {
        PreviousAnimationName = CurrentAnimationName;
        CurrentAnimationName = currentAnimationName;

        if(PreviousAnimationName == CurrentAnimationName && SetNamesAndNumFrames[CurrentAnimationName] > 1)
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

    private void LoadAnimationSets(Texture2D texture, int res)
    {
        for(int row = 0; row < SetNamesAndNumFrames.Count; row++)
        {
            List<Rectangle> frames = new List<Rectangle>();
            string name = SetNamesAndNumFrames.Keys.ElementAt(row);
            int numFrames = SetNamesAndNumFrames[name];

            for(int col = 0; col < numFrames; col++)
            {
                frames.Add(new Rectangle(res*col,res*row,res, res));
            }
            AnimationSets.Add(name, new Animation(frames, Delay));
        }
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