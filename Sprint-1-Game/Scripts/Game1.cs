using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using Scripts.Game;
using Scripts.GameComponents;

namespace Sprint_1_Game.Scripts;

public class Game1 : Core
{
    private static readonly string Name = "Sprint-1-Game";
    private static readonly int ScreenWidth = 1280;
    private static readonly int ScreenHeight = 720;
    private static readonly bool IsFullScreen = false;
    private static readonly Color BackgroundColor = Color.White;
    private readonly SceneManager SceneManager;

    public Game1() : base(Name, ScreenWidth, ScreenHeight, IsFullScreen)
    {
        KeyboardInputManager keyInput = new();
        MouseInputManager mouseInput = new();
        SceneManager = new(keyInput, mouseInput);
    }

    protected override void Initialize()
    {
        BindAllTextures();
        SceneManager.Init();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        BindAllTextures();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        SceneManager.Update(gameTime.ElapsedGameTime.Milliseconds);
        if (SceneManager.ShouldExit)
        {
            Exit();
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        SpriteBatch.Begin();
        SceneManager.Draw(SpriteBatch);
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    private static void BindAllTextures()
    {
        PlayGameButton.ButtonTexture = Content.Load<Texture2D>("Images/play-button");
        ExitGameButton.ButtonTexture = Content.Load<Texture2D>("Images/exit-button");
        
        StoneBlock.objectTexture = Content.Load<Texture2D>("ObjectSprites/stoneBlock");
        Rock.objectTexture = Content.Load<Texture2D>("ObjectSprites/rock");
    }
}
