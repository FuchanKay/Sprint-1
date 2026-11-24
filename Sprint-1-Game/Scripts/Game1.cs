using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using Scripts.Game;
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
    Gum.GumService GumUI => Gum.GumService.Default;
    public Game1() : base(Name, ScreenWidth, ScreenHeight, IsFullScreen)
    {
        Setup = new(Content, Window);
    }

    protected override void Initialize()
    {
        Setup.Initialize(out var sceneManager);
        SceneManager = sceneManager;

        GumUI.Initialize(this, "GUMProject/GUMProject.gumj");

        base.Initialize();
        InitializeGum();
    }

    protected override void LoadContent()
    {
        Setup.LoadContent();
        base.LoadContent();
        SceneManager.Init();
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
            SceneManager.Init();
        }
        GumUI.Update(gameTime);


        Gum.GumService.Default.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        SpriteBatch.Begin(SpriteSortMode.FrontToBack);
        SceneManager.Draw(SpriteBatch);
        SpriteBatch.End();

        Gum.GumService.Default.Draw();
        SpriteBatch.Begin(SpriteSortMode.FrontToBack);
        SceneManager.DrawOverGUM(SpriteBatch);
        SpriteBatch.End();
        base.Draw(gameTime);
    }

    private void InitializeGum()
    {
        //Gum.GumService.Default.Initialize(this, DefaultVisualsVersion.V3);
        Gum.GumService.Default.ContentLoader.XnaContentManager = Core.Content;

        FrameworkElement.KeyboardsForUiControl.Add(Gum.GumService.Default.Keyboard);
        FrameworkElement.GamePadsForUiControl.AddRange(Gum.GumService.Default.Gamepads);
        FrameworkElement.TabReverseKeyCombos.Add(
        new KeyCombo() { PushedKey = Gum.Forms.Input.Keys.Up });
        FrameworkElement.TabKeyCombos.Add(
        new KeyCombo() { PushedKey = Gum.Forms.Input.Keys.Down });
    }
}
