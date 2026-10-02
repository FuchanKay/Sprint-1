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
    private IInputManager ButtonInput;
    private IInputManager MouseInput;
    private ITextureAtlas TextureAtlas;
    public void Initialize(out SceneManager sceneManager)
    {
        ButtonInput = new KeyboardInputManager();
        MouseInput = new MouseInputManager();
        AudioManager = new AudioManager();

        TextureAtlas = new TextureAtlas(content);
        MapDefaultInputs();
        AddAnimations();

        sceneManager = new(ButtonInput, MouseInput, AudioManager, TextureAtlas);
        sceneManager.Init();
    }

    public void LoadContent()
    {
        BindAllTextures();
        LoadAudio();
    }


    private void MapDefaultInputs()
    {
        ButtonInput.MapInput(InputNames.MoveNorth, (int)Keys.W);
        ButtonInput.MapInput(InputNames.MoveEast, (int)Keys.D);
        ButtonInput.MapInput(InputNames.MoveSouth, (int)Keys.S);
        ButtonInput.MapInput(InputNames.MoveWest, (int)Keys.A);
        ButtonInput.MapInput("Destroy", (int)Keys.Space);
        ButtonInput.MapInput(InputNames.Snap, (int)Keys.F);
        ButtonInput.MapInput("Cycle Block Left", (int)Keys.T);
        ButtonInput.MapInput("Cycle Block Right", (int)Keys.Y);

        ButtonInput.MapInput(InputNames.ExitGame, (int)Keys.Q);
        ButtonInput.MapInput(InputNames.ResetGame, (int)Keys.R);

        MouseInput.MapInput(InputNames.Select, (int)MouseButtons.Left);
    }

    private void AddAnimations()
    {
        TextureAtlas.AddAnimation("SproutWalkDown", 3, 6, 64, 64);
        TextureAtlas.AddAnimation("SproutWalkRight", 0, 6, 64, 64);
        // AddAnimation(name, numFrames, row, width, height, delay)
        TextureAtlas.AddAnimation("SproutWalkLeft", 1, 6, 64, 64, 100);
        // AddRegion(name, column, row, width, height)
        TextureAtlas.AddRegion("SproutIdle", 0, 5, 64, 64);

        TextureAtlas.AddRegion("Rock", 0, 0, 256, 256);
    }

    private void BindAllTextures()
    {
        TextureAtlas.AddTexture(TextureNames.PlayGameButton, "Images/play-button");
        TextureAtlas.AddTexture(TextureNames.ExitGameButton, "Images/exit-button");

        // StoneBlock.ObjectTexture = content.Load<Texture2D>("ObjectSprites/stoneBlock");
        // Rock.ObjectTexture = content.Load<Texture2D>("ObjectSprites/rock");
        // StoneWall.ObjectTexture = content.Load<Texture2D>("ObjectSprites/stoneWall");
        // Bomb.ObjectTexture = content.Load<Texture2D>("ObjectSprites/bomb");
        // ExitDoor.ObjectTexture = content.Load<Texture2D>("ObjectSprites/exitDoor");
        // Pylon.ObjectTexture = content.Load<Texture2D>("ObjectSprites/pylon");
        // Vine.ObjectTexture = content.Load<Texture2D>("ObjectSprites/vines");
        // TimedBomb.ObjectTexture = content.Load<Texture2D>("ObjectSprites/timedBomb");

        // //TODO: Remove this when sprite becomes its own thing
        // GameplaySceneController.SproutTexture = content.Load<Texture2D>("Images/player-sprites");
    }
    private void LoadAudio()
    {
        AudioManager.MapSound(SoundNames.Snap, content.Load<SoundEffect>("audio/snap"), 1.0f);
        AudioManager.MapSong(SoundNames.Song, content.Load<Song>("audio/song"), 0.2f);
    }
}