using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class Sprout : Character
{
    public static Texture2D SproutTexture1;
    private AnimatedSprite Sprout1;
    public Sprout(Vector2 pos, float speed, Direction direction)
        : base(pos, speed, direction)
    {
    }
    public void initialize()
    {
        /*
        AnimationAtlas aniAtlas = new();
        // AddAnimation(name, numFrames, row, width, height, delay)
        aniAtlas.AddAnimation("SproutWalkSouth", 3, 6, 64, 64);
        aniAtlas.AddAnimation("SproutWalkEast",0, 6, 64, 64);
        aniAtlas.AddAnimation("SproutWalkNorth", 2, 6, 64, 64);
        aniAtlas.AddAnimation("SproutWalkWest", 1, 6, 64, 64);
        Sprout1 = new AnimatedSprite(aniAtlas);
        Sprout1.SetAnimation("SproutWalkSouth");
        */
    }   
    
    public void Update(int dtMs)
    {
        /*
        //switch()
        switch (Direction)
        {
            case Direction.North:
                Sprout1.SetAnimation("SproutWalkNorth");
                break;
            case Direction.East:
                Sprout1.SetAnimation("SproutWalkEast");
                break;
            case Direction.South:
                Sprout1.SetAnimation("SproutWalkSouth");
                break;
            case Direction.West:
                Sprout1.SetAnimation("SproutWalkWest");
                break;
        }
        Sprout1.Update(dtMs);
        */
    }
    public void Draw(SpriteBatch sb)
    {
     //   sb.Draw(SproutTexture1, Pos, rect, Color.White);
    }
}