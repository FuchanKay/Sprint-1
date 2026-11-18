using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameCreditsSceneController(ISceneManager sceneManager, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas, SpriteFontManager spriteFontManager, GameWindow window) : ISceneController
{
    private readonly static int ExitGameButtonX = 200, ExitGameButtonY = 400;
    private Button ExitGameButton;
    private BasicSpriteFontWriter GameCompleteMessage = new(spriteFontManager);

    public void Init()
    {
        ExitGameButton = new ExitGameButton(sceneManager, textureAtlas);
        ExitGameButton.Init(ExitGameButtonX, ExitGameButtonY, mouseInput, true);
        string gameCompleteText = "Thank you for playing!\n\nGame Created By:\nFuma\nMallory\nSadie\nYongan\nLucas";
        Vector2 messageSize = spriteFontManager.GetFont(FontNames.DungeonFont).MeasureString(gameCompleteText);
        Vector2 gameCompletePosition = new Vector2((window.ClientBounds.Width- messageSize.X) / 4f, (window.ClientBounds.Height - messageSize.Y)/4f);
        GameCompleteMessage.Init(FontNames.DungeonFont, gameCompletePosition, gameCompleteText, Color.Gold);
        audioManager.PlaySong(SoundNames.Song);
    }

    public void Update(int dtMs)
    {
        ExitGameButton.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        ExitGameButton.Draw(sb);
        GameCompleteMessage.Draw(sb);
    }
}
