using System;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(KeyboardInputManager keyInput, MouseInputManager mouseInput) : ISceneController
{
    private SceneManager SceneManager;
    private readonly KeyboardInputManager KeyInput = keyInput;
    private readonly MouseInputManager MouseInput = mouseInput;
    public readonly static string Name = "Main Menu";
    public void Init(SceneManager sm)
    {
        SceneManager = sm;
    }

    public void Update(int dtMs)
    {
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
        
    }
}
