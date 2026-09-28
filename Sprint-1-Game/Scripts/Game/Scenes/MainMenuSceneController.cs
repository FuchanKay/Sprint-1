using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(KeyboardInputManager keyInput, MouseInputManager mouseInput) : ISceneController
{
    private SceneManager SceneManager;
    private readonly KeyboardInputManager KeyInput = keyInput;
    private readonly MouseInputManager MouseInput = mouseInput;
    private Player Player1;
    public void Init(SceneManager sm)
    {
        Player1 = new Player(new Vector2(300,300), 8f, Direction.East);
        SceneManager = sm;
    }

    public void Update(int dtMs)
    {
        //TODO: Example code. Remove this later
        var moveNorth = KeyInput.IsPressed("Move North");
        if (moveNorth)
        {
            Console.WriteLine("Moved North!");
            Player1.MoveNorth();
            Console.WriteLine(Player1.Pos);
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
