using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
namespace Scripts.Game;

public class StoneBlock(Vector2 pos, IAudioManager am) : Object(pos, am)
{
    public static Texture2D ObjectTexture {get; set;}
    protected override Texture2D Texture => ObjectTexture;
    protected override Rectangle SourceRectangle => new Rectangle(0,  0, Texture.Width, Texture.Height);
    protected override bool Pushable => false;
    protected override bool Destructible => true;
    protected override float Scale => 0.25f;
    protected override Vector2 Origin => new Vector2(Texture.Width/2, Texture.Height/2);

    public override void SnapBehavior()
    {
    }
}