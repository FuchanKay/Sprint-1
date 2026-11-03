using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using Scripts.Game;
using Gum.Forms;
using Gum.Forms.Controls;
namespace Sprint_1_Game.Scripts;

public class Game1 : Core
{
    private static readonly string Name = "Sprint-1-Game";
    private static readonly int ScreenWidth = 1280;
    private static readonly int ScreenHeight = 720;
    private static readonly bool IsFullScreen = false;
    private static readonly Color BackgroundColor = Color.Black;
    private readonly Setup Setup;
    private SceneManager SceneManager;

    public Game1() : base(Name, ScreenWidth, ScreenHeight, IsFullScreen)
    {
        Setup = new(Content, Window);
    }

    protected override void Initialize()
    {
        Setup.Initialize(out var sceneManager);
        //InitializeGum();
        SceneManager = sceneManager;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Setup.LoadContent();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        SceneManager.Update(gameTime.ElapsedGameTime.Milliseconds);
        if (SceneManager.ShouldExit)
        {
            Exit();
        }
        if (SceneManager.ShouldRestart)
        {
            Setup.Initialize(out var sm);
            SceneManager = sm;
            Setup.LoadContent();
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        SpriteBatch.Begin();

        // TEST (random destination)
        SceneManager.Draw(SpriteBatch);
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    
    private void InitializeGum()
{
    // Initialize the Gum service. The second parameter specifies
    // the version of the default visuals to use. V3 is the latest
    // version.
    Gum.GumService.Default.Initialize(this, DefaultVisualsVersion.V3);

    // Tell the Gum service which content manager to use. We will tell it to
    // use the global content manager from our Core.
    Gum.GumService.Default.ContentLoader.XnaContentManager = Core.Content;

    // Register keyboard input for UI control.
    FrameworkElement.KeyboardsForUiControl.Add(Gum.GumService.Default.Keyboard);

    // Register gamepad input for Ui control.
    FrameworkElement.GamePadsForUiControl.AddRange(Gum.GumService.Default.Gamepads);

    // Customize the tab reverse UI navigation to also trigger when the keyboard
    // Up arrow key is pushed.
    FrameworkElement.TabReverseKeyCombos.Add(
       new KeyCombo() { PushedKey = (Gum.Forms.Input.Keys)Microsoft.Xna.Framework.Input.Keys.Up });

    // Customize the tab UI navigation to also trigger when the keyboard
    // Down arrow key is pushed.
    FrameworkElement.TabKeyCombos.Add(
       new KeyCombo() { PushedKey = (Gum.Forms.Input.Keys)Microsoft.Xna.Framework.Input.Keys.Down });

    // The assets created for the UI were done so at 1/4th the size to keep the size of the
    // texture atlas small.  So we will set the default canvas size to be 1/4th the size of
    // the game's resolution then tell gum to zoom in by a factor of 4.
    Gum.GumService.Default.CanvasWidth = GraphicsDevice.PresentationParameters.BackBufferWidth / 4.0f;
    Gum.GumService.Default.CanvasHeight = GraphicsDevice.PresentationParameters.BackBufferHeight / 4.0f;
    Gum.GumService.Default.Renderer.Camera.Zoom = 4.0f;
}

}
