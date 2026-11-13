using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MainMenuSceneController(ISceneManager sceneManager, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas, SpriteFontManager spriteFontManager) : ISceneController
{
    private readonly static int PlayGameButtonX = 500, PlayGameButtonY = 200;
    private readonly static int LevelEditorButtonX = 500, LevelEditorButtonY = 300;
    private readonly static int ExitGameButtonX = 500, ExitGameButtonY = 400;
    private Button PlayGameButton;
    private Button LevelEditorButton;
    private Button ExitGameButton;
    private BasicSpriteFontWriter GameTitle = new(spriteFontManager);



    public void Init()
    {

        PlayGameButton = new PlayGameButton(sceneManager, audioManager, textureAtlas);
        PlayGameButton.Init(PlayGameButtonX, PlayGameButtonY, mouseInput, true);

        LevelEditorButton = new LevelEditorButton(sceneManager, textureAtlas);
        LevelEditorButton.Init(LevelEditorButtonX, LevelEditorButtonY, mouseInput, true);

        ExitGameButton = new ExitGameButton(sceneManager, textureAtlas);
        ExitGameButton.Init(ExitGameButtonX, ExitGameButtonY, mouseInput, true);
        GameTitle.Init(FontNames.DungeonFont, new Vector2(1280 / 2 - 100, 10), "Sugar Snap", Color.Gold);

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
        GameTitle.Draw(sb);
    }
}
