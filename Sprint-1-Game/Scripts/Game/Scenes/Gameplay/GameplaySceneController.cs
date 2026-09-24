using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(IInputManager buttonInput, IInputManager mouseInput) : ISceneController
{
    private SceneManager SceneManager;
    private readonly IInputManager ButtonInput = buttonInput;
    private readonly IInputManager MouseInput = mouseInput;
    public readonly static string Name = "GamePlay";
    //placeholder mario texture to indicate that this is the gameplay scene
    public static Texture2D Mario { get; set; }
    private Object _object;

    public void Init(ISceneManager sm)
    {
        SceneManager = sm as SceneManager;
        _object = new StoneBlock(SceneManager);
        _object.Init(new Vector2(300, 100));
    }

    public void Update(int dtMs)
    {
        //TODO: Implement game logic here
        if(!_object.isDestroyed) {
            _object.Update(dtMs);
            if(ButtonInput.IsPressed("Destroy")) _object.Destroy();
            if(ButtonInput.IsPressed("Move East")) _object.MoveRight();
            if(ButtonInput.IsPressed("Move West")) _object.MoveLeft();
            if(ButtonInput.IsPressed("Move North")) _object.MoveUp();
            if(ButtonInput.IsPressed("Move South")) _object.MoveDown();
        }
    }

    public void Draw(SpriteBatch sb)
    {

        if(!_object.isDestroyed) 
        {
            _object.Draw(sb);
        }
    }
}
