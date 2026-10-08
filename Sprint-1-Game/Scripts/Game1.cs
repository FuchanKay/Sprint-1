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
    private readonly Setup Setup;
    private SceneManager SceneManager;

    public Game1() : base(Name, ScreenWidth, ScreenHeight, IsFullScreen)
    {
        Setup = new(Content);
    }

    protected override void Initialize()
    {
        Setup.Initialize(out var sceneManager);
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
}
