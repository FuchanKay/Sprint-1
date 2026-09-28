using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
using System.Security.Principal;
namespace Scripts.Game;

public class TimedBomb(Vector2 pos, IAudioManager am) : Object(pos, am)
{
    public static Texture2D ObjectTexture {get; set;}
    protected override Texture2D Texture => ObjectTexture;
    protected override Rectangle SourceRectangle => new Rectangle(0,  0, Texture.Width, Texture.Height);
    protected override bool Pushable => true;
    protected override bool Destructible => false;
    protected override float Scale => 0.2f;
    protected override Vector2 Origin => new Vector2(Texture.Width/2, Texture.Height/2);
    private int remainingTime = 2;

    public override void SnapBehavior()
    {
        if(IsDestroyed) return;
        if(remainingTime == 1)
        {
            //TODO: Play explosion animation and sound effect
            this.IsDestroyed = true;
        } else
        {
            remainingTime--;
        }
    }
}