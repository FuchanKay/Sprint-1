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


        // Exits to the main menu upon a levels completion
        if (Level.Context.LevelIsCompleted)
        {
            CurrentLevelIndex = (CurrentLevelIndex + 1) % FileNames.LevelFileNames.Length;
            Level.Init(CurrentLevelIndex);
            Level.Context.LevelIsCompleted = false;
        }
    }
    public void Draw(SpriteBatch sb)
    {
        EnemyCounter.Draw(sb);
        InventoryStatus.Draw(sb);
        Level.Draw(sb);
    }
}