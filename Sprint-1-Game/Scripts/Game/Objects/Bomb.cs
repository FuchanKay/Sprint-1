using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Bomb(Vector2 pos, IAudioManager am) : Object(pos, am)
{
    public static Texture2D ObjectTexture { get; set; }
    protected override Texture2D Texture => ObjectTexture;
    protected override Rectangle SourceRectangle => new Rectangle(0, 0, Texture.Width, Texture.Height);
    protected override bool Pushable => true;
    protected override bool Destructible => false;
    protected override float Scale => 1.0f;
    protected override Vector2 Origin => new Vector2(Texture.Width / 2, Texture.Height / 2);

    public override void SnapBehavior()
    {
        if (IsDestroyed) return;
        //TODO: Play explosion animation and sound effect
        IsDestroyed = true;
    }
}