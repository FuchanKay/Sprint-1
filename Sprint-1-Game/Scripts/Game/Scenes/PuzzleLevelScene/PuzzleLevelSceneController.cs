using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class PuzzleLevelSceneController(IInputManager buttonInput, ITextureAtlas textureAtlas) : ISceneController
{
    private PuzzleLevel Level;
    private ObjectFactory ObjectFactory = new();
    private TileFactory TileFactory = new();
    private ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private TileSpriteFactory TileSpriteFactory = new(textureAtlas);

    public void Init()
    {
        Level = new(ObjectFactory, TileFactory, ObjectSpriteFactory, TileSpriteFactory);
        Level.Init();
    }
    public void Update(int dtMs)
    {
        if (buttonInput.IsPressed(InputNames.MoveNorth) || buttonInput.IsPressed(InputNames.MoveNorthAlt))
        {
            Level.EnqueueEvent(new MoveNorthEvent(ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveEast) || buttonInput.IsPressed(InputNames.MoveEastAlt))
        {
            Level.EnqueueEvent(new MoveEastEvent(ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveSouth) || buttonInput.IsPressed(InputNames.MoveSouthAlt))
        {
            Level.EnqueueEvent(new MoveSouthEvent(ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.MoveWest) || buttonInput.IsPressed(InputNames.MoveWestAlt))
        {
            Level.EnqueueEvent(new MoveWestEvent(ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.Snap))
        {
            Level.EnqueueEvent(new SnapEvent());
        }
        else if (buttonInput.IsPressed(InputNames.UseItem))
        {
            Level.EnqueueEvent(new UseItemEventEvent());
        }
        else if (buttonInput.IsPressed(InputNames.Undo))
        {
            Level.EnqueueEvent(new UndoEvent());
        }
        else if (buttonInput.IsPressed(InputNames.CycleObjectLeft))
        {
            Level.EnqueueEvent(new CycleObjectLeftEvent(ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.CycleObjectRight))
        {
            Level.EnqueueEvent(new CycleObjectRightEvent(ObjectFactory));
        }
        else if (buttonInput.IsPressed(InputNames.CycleTileLeft))
        {
            Level.EnqueueEvent(new CycleTileLeftEvent(TileFactory));
        }
        else if (buttonInput.IsPressed(InputNames.CycleTileRight))
        {
            Level.EnqueueEvent(new CycleTileRightEvent(TileFactory));
        }
        else if (buttonInput.IsPressed(InputNames.CycleEnemyLeft))
        {
            Level.EnqueueEvent(new CycleEnemyLeftEvent());
        }
        else if (buttonInput.IsPressed(InputNames.CycleEnemyRight))
        {
            Level.EnqueueEvent(new CycleEnemyRightEvent());
        }
        Level.TryExecuteEvent();

        Level.Update(dtMs);
    }
    public void Draw(SpriteBatch sb)
    {
        Level.Draw(sb);
    }
}