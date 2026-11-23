using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameCreditsSceneController(ISceneManager sceneManager, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas, UIFontAtlas uIFontAtlas, GameWindow window) : ISceneController
{
    private readonly static int ExitGameButtonX = 200, ExitGameButtonY = 400;
    private Button ExitGameButton;
    private UIFont GameCompleteMessage = new(uIFontAtlas);
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
        GameCompleteMessage.Init(FontNames.DungeonFont);
        GameCompleteMessage.Text = "Thank you for playing!\n\nGame Created By:\nFuma\nMallory\nSadie\nYongan\nLucas";
        GameCompleteMessage.Pos = new Vector2((window.ClientBounds.Width - GameCompleteMessage.Size.X) / 4f, (window.ClientBounds.Height - GameCompleteMessage.Size.Y)/4f);
        GameCompleteMessage.Color = Color.Gold;
        
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
