using System;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(IInputManager mouseInput, IAudioManager audioManager) : ISceneController
{
    public readonly static string Name = "Main Menu";
    private readonly static int PlayGameButtonX = 200, PlayGameButtonY = 200;
    private readonly static int ExitGameButtonX = 200, ExitGameButtonY = 300;

    private SceneManager SceneManager;
    private Button PlayGameButton;
    private Button ExitGameButton;

    public void Init(ISceneManager sm)
    {
        SceneManager = sm as SceneManager;
        PlayGameButton = new PlayGameButton(SceneManager, audioManager);
        PlayGameButton.Init(PlayGameButtonX, PlayGameButtonY, mouseInput);

        ExitGameButton = new ExitGameButton(SceneManager);
        ExitGameButton.Init(ExitGameButtonX, ExitGameButtonY, mouseInput);
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
