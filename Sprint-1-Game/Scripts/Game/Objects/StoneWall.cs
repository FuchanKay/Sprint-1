using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
namespace Scripts.Game;

public class StoneWall() : Object
{
    public static Texture2D objectTexture {get; set;}
    protected override Texture2D Texture => objectTexture;
    protected override bool pushable => false;
    protected override bool destructible => false;
    protected override float scale => 0.25f;

    public override void SnapBehavior()
    {
    }
}