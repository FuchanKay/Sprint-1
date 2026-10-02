using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;
public class PuzzleLevelSceneController(ISceneManager sceneManager, IInputManager buttonInput, IAudioManager audioManager, ITextureAtlas textureAtlas) : ISceneController
{
    private PuzzleLevel Level; 

    public void Init()
    {
        Level = new();
    }
    public void Update(int dtMs)
    {   
        if (buttonInput.IsPressed(InputNames.MoveNorth))
        {
            Level.EnqueueEvent(new MoveNorthEvent());
        }
        else if (buttonInput.IsPressed(InputNames.MoveEast))
        {
            
        }
        else if (buttonInput.IsPressed(InputNames.MoveSouth))
        {
            
        }
        else if (buttonInput.IsPressed(InputNames.MoveWest))
        {
            
        }
        else if (buttonInput.IsPressed(InputNames.Snap))
        {
            
        }
        else if (buttonInput.IsPressed(InputNames.UseItem))
        {
            
        }
        else if (buttonInput.IsPressed(InputNames.Undo))
        {
            
        }
    }
    public void Draw(SpriteBatch sb)
    {
    }
}