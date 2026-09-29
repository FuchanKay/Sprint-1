using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
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
    private readonly AudioManager AudioManager;


    // TEST
    public static Texture2D SproutTexture { get; set; }
    private AnimationAtlas AniAtlas;
    private AnimatedSprite Sprout;
    private Rectangle SproutSourceRectangle;
    private int i;

    public Game1() : base(Name, ScreenWidth, ScreenHeight, IsFullScreen)
    {
        KeyboardInputManager keyInput = new();
        MouseInputManager mouseInput = new();
        AniAtlas = new();
        AudioManager = new();
        SceneManager = new(keyInput, mouseInput, AudioManager);
    }

    protected override void Initialize()
    {
        BindAllTextures();
        LoadAudio();
        SceneManager.Init();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // TEST: i'm not initializing each of my test magic numbers as a variable bc that's ANNOYING!!!
        // AddAnimation(name, numFrames, row, width, height)
        AniAtlas.AddAnimation("SproutWalkDown", 3, 6, 64, 64);
        AniAtlas.AddAnimation("SproutWalkRight",0, 6, 64, 64);
        // AddAnimation(name, numFrames, row, width, height, delay)
        AniAtlas.AddAnimation("SproutWalkLeft", 1, 6, 64, 64, 100);

        Sprout = new AnimatedSprite(AniAtlas);
        Sprout.SetAnimation("SproutWalkRight");

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

        // TEST
        Sprout.Update(gameTime.ElapsedGameTime.Milliseconds);
        if(i == 100)
        {
            Sprout.SetAnimation("SproutWalkDown");
        } 
        else if(i == 200)
        {
            Sprout.SetAnimation("SproutWalkLeft");
        } 
        else if(i == 300) {
            Sprout.SetAnimation("SproutWalkRight");
            i = 0; 
        }
        i++;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(BackgroundColor);

        SpriteBatch.Begin();

        // TEST (random destination)
        SproutSourceRectangle = Sprout.GetFrame();
        SpriteBatch.Draw(SproutTexture, new Vector2(800, 500), SproutSourceRectangle, Color.White);

        SceneManager.Draw(SpriteBatch);
        SpriteBatch.End();

        base.Draw(gameTime);
    }

    private static void BindAllTextures()
    {
        PlayGameButton.ButtonTexture = Content.Load<Texture2D>("Images/play-button");
        ExitGameButton.ButtonTexture = Content.Load<Texture2D>("Images/exit-button");
        
        // TEST
        SproutTexture = Content.Load<Texture2D>("Images/player-sprites");
        
        // Placeholder Sprites
        StoneBlock.ObjectTexture = Content.Load<Texture2D>("ObjectSprites/stoneBlock");
        Rock.ObjectTexture = Content.Load<Texture2D>("ObjectSprites/rock");
        StoneWall.ObjectTexture = Content.Load<Texture2D>("ObjectSprites/stoneWall");
        Bomb.ObjectTexture = Content.Load<Texture2D>("ObjectSprites/bomb");
        ExitDoor.ObjectTexture = Content.Load<Texture2D>("ObjectSprites/exitDoor");
        Pylon.ObjectTexture = Content.Load<Texture2D>("ObjectSprites/pylon");
        Vine.ObjectTexture = Content.Load<Texture2D>("ObjectSprites/vines");
        TimedBomb.ObjectTexture = Content.Load<Texture2D>("ObjectSprites/timedBomb");
    }

    private void LoadAudio()
    {
        AudioManager.MapSound("snap", Content.Load<SoundEffect>("audio/snap"), 1.0f);
        AudioManager.MapSong("song", Content.Load<Song>("audio/song"), 0.2f);
    }
}
