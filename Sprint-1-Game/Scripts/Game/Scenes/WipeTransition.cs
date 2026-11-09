using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;

public class WipeTransition(PuzzleLevel Level) : ILevelTransition
{
    public int TransitionDurationMs { get; set; } = 400;
    public bool IsActive => currentTransitionType != TransitionType.None;
    private int TransitionTimeElapsedMs;
    private int NewLevelIndex;
    
    #nullable enable
    private Texture2D? transitionPixel;
    private enum TransitionType {None, TransitionIn, TransitionOut};
    private TransitionType currentTransitionType = TransitionType.None; 
    private Directions direction = Directions.West;
   
    public void StartLevelTransition(int newLevelIndex)
    {
        currentTransitionType = TransitionType.TransitionOut;
        NewLevelIndex = newLevelIndex;
        TransitionTimeElapsedMs = 0;
        var gridPointer = Level.LevelStateHistory.GridPointer;
        direction = gridPointer.GetObject(gridPointer.GetPlayerCoord()).Direction;
    }

    public void Update(int dtMs)
    {
        TransitionTimeElapsedMs += dtMs;
        if (TransitionTimeElapsedMs < TransitionDurationMs) return;
        if (currentTransitionType == TransitionType.TransitionOut)
        {
            Level.Init(NewLevelIndex);
            Level.Context.LevelIsCompleted = false;
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
        var viewport = sb.GraphicsDevice.Viewport;

        Rectangle wipeRectangle = new Rectangle(0, 0, viewport.Width, viewport.Height);

        switch (direction)
        {
            case Directions.South:
                int height = (int)(viewport.Height * progress);
                if (currentTransitionType == TransitionType.TransitionOut) wipeRectangle = new Rectangle(0, 0, viewport.Width, height);
                else wipeRectangle = new Rectangle(0, height, viewport.Width, viewport.Height - height);
                break;
            case Directions.North:
                height = (int)(viewport.Height * progress);
                if (currentTransitionType == TransitionType.TransitionOut) wipeRectangle = new Rectangle(0, viewport.Height - height, viewport.Width, height);
                else wipeRectangle = new Rectangle(0, 0, viewport.Width, viewport.Height - height);
                break;
            case Directions.West:
                int width = (int)(viewport.Width * progress);
                if (currentTransitionType == TransitionType.TransitionOut) wipeRectangle = new Rectangle(viewport.Width - width, 0, width, viewport.Height);
                else wipeRectangle = new Rectangle(0, 0, viewport.Width - width, viewport.Height);
                break;
            case Directions.East:
                width = (int)(viewport.Width * progress);
                if (currentTransitionType == TransitionType.TransitionOut) wipeRectangle = new Rectangle(0, 0, width, viewport.Height);
                else wipeRectangle = new Rectangle(width, 0, viewport.Width - width, viewport.Height);
                break;
        }

        if (transitionPixel == null) CreateTransitionPixel(sb);
        sb.Draw(transitionPixel, wipeRectangle, Color.Black);
    }

    private void CreateTransitionPixel(SpriteBatch sb)
    {
        transitionPixel = new Texture2D(sb.GraphicsDevice, 1, 1);
        transitionPixel.SetData([Color.White]);
    }
}