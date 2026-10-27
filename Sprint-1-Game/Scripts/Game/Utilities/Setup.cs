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
        AddRegions();

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
        ButtonInput.MapInput(InputNames.MoveNorthAlt, (int)Keys.Up);
        ButtonInput.MapInput(InputNames.MoveEastAlt, (int)Keys.Right);
        ButtonInput.MapInput(InputNames.MoveSouthAlt, (int)Keys.Down);
        ButtonInput.MapInput(InputNames.MoveWestAlt, (int)Keys.Left);

        ButtonInput.MapInput(InputNames.ChangeLevel, (int) Keys.B);

        ButtonInput.MapInput(InputNames.LCtrl, (int)Keys.LeftControl);
        ButtonInput.MapInput(InputNames.LShift, (int)Keys.LeftShift);

        ButtonInput.MapInput(InputNames.Snap, (int)Keys.F);

        ButtonInput.MapInput(InputNames.ExitGame, (int)Keys.Q);
        ButtonInput.MapInput(InputNames.ResetGame, (int)Keys.R);
        ButtonInput.MapInput(InputNames.ResetLevel, (int)Keys.R);

        ButtonInput.MapInput(InputNames.Undo, (int)Keys.Z);

        MouseInput.MapInput(InputNames.Select, (int)MouseButtons.Left);
    }

    private void AddAnimations()
    {
        // walk
        int walkDelay = 50;
        TextureAtlas.AddAnimation(AnimationNames.PlayerWalkNorth, 6, 0, 6, 64, 64, walkDelay);
        TextureAtlas.AddAnimation(AnimationNames.PlayerWalkSouth, 7, 0, 6, 64, 64, walkDelay);
        TextureAtlas.AddAnimation(AnimationNames.PlayerWalkEast, 4, 0, 6, 64, 64, walkDelay);
        TextureAtlas.AddAnimation(AnimationNames.PlayerWalkWest, 5, 0, 6, 64, 64, walkDelay);

        // snap
        TextureAtlas.AddAnimation(AnimationNames.PlayerSnap, 8, 0, 6, 64, 64);

        // rotate counterclockwise
        int rotateFrames = 2;
        int rotateDelay = 200;
        TextureAtlas.AddAnimation(AnimationNames.RotateSE, 0, 0, rotateFrames, 64, 64, rotateDelay);
        TextureAtlas.AddAnimation(AnimationNames.RotateEN, 0, 2, rotateFrames, 64, 64, rotateDelay);
        TextureAtlas.AddAnimation(AnimationNames.RotateNW, 0, 4, rotateFrames, 64, 64, rotateDelay);
        TextureAtlas.AddAnimation(AnimationNames.RotateWS, 0, 6, rotateFrames, 64, 64, rotateDelay);
        // rotate clockwise
        TextureAtlas.AddAnimation(AnimationNames.RotateSW, 1, 0, rotateFrames, 64, 64, rotateDelay);
        TextureAtlas.AddAnimation(AnimationNames.RotateWN, 1, 2, rotateFrames, 64, 64, rotateDelay);
        TextureAtlas.AddAnimation(AnimationNames.RotateNE, 1, 4, rotateFrames, 64, 64, rotateDelay);
        TextureAtlas.AddAnimation(AnimationNames.RotateES, 1, 6, rotateFrames, 64, 64, rotateDelay);

        // push
        int pushFrames = 4;
        TextureAtlas.AddAnimation(AnimationNames.PlayerPushNorth, 3, 4, pushFrames, 64, 64);
        TextureAtlas.AddAnimation(AnimationNames.PlayerPushSouth, 3, 0, pushFrames, 64, 64);
        TextureAtlas.AddAnimation(AnimationNames.PlayerPushEast, 2, 0, pushFrames, 64, 64);
        TextureAtlas.AddAnimation(AnimationNames.PlayerPushWest, 2, 4, pushFrames, 64, 64);

        // tiles
        TextureAtlas.AddAnimation(AnimationNames.BugTileRotate, 2, 0, rotateFrames, 64, 64, rotateDelay);
        TextureAtlas.AddAnimation(AnimationNames.BugTileRotateCC, 2, 2, rotateFrames, 64, 64, rotateDelay);

        // obj explode
        TextureAtlas.AddAnimation(AnimationNames.Explosion, 0, 0, 4, 105, 96);

        // slash attack effect
        TextureAtlas.AddAnimation(AnimationNames.Slash, 0, 0, 6, 102, 440, 50);
    }

    private void AddRegions()
    {
        // player
        TextureAtlas.AddRegion(RegionNames.IdleNorth, 3, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.IdleSouth, 7, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.IdleEast, 1, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.IdleWest, 5, 0, 64, 64);

        // tiles
        TextureAtlas.AddRegion(RegionNames.DirtTile, 0, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.GrassTile, 1, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.BrickTile, 2, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.MossyBrickTile, 3, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.PitTile, 0, 1, 64, 64);
        TextureAtlas.AddRegion(RegionNames.LavaTile, 1, 1, 64, 64);
        TextureAtlas.AddRegion(RegionNames.WaterTile, 2, 1, 64, 64);
        TextureAtlas.AddRegion(RegionNames.BugTile, 1, 2, 64, 64);
        TextureAtlas.AddRegion(RegionNames.BugTileCC, 3, 2, 64, 64);

        // objects
        TextureAtlas.AddRegion(RegionNames.Rock, 0, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.Pumpkin, 1, 0, 64, 64);
        TextureAtlas.AddRegion(RegionNames.Wall, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.Bomb, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.TimedBomb, 0, 0, 256, 256);

        // effects
        TextureAtlas.AddRegion(RegionNames.LaserBeam, 4, 2, 64, 64);
        TextureAtlas.AddRegion(RegionNames.Explosion, 2, 0, 105, 96);
        TextureAtlas.AddRegion(RegionNames.Slash, 0, 0, 102, 440);

        // lizard mouth open states
        TextureAtlas.AddRegion(RegionNames.LizardMouthOpenWest, 0, 2, 64, 64);
        TextureAtlas.AddRegion(RegionNames.LizardMouthOpenNorth, 1, 2, 64, 64);
        TextureAtlas.AddRegion(RegionNames.LizardMouthOpenEast, 2, 2, 64, 64);
        TextureAtlas.AddRegion(RegionNames.LizardMouthOpenSouth, 3, 2, 64, 64);

        TextureAtlas.AddRegion(RegionNames.Skeleton, 0, 0, 256, 256);
        TextureAtlas.AddRegion(RegionNames.Warlock, 0, 0, 256, 256);
    }

    private void BindAllTextures()
    {
        TextureAtlas.AddTexture(TextureNames.PlayGameButton, FileNames.PlayGameButtonTexture);
        TextureAtlas.AddTexture(TextureNames.ExitGameButton, FileNames.ExitGameButtonTexture);

        TextureAtlas.AddTexture(TextureNames.Player, FileNames.PlayerTexture);
        TextureAtlas.AddTexture(TextureNames.TileSprites, FileNames.TileTextures);
        TextureAtlas.AddTexture(TextureNames.ObjectSprites, FileNames.ObjectTextures);
        TextureAtlas.AddTexture(TextureNames.Wall, FileNames.WallTexture);
        TextureAtlas.AddTexture(TextureNames.Bomb, FileNames.BombTexture);
        TextureAtlas.AddTexture(TextureNames.Explosion, FileNames.ExplosionTexture);
        TextureAtlas.AddTexture(TextureNames.Slash, FileNames.SlashTexture);

        TextureAtlas.AddTexture(TextureNames.Skeleton, FileNames.SkeletonTexture);
        TextureAtlas.AddTexture(TextureNames.Warlock, FileNames.WarlockTexture);
        TextureAtlas.AddTexture(TextureNames.RedLizard, FileNames.RedLizardTexture);
        TextureAtlas.AddTexture(TextureNames.BlueLizard, FileNames.BlueLizardTexture);
    }
    private void LoadAudio()
    {
        AudioManager.MapSound(SoundNames.Snap, content.Load<SoundEffect>(FileNames.SnapAudio), 1.0f);
        AudioManager.MapSong(SoundNames.Song, content.Load<Song>(FileNames.SongAudio), 0.2f);
    }
}