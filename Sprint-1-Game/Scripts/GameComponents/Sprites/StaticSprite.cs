using Microsoft.Xna.Framework;
using System;

namespace Scripts.GameComponents;

public class StaticSprite(ITextureAtlas textureAtlas) : Sprite(textureAtlas)
{
    public override bool IsFinished { get; protected set; } = false;
    private float Progress => Math.Min((float)Elapsed / (float)Duration, Complete);
    public override void Update(int dtMs)
    {
        Elapsed += dtMs;

        Vector2 diff = CurrentPosition - TargetPosition;
        bool shouldMove = diff.Length() > 0.01f;
        if (shouldMove)
        {
            IsFinished = false;
            // update current position based on progress through animation
            float xPos = StartPosition.X + (TargetPosition.X - StartPosition.X) * Progress;
            float yPos = StartPosition.Y + (TargetPosition.Y - StartPosition.Y) * Progress;

            CurrentPosition = new Vector2(xPos, yPos);
        }
        else
        {
            CurrentPosition = TargetPosition;
            Elapsed = 0;
            IsFinished = true;
        }
    }
    public override void SetState(string name)
    {
        CurrentRegion = textureAtlas.GetRegion(name);
    }

}