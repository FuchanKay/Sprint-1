using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
namespace Scripts.Game;

public class StoneBlock() : Object
{
    public static Texture2D objectTexture {get; set;}
    protected override Texture2D Texture => objectTexture;
    protected override Rectangle sourceRectangle => new Rectangle(0,  0, Texture.Width, Texture.Height);
    protected override bool pushable => false;
    protected override bool destructible => true;
    protected override float scale => 0.25f;
    protected override Vector2 origin => new Vector2(Texture.Width/2, Texture.Height/2);

    public override void SnapBehavior()
    {
    }
}