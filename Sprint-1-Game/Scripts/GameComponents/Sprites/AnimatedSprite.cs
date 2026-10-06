using System;
using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;

public class AnimatedSprite(ITextureAtlas textureAtlas) : Sprite(textureAtlas)
{
    private Animation CurrentAnimation;
    private int CurrentFrameIndex;
    private int TotalElapsed;
    private int Elapsed;
    private Vector2 StartPosition;
    private int NumFrames => CurrentAnimation.Frames.Count;
    private int Duration => NumFrames * CurrentAnimation.Delay;
    private readonly float Complete = 1.0f;
    private float Progress => Math.Min((float)TotalElapsed / (float)Duration, Complete);
    public override bool IsFinished { get; protected set; }

    public override void Update(int dtMs)
    {
        TotalElapsed += dtMs;

        Vector2 diff = CurrentPosition - TargetPosition;
        bool shouldMove = diff.Length() > 0.01f; 
        if(shouldMove)
        {
            // update current position based on progress through animation
            float xPos = StartPosition.X + (TargetPosition.X - StartPosition.X) * Progress;
            float yPos = StartPosition.Y + (TargetPosition.Y - StartPosition.Y) * Progress;
            Console.WriteLine("TargetX: " + TargetPosition.X + " CurrentX: " + xPos);
            Console.WriteLine("TargetY: " + TargetPosition.Y + " CurrentY: " + yPos);
            Console.WriteLine("Frame Index: " + CurrentFrameIndex);

            CurrentPosition = new Vector2(xPos, yPos);
        } else
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

    public override void SetTargetPosition(Vector2 position)
    {
        TargetPosition = position;
        StartPosition = CurrentPosition;
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