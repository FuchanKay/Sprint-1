using System;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(ISceneManager sceneManager, IInputManager mouseInput, IAudioManager audioManager) : ISceneController
{
    public readonly static string Name = "Main Menu";
    private readonly static int PlayGameButtonX = 200, PlayGameButtonY = 200;
    private readonly static int ExitGameButtonX = 200, ExitGameButtonY = 300;
    private Button PlayGameButton;
    private Button ExitGameButton;

    public void Init()
    {
        PlayGameButton = new PlayGameButton(sceneManager, audioManager);
        PlayGameButton.Init(PlayGameButtonX, PlayGameButtonY, mouseInput);

        ExitGameButton = new ExitGameButton(sceneManager);
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
