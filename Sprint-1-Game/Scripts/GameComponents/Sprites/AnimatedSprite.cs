namespace Scripts.GameComponents;
public class AnimatedSprite(ITextureAtlas textureAtlas) : Sprite
{
    private Animation CurrentAnimation;
    private int CurrentFrameIndex;
    private int Elapsed;
    public bool IsFinished { get; private set; } = false;

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

        CurrentRegion = CurrentAnimation.Frames[CurrentFrameIndex];
    }

    public void SetAnimation(string animationName)
    {
        CurrentAnimation = textureAtlas.GetAnimation(animationName);
        CurrentFrameIndex = 0;
        CurrentRegion = CurrentAnimation.Frames[CurrentFrameIndex];
    }

}