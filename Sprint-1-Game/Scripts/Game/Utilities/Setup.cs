using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Scripts.GameComponents;

namespace Scripts.Game;

public class Setup(ContentManager content)
{
    //TODO: If there are any save file stuff that needs to be resolved it should be done here. 
    private IAudioManager AudioManager;
    private ITextureAtlas TextureAtlas;

    public void Initialize(out SceneManager sceneManager)
    {
        KeyboardInputManager keyboardInput = new();
        MouseInputManager mouseInput = new();
        TextureAtlas = new TextureAtlas(content);
        AudioManager = new AudioManager();
        sceneManager = new(keyboardInput, mouseInput, AudioManager, TextureAtlas);
        sceneManager.Init();
    }

    public void LoadContent()
    {
        BindAllTextures();
        LoadAudio();
    }

    private void BindAllTextures()
    {
        
        PlayGameButton.ButtonTexture = content.Load<Texture2D>("Images/play-button");
        ExitGameButton.ButtonTexture = content.Load<Texture2D>("Images/exit-button");

        // Placeholder Sprites
        StoneBlock.ObjectTexture = content.Load<Texture2D>("ObjectSprites/stoneBlock");
        Rock.ObjectTexture = content.Load<Texture2D>("ObjectSprites/rock");
        StoneWall.ObjectTexture = content.Load<Texture2D>("ObjectSprites/stoneWall");
        Bomb.ObjectTexture = content.Load<Texture2D>("ObjectSprites/bomb");
        ExitDoor.ObjectTexture = content.Load<Texture2D>("ObjectSprites/exitDoor");
        Pylon.ObjectTexture = content.Load<Texture2D>("ObjectSprites/pylon");
        Vine.ObjectTexture = content.Load<Texture2D>("ObjectSprites/vines");
        TimedBomb.ObjectTexture = content.Load<Texture2D>("ObjectSprites/timedBomb");

        //TODO: Remove this when sprite becomes its own thing
        Sprout.SproutTexture1 = content.Load<Texture2D>("Images/player-sprites");
        GameplaySceneController.SproutTexture = content.Load<Texture2D>("Images/player-sprites");
    }
    private void LoadAudio()
    {
        AudioManager.MapSound("snap", content.Load<SoundEffect>("audio/snap"), 1.0f);
        AudioManager.MapSong("song", content.Load<Song>("audio/song"), 0.2f);
    }
}