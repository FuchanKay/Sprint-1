using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary;
using Scripts.Game;
using Scripts.GameComponents;

namespace Sprint_1_Game;

public class Game1 : Core
{
    private static readonly string name = "Sprint-1-Game";
    private static readonly int screenWidth = 1280;
    private static readonly int screenHeight = 720;
    private static readonly bool isFullScreen = false;
    private static readonly Color backgroundColor = Color.Aquamarine;
    private readonly GraphicsDeviceManager graphics;
    private readonly SpriteBatch spriteBatch;
    private readonly SceneManager sceneManager;

    private readonly KeyboardInputManager keyInput;
    private readonly MouseInputManager mouseInput;

    public Game1() : base(name, screenWidth, screenHeight, isFullScreen)
    {
        keyInput = new();
        mouseInput = new();
        sceneManager = new();
    }

    protected override void Initialize()
    {
        sceneManager.Init();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        sceneManager.Update(gameTime.ElapsedGameTime.Milliseconds);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(backgroundColor);

        spriteBatch.Begin();

        sceneManager.Draw(spriteBatch);

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
