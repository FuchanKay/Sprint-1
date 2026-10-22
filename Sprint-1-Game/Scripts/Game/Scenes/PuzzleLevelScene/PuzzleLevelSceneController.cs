using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class PuzzleLevelSceneController(IInputManager buttonInput, ITextureAtlas textureAtlas) : ISceneController
{
    private PuzzleLevel Level;
    private LevelFileParser LevelFileParser;
    private ObjectFactory ObjectFactory = new();
    private TileFactory TileFactory = new();
    private ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private TileSpriteFactory TileSpriteFactory;

    public void Init()
    {
        TileSpriteFactory = new(textureAtlas, TileFactory);
        
        LevelFileParser = new LevelFileParser(ObjectFactory, TileFactory, ObjectSpriteFactory, TileSpriteFactory);
        Level = new PuzzleLevel(ObjectFactory, TileFactory, ObjectSpriteFactory, TileSpriteFactory, LevelFileParser);
        Level.Init(FileNames.Level1File);
    }
    public void Update(int dtMs)
    {
        var lockMove = buttonInput.IsHeld(InputNames.LShift);
        if (buttonInput.IsPressed(InputNames.MoveNorth) || buttonInput.IsPressed(InputNames.MoveNorthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.North, lockMove, ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveEast) || buttonInput.IsPressed(InputNames.MoveEastAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.East, lockMove, ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveSouth) || buttonInput.IsPressed(InputNames.MoveSouthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.South, lockMove, ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveWest) || buttonInput.IsPressed(InputNames.MoveWestAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.West, lockMove, ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.Snap))
        {
            Level.EnqueueEvent(new SnapEvent(ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.UseItem))
        {
            Level.EnqueueEvent(new UseItemEventEvent());
        }
        else if (buttonInput.IsPressed(InputNames.ResetLevel))
        {
            Level.RestartLevel();
        }
        else if (buttonInput.IsPressed(InputNames.Undo))
        {
            Level.Undo();
        }
        Level.TryExecuteEvent();

        Level.Update(dtMs);
    }
    public void Draw(SpriteBatch sb)
    {
        Level.Draw(sb);
    }
}