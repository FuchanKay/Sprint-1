using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Vine() : Object
{
    public static Texture2D objectTexture {get; set;}
    protected override Texture2D Texture => objectTexture;
    protected override Rectangle sourceRectangle => new Rectangle(0,  0, Texture.Width, Texture.Height);
    protected override bool pushable => false;
    protected override bool destructible => false;
    protected override float scale => 0.5f;
    protected override Vector2 origin => new Vector2(Texture.Width/2, Texture.Height/2);
    public bool grown = false;

    public override void SnapBehavior()
    {
        if (grown) 
        {
            // TODO: Grow Vines 
            grown = true;
        } else
        {
            this.isDestroyed = true;
        }
    }
}