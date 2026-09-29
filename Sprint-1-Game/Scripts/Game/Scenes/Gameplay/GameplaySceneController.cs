using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager) : ISceneController
{
    private ISceneManager SceneManager;
    private readonly IAudioManager AudioManager = audioManager;
    private readonly IInputManager ButtonInput = buttonInput;
    private readonly IInputManager MouseInput = mouseInput;
    public readonly static string Name = "GamePlay";
    private Object _exampleObject;
    private int timer = 0;

    public void Init(ISceneManager sm)
    {
        SceneManager = sm as SceneManager;

        _exampleObject = new StoneBlock(new Vector2(300, 300), audioManager);

        // placeholder song to play in the background of the gameplay scene
        AudioManager.PlaySong("song");
    }

    public void Update(int dtMs)
    {
        // This is placeholder code that cycles an object through each object type for demonstration purposes
        Vector2 currentPosition = _exampleObject.Position;
        timer++;
        switch (timer)
        {
            case 1:
                _exampleObject = new StoneBlock(currentPosition, AudioManager);
                break;
            case 50:
                _exampleObject.Destroy();
                break;
            case 100:
                _exampleObject = new Rock(currentPosition, AudioManager);
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
                _exampleObject = new StoneWall(currentPosition, AudioManager);
                break;
            case 300:
                _exampleObject = new Bomb(currentPosition, AudioManager);
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
                _exampleObject = new TimedBomb(currentPosition, AudioManager);
                break;
            case 425:
                _exampleObject.MoveLeft();
                break;
            case 450:
                _exampleObject.MoveRight();
                break;
            case 500:
                _exampleObject = new ExitDoor(currentPosition, AudioManager);
                break;
            case 600:
                _exampleObject = new Pylon(currentPosition, AudioManager);
                break;
            case 700:
                _exampleObject = new Vine(currentPosition, AudioManager);
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
        _exampleObject.Draw(sb);
    }
}
