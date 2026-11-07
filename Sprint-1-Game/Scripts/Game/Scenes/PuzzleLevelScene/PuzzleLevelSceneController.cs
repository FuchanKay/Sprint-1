using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class PuzzleLevelSceneController(ISceneManager sceneManager, IInputManager buttonInput, IAudioManager audioManager, ITextureAtlas textureAtlas, SpriteFontManager spriteFontManager, GameWindow window) : ISceneController
{
    private PuzzleLevel Level;
    private LevelFileParser LevelFileParser;
    private readonly ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private readonly TileSpriteFactory TileSpriteFactory = new(textureAtlas);
    private readonly EffectSpriteFactory EffectSpriteFactory = new(textureAtlas);
    private GameLayoutManager GameLayoutManager = new();
    private BasicSpriteFontWriter EnemyCounter = new (spriteFontManager);
    private BasicSpriteFontWriter InventoryStatus = new (spriteFontManager);

    private enum FadeType {None, FadeIn, FadeOut};
    private const int FadeDurationMs = 400;
    private FadeType currentFadeType;
    private int fadeTimeElapsedMs;
    #nullable enable
    private Texture2D? transitionPixel;


    private readonly IAudioManager AudioManager = audioManager;
    public int CurrentLevelIndex = 0;

    public void Init()
    {
        GameLayoutManager.Init(window);
        LevelFileParser = new LevelFileParser();
        Level = new PuzzleLevel(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory, LevelFileParser, AudioManager, GameLayoutManager);
        Level.Init(CurrentLevelIndex);
        EnemyCounter.Init(FontNames.DefaultFont, new Vector2 (1040,40), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}", Color.White);
        InventoryStatus.Init(FontNames.DefaultFont, new Vector2(1040,80), $"Inventory:\n{Level.LevelStateHistory.InventoryManager.ReadInventoryString()}", Color.White);
    }
    public void Update(int dtMs)
    {
        var lockMove = buttonInput.IsHeld(InputNames.LShift);
        if (buttonInput.IsPressed(InputNames.MoveNorth) || buttonInput.IsPressed(InputNames.MoveNorthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.North, lockMove));
        }
        else if (buttonInput.IsPressed(InputNames.MoveEast) || buttonInput.IsPressed(InputNames.MoveEastAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.East, lockMove));
        }
        else if (buttonInput.IsPressed(InputNames.MoveSouth) || buttonInput.IsPressed(InputNames.MoveSouthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.South, lockMove));
        }
        else if (buttonInput.IsPressed(InputNames.MoveWest) || buttonInput.IsPressed(InputNames.MoveWestAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.West, lockMove));
        }
        else if (buttonInput.IsPressed(InputNames.ChangeLevel))
        {
            CurrentLevelIndex = (CurrentLevelIndex + 1) % FileNames.LevelFileNames.Length;
            Level.Init(CurrentLevelIndex);
        }
        else if (buttonInput.IsPressed(InputNames.Snap))
        {
            Level.EnqueueEvent(new SnapEvent());
        }
        else if (buttonInput.IsPressed(InputNames.UseItem1))
        {
            Level.EnqueueEvent(new UseItemEvent(0));
        }
        else if (buttonInput.IsPressed(InputNames.UseItem2))
        {
            Level.EnqueueEvent(new UseItemEvent(1));

        }
        else if (buttonInput.IsPressed(InputNames.UseItem3))
        {
            Level.EnqueueEvent(new UseItemEvent(2));

        }
        else if (buttonInput.IsPressed(InputNames.PickItem))
        {
            Level.EnqueueEvent(new PickItemEvent());

        }
        else if (buttonInput.IsPressed(InputNames.ResetLevel))
        {
            Level.RestartLevel();
        }
        else if (buttonInput.IsPressed(InputNames.Undo))
        {
            Level.Undo();
        }

        Level.Update(dtMs);
        EnemyCounter.Update(new Vector2 (1040,40), $"Enemy remains: {Level.LevelStateHistory.GridPointer.EnemyCollection.Count}");
        InventoryStatus.Update(new Vector2(1040,80), $"Inventory:\n{Level.LevelStateHistory.InventoryManager.ReadInventoryString()}");


        if (currentFadeType != FadeType.None)
        {
            UpdateLevelTransition(dtMs);
        }

        if (Level.Context.LevelIsCompleted && currentFadeType == FadeType.None)
        {
            currentFadeType = FadeType.FadeOut;
            fadeTimeElapsedMs = 0;
        }
    }
    public void Draw(SpriteBatch sb)
    {
        EnemyCounter.Draw(sb);
        InventoryStatus.Draw(sb);
        Level.Draw(sb);

        if (currentFadeType == FadeType.None) return;

        // Clamp ensures progress is always between 0 and 1, even if fadeTimeElapsedMs is greater than FadeDurationMs
        float progress = MathHelper.Clamp((float)fadeTimeElapsedMs / FadeDurationMs, 0f, 1f);

        float opacity;
        if (currentFadeType == FadeType.FadeOut) opacity = progress;
        else opacity = 1f - progress;

        var viewport = sb.GraphicsDevice.Viewport;
        if (transitionPixel == null) CreateTransitionPixel(sb);
        sb.Draw(transitionPixel, new Rectangle(0, 0, viewport.Width, viewport.Height), Color.Black * opacity);
    }

    private void UpdateLevelTransition(int dtMs)
    {
        fadeTimeElapsedMs += dtMs;
        if (fadeTimeElapsedMs < FadeDurationMs) return;
        if (currentFadeType == FadeType.FadeOut)
        {
            CurrentLevelIndex = (CurrentLevelIndex + 1) % FileNames.LevelFileNames.Length;
            Level.Init(CurrentLevelIndex);
            currentFadeType = FadeType.FadeIn;
            fadeTimeElapsedMs = 0;
        }
        else
        {
            currentFadeType = FadeType.None;
            fadeTimeElapsedMs = 0;
        }
    }

    private void CreateTransitionPixel(SpriteBatch sb)
    {
        transitionPixel = new Texture2D(sb.GraphicsDevice, 1, 1);
        transitionPixel.SetData([Color.White]);
    }
}