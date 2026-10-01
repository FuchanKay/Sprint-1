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
    public void Init()
    {
        TextureAtlas aniAtlas = new();
        // AddAnimation(name, numFrames, row, width, height, delay)
        aniAtlas.AddAnimation("SproutWalkSouth", 3, 6, 64, 64);
        aniAtlas.AddAnimation("SproutWalkEast",0, 6, 64, 64);
        aniAtlas.AddAnimation("SproutWalkNorth", 2, 6, 64, 64);
        aniAtlas.AddAnimation("SproutWalkWest", 1, 6, 64, 64);
        Sprout1 = new AnimatedSprite(aniAtlas);
        Sprout1.SetState("SproutWalkSouth");
    }   
    
    public void Update(int dtMs)
    {
        
        //switch()
        switch (Direction)
        {
            case Direction.North:
                Sprout1.SetState("SproutWalkNorth");
                break;
            case Direction.East:
                Sprout1.SetState("SproutWalkEast");
                break;
            case Direction.South:
                Sprout1.SetState("SproutWalkSouth");
                break;
            case Direction.West:
                Sprout1.SetState("SproutWalkWest");
                break;
        }
        Sprout1.Update(dtMs);
    }
    public void Draw(SpriteBatch sb)
    {
        Sprout1.Draw(sb);
    }
}