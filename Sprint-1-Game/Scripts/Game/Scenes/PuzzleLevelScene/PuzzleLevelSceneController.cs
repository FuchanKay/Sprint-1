using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class PuzzleLevelSceneController(ISceneManager sceneManager, IInputManager buttonInput, ITextureAtlas textureAtlas) : ISceneController
{
    private PuzzleLevel Level;
    private LevelFileParser LevelFileParser;
    private readonly ObjectFactory ObjectFactory = new();
    private readonly TileFactory TileFactory = new();
    private readonly ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private readonly TileSpriteFactory TileSpriteFactory = new(textureAtlas);
    public int CurrentLevelIndex = 0;

    public void Init()
    {
        LevelFileParser = new LevelFileParser();
        Level = new PuzzleLevel(ObjectFactory, TileFactory, ObjectSpriteFactory, TileSpriteFactory, LevelFileParser);
        Level.Init(CurrentLevelIndex);
    }
    public void Update(int dtMs)
    {
        var lockMove = buttonInput.IsHeld(InputNames.LShift);
        if (buttonInput.IsPressed(InputNames.MoveNorth) || buttonInput.IsPressed(InputNames.MoveNorthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.North, lockMove, ObjectFactory, TileFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveEast) || buttonInput.IsPressed(InputNames.MoveEastAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.East, lockMove, ObjectFactory, TileFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveSouth) || buttonInput.IsPressed(InputNames.MoveSouthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.South, lockMove, ObjectFactory, TileFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveWest) || buttonInput.IsPressed(InputNames.MoveWestAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.West, lockMove, ObjectFactory, TileFactory));
        }
        else if (buttonInput.IsPressed(InputNames.ChangeLevel))
        {
            CurrentLevelIndex = (CurrentLevelIndex + 1) % FileNames.LevelFileNames.Length;
            Level.Init(CurrentLevelIndex);
        }
        else if (buttonInput.IsPressed(InputNames.Snap))
        {
            Level.EnqueueEvent(new SnapEvent(ObjectFactory, TileFactory));
        }
        else if (buttonInput.IsPressed(InputNames.UseItem1))
        {
            Level.EnqueueEvent(new UseItemEvent(0, ObjectSpriteFactory));
        }
        else if (buttonInput.IsPressed(InputNames.UseItem2))
        {
            Level.EnqueueEvent(new UseItemEvent(1, ObjectSpriteFactory));

        }
        else if (buttonInput.IsPressed(InputNames.UseItem3))
        {
            Level.EnqueueEvent(new UseItemEvent(2, ObjectSpriteFactory));

        }
        else if (buttonInput.IsPressed(InputNames.PickItem))
        {
            Level.EnqueueEvent(new PickItemEvent(ObjectFactory, TileFactory));

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
        Level.Draw(sb);
    }
}