using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(IInputManager buttonInput, IInputManager mouseInput) : ISceneController
{
    private ISceneManager SceneManager;
    private readonly IInputManager ButtonInput = buttonInput;
    private readonly IInputManager MouseInput = mouseInput;
    public readonly static string Name = "GamePlay";
    //placeholder mario texture to indicate that this is the gameplay scene
    public static Texture2D Mario { get; set; }

    public void Init(ISceneManager sm)
    {
        SceneManager = sm;
    }

    public void Update(int dtMs)
    {
        //TODO: Implement game logic here
    }

    public void Draw(SpriteBatch sb)
    {
        //TODO: Remove this after actual gameplay scenes get implemented
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
