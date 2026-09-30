using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(ISceneManager sceneManager, IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas texAtlas) : ISceneController
{
    public readonly static string Name = "GamePlay";
    private Object[] Objects =
    {
        new StoneBlock(new Vector2(300, 300), audioManager, texAtlas),
        new Rock(new Vector2(300, 300), audioManager, texAtlas),
        new Bomb(new Vector2(300, 300), audioManager, texAtlas),
        new ExitDoor(new Vector2(300, 300), audioManager, texAtlas),
        new Pylon(new Vector2(300, 300), audioManager, texAtlas),
        new Rock(new Vector2(300, 300), audioManager, texAtlas),
        new StoneBlock(new Vector2(300, 300), audioManager, texAtlas),
        new StoneWall(new Vector2(300, 300), audioManager, texAtlas),
        new TimedBomb(new Vector2(300, 300), audioManager, texAtlas),
        new Vine(new Vector2(300, 300), audioManager, texAtlas)
    };
    private Object CurrentObject;
    private int ObjectPointer = 2;
    // SPRITE TESTS
    private int SproutTimer = 0;
    private ISprite Sprout;
    public static Texture2D SproutTexture;

    public void Init()
    {
        CurrentObject = Objects[ObjectPointer];

        Sprout = new AnimatedSprite(texAtlas);
        Sprout.SetState("SproutWalkRight");
        Sprout.Position = new Vector2(800, 500);
        Sprout.Texture = SproutTexture;

        audioManager.PlaySong("song");
    }

    public void Update(int dtMs)
    {

        // TEST
        Sprout.Update(dtMs);
        SproutTimer++;
        switch (SproutTimer)
        {
            case 1:
                Sprout = Sprout.ConvertToAnimated("SproutWalkRight");
                break;
            case 100:
                Sprout.SetState("SproutWalkDown");
                break;
            case 200:
                Sprout.SetState("SproutWalkLeft");
                break;
            case 300:
                Sprout = Sprout.ConvertToStatic("SproutIdle");
                break;
            case 400:
                SproutTimer = 0;
                break;
            default:
                break;
        }

        if (buttonInput.IsPressed("Cycle Block Left"))
        {
            ObjectPointer--;
            if (ObjectPointer < 0) ObjectPointer = Objects.Length - 1;
        }
        if (buttonInput.IsPressed("Cycle Block Right"))
        {
            ObjectPointer++;
            if (ObjectPointer >= Objects.Length) ObjectPointer = 0;
        }
        CurrentObject = Objects[ObjectPointer];
        if(buttonInput.IsPressed("Destroy")) CurrentObject.Destroy();
        if(buttonInput.IsPressed("Snap")) CurrentObject.SnapBehavior();
        CurrentObject.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        CurrentObject.Draw(sb);
        Sprout.Draw(sb);
    }
}
