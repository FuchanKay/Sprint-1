using Microsoft.Xna.Framework;

namespace Scripts.Game;
public class AnimatedSprite
{
    private Animation Animation;
    private int CurrentFrame;
    private int Elapsed;
    public bool IsFinished { get; private set; }

    public AnimatedSprite(Animation animation)
    {
        Animation = animation;
        IsFinished = false;
    }

    public Rectangle UpdateFrame(int dtMs)
    {
        Elapsed += dtMs;
        IsFinished = false;

        if (Elapsed >= Animation.Delay)
        {
            Elapsed -= Animation.Delay;
            CurrentFrame++;

            if (CurrentFrame >= Animation.Frames.Count)
            {
                IsFinished = true;
                CurrentFrame = 0;
            }
        }

        return Animation.Frames[CurrentFrame];
    }

    public Rectangle UpdateAnimation(Animation animation)
    {
        Animation = animation;
        CurrentFrame = 0;
        return Animation.Frames[CurrentFrame];
    }

}