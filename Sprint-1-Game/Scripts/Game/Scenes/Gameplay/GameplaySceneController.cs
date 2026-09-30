using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class GameplaySceneController(ISceneManager sceneManager, IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager, IAnimationAtlas aniAtlas) : ISceneController
{
    public readonly static string Name = "GamePlay";
    private Object[] Objects =
    {
        new StoneBlock(new Vector2(300, 300), audioManager),
        new Rock(new Vector2(300, 300), audioManager),
        new Bomb(new Vector2(300, 300), audioManager),
        new ExitDoor(new Vector2(300, 300), audioManager),
        new Pylon(new Vector2(300, 300), audioManager),
        new Rock(new Vector2(300, 300), audioManager),
        new StoneBlock(new Vector2(300, 300), audioManager),
        new StoneWall(new Vector2(300, 300), audioManager),
        new TimedBomb(new Vector2(300, 300), audioManager),
        new Vine(new Vector2(300, 300), audioManager)
    };
    private Object CurrentObject;
    private int ObjectPointer = 0;
    private int SproutTimer = 0;
    private AnimatedSprite Sprout;
    public static Texture2D SproutTexture;

    public void Init()
    {
        CurrentObject = Objects[ObjectPointer];

        Sprout = new AnimatedSprite(aniAtlas);
        Sprout.SetAnimation("SproutWalkRight");

        // placeholder song to play in the background of the gameplay scene
        audioManager.PlaySong("song");
    }

    public void Update(int dtMs)
    {

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
        var rect = Sprout.GetFrame();
        sb.Draw(SproutTexture, new Vector2(800, 500), rect, Color.White);
    }
}
