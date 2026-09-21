using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(KeyboardInputManager keyInput, MouseInputManager mouseInput) : ISceneController
{
    private SceneManager SceneManager;
    private readonly KeyboardInputManager KeyInput = keyInput;
    private readonly MouseInputManager MouseInput = mouseInput;
    public readonly static string Name = "GamePlay";
    //placeholder mario texture to indicate that this is the gameplay scene
    public static Texture2D Mario { get; set; }

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
        var rect = new Rectangle(0, 0, Mario.Width, Mario.Height);
        sb.Draw(
            Mario,
            Vector2.Zero,
            rect,
            Color.White,
            0.0f,
            Vector2.Zero,
            0.2f,
            SpriteEffects.None,
            0.0f
        );
    }
}
