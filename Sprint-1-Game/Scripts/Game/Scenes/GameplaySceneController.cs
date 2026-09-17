using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(KeyboardInputManager keyInput, MouseInputManager mouseInput) : ISceneController
{
    private SceneManager SceneManager;
    private readonly KeyboardInputManager KeyInput = keyInput;
    private readonly MouseInputManager MouseInput = mouseInput;
    public void Init(SceneManager sm)
    {
        SceneManager = sm;
    }

    public void Update(int dtMs)
    {
        //TODO: Implement game logic here
    }

    public void Draw(SpriteBatch sb)
    {
        //TODO: Implement drawing here
    }
}
