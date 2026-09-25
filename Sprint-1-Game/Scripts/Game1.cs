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
    private static readonly Color BackgroundColor = Color.White;
    private readonly SceneManager SceneManager;


    // TEST
    public static Texture2D SproutTexture { get; set; }
    private AnimationManager AnimationManager;
    private Rectangle SproutSource;
    private int i;

    public Game1() : base(Name, ScreenWidth, ScreenHeight, IsFullScreen)
    {
        KeyboardInputManager keyInput = new();
        MouseInputManager mouseInput = new();
        SceneManager = new(keyInput, mouseInput);
    }

    protected override void Initialize()
    {
        SceneManager.Init();

        // TEST
        int delay = 100;
        AnimationManager = new(delay);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TEST: i'm not initializing each of my test magic numbers as a variable bc that's ANNOYING!!!
        // AddAnimation(name, numFrames, row)
        AnimationManager.AddAnimation("WalkDown", 6, 3);
        AnimationManager.AddAnimation("WalkLeft", 6, 1);
        AnimationManager.AddAnimation("WalkRight", 6, 0);
        // LoadAnimations(spritesWidth, SpritesHeight)
        // requires all sprites to be the same width and height!
        AnimationManager.LoadAnimations(64, 64);

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

        // TEST: note that gameTime will update much faster than animation frame
        i++;
        if(i < 100)
        {
            SproutSource = AnimationManager.UpdateSourceRectangle("WalkRight", gameTime.ElapsedGameTime.Milliseconds);
        } else if(i < 200)
        {
            SproutSource = AnimationManager.UpdateSourceRectangle("WalkDown", gameTime.ElapsedGameTime.Milliseconds);
        } else if(i < 300)
        {
            SproutSource = AnimationManager.UpdateSourceRectangle("WalkLeft", gameTime.ElapsedGameTime.Milliseconds);
        } else { i = 0; }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        SpriteBatch.Begin();

        // TEST (random destination)
        SpriteBatch.Draw(SproutTexture, new Vector2(800, 500), SproutSource, Color.White);

        SceneManager.Draw(SpriteBatch);
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    private static void BindAllTextures()
    {
        PlayGameButton.ButtonTexture = Content.Load<Texture2D>("Images/play-button");
        ExitGameButton.ButtonTexture = Content.Load<Texture2D>("Images/exit-button");
        //TODO: Remove this once game play actually has stuff in it
        GameplaySceneController.Mario = Content.Load<Texture2D>("Images/mario");
        
        // TEST
        SproutTexture = Content.Load<Texture2D>("Images/player-sprites");
    }
}
