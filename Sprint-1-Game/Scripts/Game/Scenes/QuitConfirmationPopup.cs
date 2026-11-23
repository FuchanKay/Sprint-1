using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;
using Scripts.GameComponents;

public class QuitConfirmationPopup (SceneManager sceneManager, IInputManager buttonInput, UIFontAtlas uIFontAtlas, GameWindow window)
{
    private UIFont ConfirmationText = new(uIFontAtlas);
    #nullable enable
    private Texture2D? Background;
    public bool IsOnscreen;

    public void Init()
    {
        IsOnscreen = false;
        ConfirmationText.Init(FontNames.DungeonFont);
        ConfirmationText.Text = "Are You Sure You Want To Quit? (Press Quit Again To Exit or Esc to Resume)";
        ConfirmationText.Pos = new Vector2((window.ClientBounds.Width - ConfirmationText.Size.X) / 2f, (window.ClientBounds.Height - ConfirmationText.Size.Y) / 2f);
        ConfirmationText.Color = Color.White;
    }

    public void Update()
    {
        if (!IsOnscreen) return;
        if (buttonInput.IsPressed(InputNames.ExitGame)) sceneManager.ExitGame();
        else if(buttonInput.IsPressed(InputNames.Resume))
        {
            IsOnscreen = false;
            sceneManager.currentGameState = SceneManager.GameStates.Play;
        }
    }

    public void Draw(SpriteBatch sb)
    {
        if (!IsOnscreen) return;
        if(Background == null) CreateBackground(sb);
        var viewport = sb.GraphicsDevice.Viewport;
        sb.Draw(
            Background,
            new Rectangle(0, 0, viewport.Width, viewport.Height),
            null,
            Color.Black * 0.5f,
            0f,
            Vector2.Zero,
            SpriteEffects.None,
            0.99f);

        ConfirmationText.Draw(sb);
    }

    private void CreateBackground(SpriteBatch sb)
    {
        Background = new Texture2D(sb.GraphicsDevice, 1, 1);
        Background.SetData([Color.White]);
    }
}