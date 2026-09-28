using Microsoft.Xna.Framework;

namespace Scripts.Game;
public class AnimatedSprite
{
    private Animation CurrentAnimation;
    private int CurrentFrameIndex;
    private int Elapsed;
    private Rectangle CurrentFrame;
    public bool IsFinished { get; private set; }

    public AnimatedSprite()
    {
        IsFinished = false;
    }
    public AnimatedSprite(Animation animation)
    {
        CurrentAnimation = animation;
        IsFinished = false;
    }

    public void Update(int dtMs)
    {
        Elapsed += dtMs;
        IsFinished = false;

        if (Elapsed >= CurrentAnimation.Delay)
        {
            Elapsed -= CurrentAnimation.Delay;
            CurrentFrameIndex++;

            if (CurrentFrameIndex >= CurrentAnimation.Frames.Count)
            {
                IsFinished = true;
                CurrentFrameIndex = 0;
            }
        }

        CurrentFrame = CurrentAnimation.Frames[CurrentFrameIndex];
    }

    public Rectangle GetFrame()
    {
        return CurrentFrame;
    }

    public void SetAnimation(Animation animation)
    {
        CurrentAnimation = animation;
        CurrentFrameIndex = 0;
        CurrentFrame = CurrentAnimation.Frames[CurrentFrameIndex];
    }

}