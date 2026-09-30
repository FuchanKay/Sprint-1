using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(ISceneManager sceneManager, IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager, IAnimationAtlas aniAtlas) : ISceneController
{
    public readonly static string Name = "GamePlay";
    private Object _exampleObject;
    private int timer = 0;
    private int SproutTimer = 0;
    private AnimatedSprite Sprout;
    public static Texture2D SproutTexture;
    private Sprout ExampleSprout;

    public void Init()
    {
        _exampleObject = new StoneBlock(new Vector2(300, 300), audioManager);
        ExampleSprout = new Sprout(new Vector2(400, 400), 8, Direction.South);
        ExampleSprout.initialize();
        Sprout = new AnimatedSprite(aniAtlas);
        Sprout.SetAnimation("SproutWalkRight");

        // placeholder song to play in the background of the gameplay scene
        audioManager.PlaySong("song");
    }

    public void Update(int dtMs)
    {
        //test
        ExampleSprout.Update(dtMs);
        // TEST
        SproutTimer++;
        Sprout.Update(dtMs);
        if(SproutTimer == 100)
        {
            Sprout.SetAnimation("SproutWalkDown");
        } 
        else if(SproutTimer == 200)
        {
            Sprout.SetAnimation("SproutWalkLeft");
        } 
        else if(SproutTimer == 300) {
            Sprout.SetAnimation("SproutWalkRight");
            SproutTimer = 0; 
        }

        // This is placeholder code that cycles an object through each object type for demonstration purposes
        Vector2 currentPosition = _exampleObject.Position;
        timer++;
        switch (timer)
        {
            case 1:
                _exampleObject = new StoneBlock(currentPosition, audioManager);
                break;
            case 50:
                _exampleObject.Destroy();
                break;
            case 100:
                _exampleObject = new Rock(currentPosition, audioManager);
                break;
            case 125:
                _exampleObject.MoveLeft();
                break;
            case 150:
                _exampleObject.MoveRight();
                break;
            case 175:
                _exampleObject.Destroy();
                break;
            case 200:
                _exampleObject = new StoneWall(currentPosition, audioManager);
                break;
            case 300:
                _exampleObject = new Bomb(currentPosition, audioManager);
                break;
            case 325:
                _exampleObject.MoveLeft();
                break;
            case 350:
                _exampleObject.MoveRight();
                break;
            case 375:
                _exampleObject.SnapBehavior();
                break;
            case 400:
                _exampleObject = new TimedBomb(currentPosition, audioManager);
                break;
            case 425:
                _exampleObject.MoveLeft();
                break;
            case 450:
                _exampleObject.MoveRight();
                break;
            case 500:
                _exampleObject = new ExitDoor(currentPosition, audioManager);
                break;
            case 600:
                _exampleObject = new Pylon(currentPosition, audioManager);
                break;
            case 700:
                _exampleObject = new Vine(currentPosition, audioManager);
                break;
            case 800:
                timer = 0;
                break;
            default:
                break;
        }

        _exampleObject.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        ExampleSprout.Draw(sb);
        _exampleObject.Draw(sb);
        var rect = Sprout.GetFrame();
        sb.Draw(SproutTexture, new Vector2(800, 500), rect, Color.White);
    }
}
