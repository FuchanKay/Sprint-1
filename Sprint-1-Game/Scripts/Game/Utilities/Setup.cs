using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Scripts.GameComponents;

namespace Scripts.Game;

public class Setup(ContentManager content)
{
    //TODO: If there are any save file stuff that needs to be resolved it should be done here. 
    private IAudioManager AudioManager;
    private ITextureAtlas TextureAtlas;
    private IInputManager ButtonInput;
    private IInputManager MouseInput;

    public void Initialize(out SceneManager sceneManager)
    {
        ButtonInput = new KeyboardInputManager();
        MouseInput = new MouseInputManager();
        TextureAtlas = new TextureAtlas(content);
        AudioManager = new AudioManager();
        sceneManager = new(ButtonInput, MouseInput, AudioManager, TextureAtlas);
        sceneManager.Init();
    }

    public void LoadContent()
    {
        BindAllTextures();
        LoadAudio();
        AddAnimations();
        MapDefaultInputs();
    }

    private void BindAllTextures()
    {
        
        PlayGameButton.ButtonTexture = content.Load<Texture2D>("Images/play-button");
        ExitGameButton.ButtonTexture = content.Load<Texture2D>("Images/exit-button");

        // Placeholder Sprites
        // StoneBlock.ObjectTexture = content.Load<Texture2D>("ObjectSprites/stoneBlock");
        // Rock.ObjectTexture = content.Load<Texture2D>("ObjectSprites/rock");
        // StoneWall.ObjectTexture = content.Load<Texture2D>("ObjectSprites/stoneWall");
        Bomb.ObjectTexture = content.Load<Texture2D>("ObjectSprites/bomb");
        TextureAtlas.AddTexture("Bomb", "ObjectSprites/bomb");
        // ExitDoor.ObjectTexture = content.Load<Texture2D>("ObjectSprites/exitDoor");
        // Pylon.ObjectTexture = content.Load<Texture2D>("ObjectSprites/pylon");
        // Vine.ObjectTexture = content.Load<Texture2D>("ObjectSprites/vines");
        // TimedBomb.ObjectTexture = content.Load<Texture2D>("ObjectSprites/timedBomb");

        //TODO: Remove this when sprite becomes its own thing
        GameplaySceneController.SproutTexture = content.Load<Texture2D>("Images/player-sprites");
        TextureAtlas.AddTexture("Sprout", "Images/player-sprites");
    }
    private void LoadAudio()
    {
        AudioManager.MapSound("snap", content.Load<SoundEffect>("audio/snap"), 1.0f);
        AudioManager.MapSong("song", content.Load<Song>("audio/song"), 0.2f);
    }

    private void AddAnimations()
    {
        TextureAtlas.AddAnimation("SproutWalkDown", 3, 6, 64, 64);
        TextureAtlas.AddAnimation("SproutWalkRight", 0, 6, 64, 64);
        // AddAnimation(name, numFrames, row, width, height, delay)
        TextureAtlas.AddAnimation("SproutWalkLeft", 1, 6, 64, 64, 100);
        // AddRegion(name, column, row, width, height)
        TextureAtlas.AddRegion("SproutIdle", 0, 5, 64, 64);

        TextureAtlas.AddRegion("Bomb", 0, 0, TextureAtlas.GetTexture("Bomb").Width, TextureAtlas.GetTexture("Bomb").Height);
    }

    private void MapDefaultInputs()
    {
        //TODO: mapping input example. Should be removed
        ButtonInput.MapInput("Move North", (int)Keys.W);
        ButtonInput.MapInput("Move East", (int)Keys.D);
        ButtonInput.MapInput("Move South", (int)Keys.S);
        ButtonInput.MapInput("Move West", (int)Keys.A);
        ButtonInput.MapInput("Destroy", (int)Keys.Space);
        ButtonInput.MapInput("Snap", (int)Keys.E);
        ButtonInput.MapInput("Cycle Block Left", (int)Keys.T);
        ButtonInput.MapInput("Cycle Block Right", (int)Keys.Y);

        ButtonInput.MapInput("Exit Game", (int)Keys.Q);
        ButtonInput.MapInput("Restart Game", (int)Keys.R);
        
        MouseInput.MapInput("Select", (int)MouseButtons.Left);
    }
}