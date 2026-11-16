using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;

public class FadeTransition(SceneManager sceneManager) : ISceneTransition
{
    public int TransitionDurationMs { get; set; } = 600;
    public bool IsActive => currentTransitionType != TransitionType.None;
    private int TransitionTimeElapsedMs;
    private string SceneName;
    
    #nullable enable
    private Texture2D? transitionPixel;
    private enum TransitionType {None, TransitionIn, TransitionOut};
    private TransitionType currentTransitionType = TransitionType.None; 
   
    public void StartLevelTransition(string sceneName)
    {
        currentTransitionType = TransitionType.TransitionOut;
        SceneName = sceneName;
        TransitionTimeElapsedMs = 0;
    }

    public void Update(int dtMs)
    {
        TransitionTimeElapsedMs += dtMs;
        if (TransitionTimeElapsedMs < TransitionDurationMs) return;
        if (currentTransitionType == TransitionType.TransitionOut)
        {
            sceneManager.SwapScene(SceneName);
            currentTransitionType = TransitionType.TransitionIn;
            TransitionTimeElapsedMs = 0;
        }
        else
        {
            currentTransitionType = TransitionType.None;
            TransitionTimeElapsedMs = 0;
        }
    }

    public void Draw(SpriteBatch sb)
    {
        if (currentTransitionType == TransitionType.None) return;
        float progress = MathHelper.Clamp((float)TransitionTimeElapsedMs / TransitionDurationMs, 0f, 1f);
        float opacity;
        if (currentTransitionType == TransitionType.TransitionOut) opacity = progress;
        else opacity = 1f - progress;

        var viewport = sb.GraphicsDevice.Viewport;
        if (transitionPixel == null) CreateTransitionPixel(sb);
        sb.Draw(
            transitionPixel,
            new Rectangle(0, 0, viewport.Width, viewport.Height),
            null,
            Color.Black * opacity,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            1.0f);
    }

    private void CreateTransitionPixel(SpriteBatch sb)
    {
        transitionPixel = new Texture2D(sb.GraphicsDevice, 1, 1);
        transitionPixel.SetData([Color.White]);
    }
}