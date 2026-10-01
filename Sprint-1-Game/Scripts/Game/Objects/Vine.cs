using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Vine(Vector2 pos, IAudioManager am, ITextureAtlas textureAtlas) : Object(pos, am , textureAtlas)
{
    public static Texture2D ObjectTexture { get; set; }
    protected override Texture2D Texture => ObjectTexture;
    protected override string InitialState => "VineIdle";
    protected override bool Pushable => false;
    protected override bool Destructible => false;
    public bool Grown = false;

    public override void SnapBehavior()
    {
        if(IsDestroyed) return;
        else if(!Grown)
        {
            //TODO: Grow vines
            Grown = true;
        } else
        {
            Sprite = Sprite.ConvertToAnimated("Explode");
            AudioManager.PlaySound("explosion");
            IsDestroyed = true;
        }
    }
}