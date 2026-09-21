using System;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(KeyboardInputManager keyInput, MouseInputManager mouseInput) : ISceneController
{
    public readonly static string Name = "Main Menu";
    private SceneManager SceneManager;
    private readonly KeyboardInputManager KeyInput = keyInput;
    private readonly MouseInputManager MouseInput = mouseInput;
    private Button PlayGameButton;
    private Button ExitGameButton;

    public void Init(SceneManager sm)
    {
        SceneManager = sm;
        PlayGameButton = new PlayGameButton(sm);
        PlayGameButton.Init(200, 200, MouseInput);

        ExitGameButton = new ExitGameButton(sm);
        ExitGameButton.Init(200, 300, MouseInput);
    }

    public void Update(int dtMs)
    {
        PlayGameButton.Update(dtMs);
        ExitGameButton.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        PlayGameButton.Draw(sb);
        ExitGameButton.Draw(sb);
    }
}
