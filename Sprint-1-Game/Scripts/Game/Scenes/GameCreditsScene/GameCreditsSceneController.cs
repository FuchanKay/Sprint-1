using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameCreditsSceneController(ISceneManager sceneManager, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas, SpriteFontManager spriteFontManager, GameWindow window) : ISceneController
{
    private readonly static int ExitGameButtonX = 200, ExitGameButtonY = 400;
    private Button ExitGameButton;
    private BasicSpriteFontWriter GameCompleteMessage = new(spriteFontManager);
    private ISprite SproutHeart = new StaticSprite(textureAtlas);

    public void Init()
    {
        audioManager.PlaySong(SoundNames.Song);
        ExitGameButton = new ExitGameButton(sceneManager, textureAtlas);
        ExitGameButton.Init(ExitGameButtonX, ExitGameButtonY, mouseInput, true);
        CreditsTextInit();
        SproutHeartInit();
    }

    public void Update(int dtMs)
    {
        ExitGameButton.Update(dtMs);
        SproutHeart.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        ExitGameButton.Draw(sb);
        GameCompleteMessage.Draw(sb);
        SproutHeart.Draw(sb);
    }

    private void CreditsTextInit()
    {
        string gameCompleteText = "Thank you for playing!\n\nGame Created By:\nFuma\nMallory\nSadie\nYongan\nLucas";
        Vector2 messageSize = spriteFontManager.GetFont(FontNames.DungeonFont).MeasureString(gameCompleteText);
        Vector2 gameCompletePosition = new Vector2((window.ClientBounds.Width - messageSize.X) / 4f, (window.ClientBounds.Height - messageSize.Y)/4f);
        GameCompleteMessage.Init(FontNames.DungeonFont, gameCompletePosition, gameCompleteText, Color.Gold);
        
    }

    private void SproutHeartInit()
    {
        SproutHeart.Texture = textureAtlas.GetTexture(TextureNames.CreditsSprout);
        SproutHeart.IdleName = RegionNames.DirtTile;
        SproutHeart.SetState(SproutHeart.IdleName);
        Vector2 sproutPosition = new((window.ClientBounds.Width * (3f / 5f)), (window.ClientBounds.Height) / 2f);
        SproutHeart.CurrentPosition = sproutPosition;
        SproutHeart.SetTargetPosition(sproutPosition);
        SproutHeart.LayerDepth = 0.95f;
        SproutHeart.Scale = 5.0f;
    }
}
