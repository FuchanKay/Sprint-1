using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class PuzzleLevelSceneController(ISceneManager sceneManager, IInputManager buttonInput, IAudioManager audioManager, ITextureAtlas textureAtlas) : ISceneController
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
        if (buttonInput.IsPressed(InputNames.MoveNorth))
        {
            Level.EnqueueEvent(new MoveNorthEvent());
        }
        else if (buttonInput.IsPressed(InputNames.MoveEast))
        {
            Level.EnqueueEvent(new MoveEastEvent());
        }
        else if (buttonInput.IsPressed(InputNames.MoveSouth))
        {
            Level.EnqueueEvent(new MoveSouthEvent());
        }
        else if (buttonInput.IsPressed(InputNames.MoveWest))
        {
            Level.EnqueueEvent(new MoveWestEvent());
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
        Level.ExecuteEvent();

        Level.Update(dtMs);
    }
    public void Draw(SpriteBatch sb)
    {
        Level.Draw(sb);
    }
}