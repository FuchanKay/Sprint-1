using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Scripts.GameComponents;
namespace Scripts.Game;

public class MainMenuSceneController(KeyboardInputManager keyInput, MouseInputManager mouseInput) : ISceneController
{
    private SceneManager SceneManager;
    private readonly KeyboardInputManager KeyInput = keyInput;
    private readonly MouseInputManager MouseInput = mouseInput;

    public static IObject _object;
    public void Init(SceneManager sm)
    {
        SceneManager = sm;
        _object = new StoneBlock(new Vector2(100, 100));
    }

    public void Update(int dtMs)
    {
        _object.Update(dtMs);

        //TODO: Example code. Remove this later
        var moveNorth = KeyInput.IsPressed("Move North");
        if (moveNorth)
        {
            Console.WriteLine("Moved North!");
        }

        var click = MouseInput.IsReleased("Select");
        if (click)
        {
            var x = MouseInput.X;
            var y = MouseInput.Y;
            Console.WriteLine($"Clicked at ({x}, {y})!");
        }
    }

    public void Draw(SpriteBatch sb)
    {
        _object?.Draw(sb);
    }
}
