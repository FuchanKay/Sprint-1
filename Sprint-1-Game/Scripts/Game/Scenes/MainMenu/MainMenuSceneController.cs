using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(ISceneManager sceneManager, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas) : ISceneController
{
    private readonly static int PlayGameButtonX = 500, PlayGameButtonY = 200;
    private readonly static int LevelEditorButtonX = 500, LevelEditorButtonY = 300;
    private readonly static int ExitGameButtonX = 500, ExitGameButtonY = 400;
    private Button PlayGameButton;
    private Button LevelEditorButton;
    private Button ExitGameButton;

    public void Init()
    {
        PlayGameButton = new PlayGameButton(sceneManager, audioManager, textureAtlas);
        PlayGameButton.Init(PlayGameButtonX, PlayGameButtonY, mouseInput, true);

        LevelEditorButton = new LevelEditorButton(sceneManager, textureAtlas);
        LevelEditorButton.Init(LevelEditorButtonX, LevelEditorButtonY, mouseInput, true);

        ExitGameButton = new ExitGameButton(sceneManager, textureAtlas);
        ExitGameButton.Init(ExitGameButtonX, ExitGameButtonY, mouseInput, true);
    }

    public void Update(int dtMs)
    {
        PlayGameButton.Update(dtMs);
        ExitGameButton.Update(dtMs);
        LevelEditorButton.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        PlayGameButton.Draw(sb);
        ExitGameButton.Draw(sb);
        LevelEditorButton.Draw(sb);
    }
}
