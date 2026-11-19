using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary;
using Scripts.Game;
using Gum.Forms;
using Gum.Forms.Controls;
using Microsoft.Xna.Framework.Media;
using Gum;
using Gum.Managers;
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
        //InitializeGum();
        SceneManager = sceneManager;

        GumUI.Initialize(this, "GUMProject/GUMProject.gumj");
        var screen = ObjectFinder.Self.GumProjectSave.Screens[1].ToGraphicalUiElement();
        screen.AddToRoot();

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
        GumUI.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        SpriteBatch.Begin(SpriteSortMode.FrontToBack);

        // TEST (random destination)
        SceneManager.Draw(SpriteBatch);
        SpriteBatch.End();
        GumUI.Draw();
        base.Draw(gameTime);
    }

}
