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
    private TileSpriteFactory TileSpriteFactory = new(textureAtlas);

    public void Init()
    {
        LevelFileParser = new LevelFileParser(ObjectFactory, TileFactory, ObjectSpriteFactory, TileSpriteFactory);
        Level = new PuzzleLevel(ObjectFactory, TileFactory, ObjectSpriteFactory, TileSpriteFactory, LevelFileParser);
        Level.Init(FileNames.Level1File);
    }
    public void Update(int dtMs)
    {
        if (buttonInput.IsPressed(InputNames.MoveNorth) || buttonInput.IsPressed(InputNames.MoveNorthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.North, ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveEast) || buttonInput.IsPressed(InputNames.MoveEastAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.East, ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveSouth) || buttonInput.IsPressed(InputNames.MoveSouthAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.South, ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveWest) || buttonInput.IsPressed(InputNames.MoveWestAlt))
        {
            Level.EnqueueEvent(new MovePlayerEvent(Directions.West, ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.Snap))
        {
            Level.EnqueueEvent(new SnapEvent(ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.UseItem))
        {
            Level.EnqueueEvent(new UseItemEventEvent());
        }
        else if (buttonInput.IsPressed(InputNames.Undo))
        {
            Level.EnqueueEvent(new UndoEvent());
        }
        Level.TryExecuteEvent();

        Level.Update(dtMs);
    }
    public void Draw(SpriteBatch sb)
    {
        Level.Draw(sb);
    }
}