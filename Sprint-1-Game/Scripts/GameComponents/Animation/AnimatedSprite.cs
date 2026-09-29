using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;
public class AnimatedSprite
{
    private ITextureAtlas TexAtlas;
    private Animation CurrentAnimation;
    private int CurrentFrameIndex;
    private int Elapsed;
    private Rectangle CurrentFrame;
    public bool IsFinished { get; private set; }

    public AnimatedSprite(ITextureAtlas texAtlas)
    {
        TexAtlas = texAtlas;
        IsFinished = false;
    }
    public AnimatedSprite(TextureAtlas texAtlas, Animation animation)
    {
        TexAtlas = texAtlas;
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
        CurrentAnimation = TexAtlas.GetAnimation(animationName);
        CurrentFrameIndex = 0;
        CurrentFrame = CurrentAnimation.Frames[CurrentFrameIndex];
    }

}