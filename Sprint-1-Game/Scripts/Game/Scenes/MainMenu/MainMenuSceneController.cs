using System;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(KeyboardInputManager keyInput, MouseInputManager mouseInput) : ISceneController
{
    public readonly static string Name = "Main Menu";
    private readonly static int PlayGameButtonX = 200;
    private readonly static int PlayGameButtonY = 200;
    private readonly static int ExitGameButtonX = 200;
    private readonly static int ExitGameButtonY = 300;

    private SceneManager SceneManager;
    private readonly KeyboardInputManager KeyInput = keyInput;
    private readonly MouseInputManager MouseInput = mouseInput;
    private Button PlayGameButton;
    private Button ExitGameButton;

    public void Init(ISceneManager sm)
    {
        SceneManager = sm as SceneManager;
        PlayGameButton = new PlayGameButton(SceneManager);
        PlayGameButton.Init(PlayGameButtonX, PlayGameButtonY, MouseInput);

        ExitGameButton = new ExitGameButton(SceneManager);
        ExitGameButton.Init(ExitGameButtonX, ExitGameButtonY, MouseInput);
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
