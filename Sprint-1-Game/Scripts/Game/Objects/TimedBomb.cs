using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Scripts.GameComponents;
using System.Security.Principal;
namespace Scripts.Game;

public class TimedBomb() : Object
{
    public static Texture2D objectTexture {get; set;}
    protected override Texture2D Texture => objectTexture;
    protected override Rectangle sourceRectangle => new Rectangle(0,  0, Texture.Width, Texture.Height);
    protected override bool pushable => true;
    protected override bool destructible => false;
    protected override float scale => 0.2f;
    protected override Vector2 origin => new Vector2(Texture.Width/2, Texture.Height/2);
    private int remainingTime = 2;

    public override void SnapBehavior()
    {
        if(isDestroyed) return;
        if(remainingTime == 1)
        {
            //TODO: Play explosion animation and sound effect
            this.isDestroyed = true;
        } else
        {
            remainingTime--;
        }
    }
}