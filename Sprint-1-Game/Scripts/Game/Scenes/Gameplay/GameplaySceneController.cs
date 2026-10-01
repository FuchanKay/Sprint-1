using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(ISceneManager sceneManager, IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas) : ISceneController
{
    public readonly static string Name = "GamePlay";
    private Object[] Objects =
    {
        new StoneBlock(new Vector2(300, 300), audioManager, textureAtlas),
        new Rock(new Vector2(300, 300), audioManager, textureAtlas),
        new Bomb(new Vector2(300, 300), audioManager, textureAtlas),
        new ExitDoor(new Vector2(300, 300), audioManager, textureAtlas),
        new Pylon(new Vector2(300, 300), audioManager, textureAtlas),
        new Rock(new Vector2(300, 300), audioManager, textureAtlas),
        new StoneBlock(new Vector2(300, 300), audioManager, textureAtlas),
        new StoneWall(new Vector2(300, 300), audioManager, textureAtlas),
        new TimedBomb(new Vector2(300, 300), audioManager, textureAtlas),
        new Vine(new Vector2(300, 300), audioManager, textureAtlas)
    };
    private Object CurrentObject;
    private int ObjectPointer = 0;
    // SPRITE TESTS
    private int SproutTimer = 0;
    private ISprite Sprout;
    public static Texture2D SproutTexture;

    public void Init()
    {
        CurrentObject = Objects[ObjectPointer];
        
        Sprout = new AnimatedSprite(textureAtlas);
        Sprout.SetState("SproutWalkRight");
        Sprout.Position = new Vector2(800, 500);
        Sprout.SetTexture("Sprout");

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
        CurrentObject.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        CurrentObject.Draw(sb);
        Sprout.Draw(sb);
    }
}
