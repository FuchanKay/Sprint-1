using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;

public class FadeTransition(PuzzleLevel Level) : ILevelTransition
{
    public int TransitionDurationMs { get; set; } = 400;
    public bool IsActive => currentFadeType != FadeType.None;
    private int TransitionTimeElapsedMs;
    private int NewLevelIndex;
    
    #nullable enable
    private Texture2D? transitionPixel;
    private enum FadeType {None, FadeIn, FadeOut};
    private FadeType currentFadeType = FadeType.None; 
   
    public void StartLevelTransition(int newLevelIndex)
    {
        currentFadeType = FadeType.FadeOut;
        NewLevelIndex = newLevelIndex;
        TransitionTimeElapsedMs = 0;
    }

    public void Update(int dtMs)
    {
        TransitionTimeElapsedMs += dtMs;
        if (TransitionTimeElapsedMs < TransitionDurationMs) return;
        if (currentFadeType == FadeType.FadeOut)
        {
            Level.Init(NewLevelIndex);
            Level.Context.LevelIsCompleted = false;
            currentFadeType = FadeType.FadeIn;
            TransitionTimeElapsedMs = 0;
        }
        else
        {
            currentFadeType = FadeType.None;
            TransitionTimeElapsedMs = 0;
        }
    }

    public void Draw(SpriteBatch sb)
    {
        if (currentFadeType == FadeType.None) return;
        float progress = MathHelper.Clamp((float)TransitionTimeElapsedMs / TransitionDurationMs, 0f, 1f);
        float opacity;
        if (currentFadeType == FadeType.FadeOut) opacity = progress;
        else opacity = 1f - progress;

        var viewport = sb.GraphicsDevice.Viewport;
        if (transitionPixel == null) CreateTransitionPixel(sb);
        sb.Draw(transitionPixel, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.Black * opacity);
    }

    private void CreateTransitionPixel(SpriteBatch sb)
    {
        transitionPixel = new Texture2D(sb.GraphicsDevice, 1, 1);
        transitionPixel.SetData([Color.White]);
    }
}