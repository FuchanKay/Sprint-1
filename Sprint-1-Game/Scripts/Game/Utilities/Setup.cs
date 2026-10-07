using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
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
        ButtonInput.MapInput(InputNames.Destroy, (int)Keys.Space);
        ButtonInput.MapInput(InputNames.Snap, (int)Keys.F);
        ButtonInput.MapInput(InputNames.CycleObjectLeft, (int)Keys.U);
        ButtonInput.MapInput(InputNames.CycleObjectRight, (int)Keys.I);
        ButtonInput.MapInput(InputNames.CycleTileLeft, (int)Keys.T);
        ButtonInput.MapInput(InputNames.CycleTileRight, (int)Keys.Y);

        ButtonInput.MapInput(InputNames.CycleEnemyLeft, (int)Keys.O);
        ButtonInput.MapInput(InputNames.CycleEnemyRight, (int)Keys.P);

        ButtonInput.MapInput(InputNames.ExitGame, (int)Keys.Q);
        ButtonInput.MapInput(InputNames.ResetGame, (int)Keys.R);

        MouseInput.MapInput(InputNames.Select, (int)MouseButtons.Left);
    }

    private void AddAnimations()
    {
        TextureAtlas.AddAnimation(AnimationNames.PlayerWalkNorth, 2, 6, 64, 64);
        TextureAtlas.AddAnimation(AnimationNames.PlayerWalkSouth, 3, 6, 64, 64);
        TextureAtlas.AddAnimation(AnimationNames.PlayerWalkEast, 0, 6, 64, 64);
        TextureAtlas.AddAnimation(AnimationNames.PlayerWalkWest, 1, 6, 64, 64, 100);
        TextureAtlas.AddAnimation(AnimationNames.PlayerSnap, 4, 6, 64, 64, 200);
        TextureAtlas.AddRegion(RegionNames.PlayerIdleNorth, 1, 5, 64, 64);
        TextureAtlas.AddRegion(RegionNames.PlayerIdleSouth, 0, 5, 64, 64);
        TextureAtlas.AddRegion(RegionNames.PlayerIdleEast, 2, 5, 64, 64);
        TextureAtlas.AddRegion(RegionNames.PlayerIdleWest, 3, 5, 64, 64);

        TextureAtlas.AddRegion(RegionNames.Rock, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.BrickTile, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.GrassTile, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.WaterTile, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.LavaTile, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.Wall, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.Bomb, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.TimedBomb, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.Explosion, 2, 0, 105, 96);
        TextureAtlas.AddAnimation(AnimationNames.Explosion, 0, 4, 105, 96);

        TextureAtlas.AddRegion(RegionNames.Skeleton, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.Warlock, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.BlueLizard, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.RedLizard, 0, 0, 256, 256);
    

    }

    private void BindAllTextures()
    {
        TextureAtlas.AddTexture(TextureNames.PlayGameButton, FileNames.PlayGameButtonTexture);
        TextureAtlas.AddTexture(TextureNames.ExitGameButton, FileNames.ExitGameButtonTexture);

        TextureAtlas.AddTexture(TextureNames.Player, FileNames.PlayerTexture);
        TextureAtlas.AddTexture(TextureNames.Rock, FileNames.RockTexture);
        TextureAtlas.AddTexture(TextureNames.BrickTile, FileNames.BrickTileTexture);
        TextureAtlas.AddTexture(TextureNames.GrassTile, FileNames.GrassTileTexture);
        TextureAtlas.AddTexture(TextureNames.WaterTile, FileNames.WaterTileTexture);
        TextureAtlas.AddTexture(TextureNames.LavaTile, FileNames.LavaTileTexture);
        TextureAtlas.AddTexture(TextureNames.Wall, FileNames.WallTexture);
        TextureAtlas.AddTexture(TextureNames.Bomb, FileNames.BombTexture);
        TextureAtlas.AddTexture(TextureNames.Explosion, FileNames.ExplosionTexture);

        TextureAtlas.AddTexture(TextureNames.Skeleton, FileNames.SkeletonTexture);
        TextureAtlas.AddTexture(TextureNames.Warlock, FileNames.WarlockTexture);
        TextureAtlas.AddTexture(TextureNames.RedLizard, FileNames.RedLizardTexture);
        TextureAtlas.AddTexture(TextureNames.BlueLizard, FileNames.BlueLizardTexture);


        

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
        AudioManager.MapSound(SoundNames.Snap, content.Load<SoundEffect>(FileNames.SnapAudio), 1.0f);
        AudioManager.MapSong(SoundNames.Song, content.Load<Song>(FileNames.SongAudio), 0.2f);
    }
}