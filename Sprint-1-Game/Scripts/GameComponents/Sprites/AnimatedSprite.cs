using Microsoft.Xna.Framework;
using System;

namespace Scripts.GameComponents;

public class AnimatedSprite(ITextureAtlas textureAtlas) : Sprite(textureAtlas)
{
    private Animation CurrentAnimation;
    private int CurrentFrameIndex;
    private int Elapsed;
    private int NumFrames => CurrentAnimation.Frames.Count;
    private int Length => NumFrames * CurrentAnimation.Delay;
    private float Progress => Math.Min((float)TotalElapsed / (float)Length, Complete);
    public override bool IsFinished { get; protected set; }

    public override void Update(int dtMs)
    {
        TotalElapsed += dtMs;

        Vector2 diff = CurrentPosition - TargetPosition;
        bool shouldMove = diff.Length() > 0.01f;
        if (shouldMove)
        {
            // update current position based on progress through animation
            float xPos = StartPosition.X + (TargetPosition.X - StartPosition.X) * Progress;
            float yPos = StartPosition.Y + (TargetPosition.Y - StartPosition.Y) * Progress;

            CurrentPosition = new Vector2(xPos, yPos);
        }
        else
        {
            CurrentPosition = TargetPosition;
        }
        UpdateFrame(dtMs);
    }

    public override void SetState(string animationName)
    {
        CurrentAnimation = TexAtlas.GetAnimation(animationName);
        CurrentFrameIndex = 0;
        CurrentRegion = CurrentAnimation.Frames[CurrentFrameIndex];
    }

    private void UpdateFrame(int dtMs)
    {
        Elapsed += dtMs;
        IsFinished = false;

        if (Elapsed >= CurrentAnimation.Delay)
        {
            Elapsed -= CurrentAnimation.Delay;
            CurrentFrameIndex++;

            if (CurrentFrameIndex >= NumFrames)
            {
                IsFinished = true;
                CurrentFrameIndex = 0;
                TotalElapsed = 0;
            }
        }

        CurrentRegion = CurrentAnimation.Frames[CurrentFrameIndex];
    }

}