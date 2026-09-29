using Microsoft.Xna.Framework;

namespace Scripts.Game;
public class AnimatedSprite
{
    private AnimationAtlas AniAtlas;
    private Animation CurrentAnimation;
    private int CurrentFrameIndex;
    private int Elapsed;
    private Rectangle CurrentFrame;
    public bool IsFinished { get; private set; }

    public AnimatedSprite(AnimationAtlas aniAtlas)
    {
        AniAtlas = aniAtlas;
        IsFinished = false;
    }
    public AnimatedSprite(AnimationAtlas aniAtlas, Animation animation)
    {
        AniAtlas = aniAtlas;
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

    public void SetAnimation(string animationName)
    {
        CurrentAnimation = AniAtlas.GetAnimation(animationName);
        CurrentFrameIndex = 0;
        CurrentFrame = CurrentAnimation.Frames[CurrentFrameIndex];
    }

}