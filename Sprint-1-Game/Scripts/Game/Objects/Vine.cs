using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Vine(Vector2 pos, IAudioManager am) : Object(pos, am)
{    public static Texture2D ObjectTexture {get; set;}
    protected override Texture2D Texture => ObjectTexture;
    protected override Rectangle SourceRectangle => new Rectangle(0,  0, Texture.Width, Texture.Height);
    protected override bool Pushable => false;
    protected override bool Destructible => false;
    protected override float Scale => 0.5f;
    protected override Vector2 Origin => new Vector2(Texture.Width/2, Texture.Height/2);
    public bool grown = false;

    public override void SnapBehavior()
    {
        if (!grown) 
        {
            // TODO: Grow Vines 
            grown = true;
        } else
        {
            this.IsDestroyed = true;
        }
    }
}