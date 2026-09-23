using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Scripts.GameComponents;

public class StoneBlock : Object
{
    public static Texture2D objectTexture {get; set;}
    protected override Texture2D Texture => objectTexture;

    public StoneBlock(Vector2 position)
    {
        this.position = position;
        this.pushable = false;
        this.destructible = true;
    }

    public override void SnapBehavior()
    {
    }
}