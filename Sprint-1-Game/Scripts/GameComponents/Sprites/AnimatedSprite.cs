using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;

namespace Scripts.GameComponents;

public class AnimatedSprite(ITextureAtlas textureAtlas) : Sprite(textureAtlas)
{
    private Animation CurrentAnimation;
    private int CurrentFrameIndex;
    private int NumFrames => CurrentAnimation.Frames.Count;
    private int Length => CustomLength ?? (NumFrames * CurrentAnimation.Delay);
    private int? CustomLength;
    public override int Duration
    {
        get => Length;
        set => CustomLength = value;
    }
    private bool ShouldMove => (CurrentPosition - TargetPosition).Length() > 0.01f;

    private float Progress => Math.Min((float)Elapsed / (float)Length, Complete);
    public override bool IsFinished { get; protected set; }

    public override void Update(int dtMs)
    {
        if (ShouldMove)
        {
            // update current position based on progress through animation
            float xPos = StartPosition.X + (TargetPosition.X - StartPosition.X) * Progress;
            float yPos = StartPosition.Y + (TargetPosition.Y - StartPosition.Y) * Progress;

            CurrentPosition = new Vector2(xPos, yPos);
        }
        else
        {
            CurrentPosition = TargetPosition;
            StartPosition = CurrentPosition;
        }
        UpdateFrame(dtMs);
    }

    public override void SetState(string animationName)
    {
        CurrentAnimation = TexAtlas.GetAnimation(animationName);
        CurrentFrameIndex = 0;
        Elapsed = 0;
        IsFinished = false;
        CurrentRegion = CurrentAnimation.Frames[CurrentFrameIndex];
    }

    private void UpdateFrame(int dtMs)
    {
        Elapsed += dtMs;
        IsFinished = Elapsed >= Length;

        if(IsFinished)
        {
            CurrentFrameIndex = 0;
            Elapsed = 0;
            CurrentPosition = TargetPosition;
            StartPosition = CurrentPosition;
        } 
        else if(ShouldMove) // updates frames evenly over animation length
        {
            CurrentFrameIndex = Math.Min(Elapsed * NumFrames / Length, NumFrames - 1);
        } 
        else
        {
            CurrentFrameIndex = Elapsed / CurrentAnimation.Delay;
        }

        CurrentRegion = CurrentAnimation.Frames[CurrentFrameIndex];
    }

}