using System.Collections.Generic;
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
    private Object _exampleObject;
    private HashSet<Object> objects;
    private int timer = 0;

    public void Init(ISceneManager sm)
    {
        SceneManager = sm as SceneManager;

        objects = new HashSet<Object>();

        _exampleObject = new StoneBlock();
        _exampleObject.Init(new Vector2(300, 100));
        objects.Add(_exampleObject);

    }

    public void Update(int dtMs)
    {
        // This is placeholder code that cycles an object through each object type for demonstration purposes
        Vector2 currentPosition = _exampleObject.position;
        timer++;
        switch (timer)
        {
            case 1:
                _exampleObject = new StoneBlock();
                _exampleObject.Init(currentPosition);
                break;
            case 100:
                _exampleObject = new Rock();
                _exampleObject.Init(currentPosition);
                break;
            case 200:
                _exampleObject = new StoneWall();
                _exampleObject.Init(currentPosition);
                break;
            case 300:
                _exampleObject = new Bomb();
                _exampleObject.Init(currentPosition);
                break;
            case 400:
                _exampleObject = new ExitDoor();
                _exampleObject.Init(currentPosition);
                break;
            case 500:
                timer = 0;
                break;
            default:
                break;
        }

        CheckInputs(_exampleObject);
        _exampleObject.Update(dtMs);
    }

    private void CheckInputs(Object obj)
    {
        if(ButtonInput.IsPressed("Destroy")) obj.Destroy();
        if(ButtonInput.IsPressed("Snap")) obj.SnapBehavior();
        if(ButtonInput.IsPressed("Move East")) obj.MoveRight();
        if(ButtonInput.IsPressed("Move West")) obj.MoveLeft();
        if(ButtonInput.IsPressed("Move North")) obj.MoveUp();
        if(ButtonInput.IsPressed("Move South")) obj.MoveDown();
    }

    public void Draw(SpriteBatch sb)
    {
        _exampleObject.Draw(sb);
    }
}
