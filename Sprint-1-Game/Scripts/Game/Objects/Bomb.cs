using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Bomb(Vector2 pos, IAudioManager am, ITextureAtlas textureAtlas) : Object(pos, am , textureAtlas)
{
    public static Texture2D ObjectTexture { get; set; }
    protected override Texture2D Texture => ObjectTexture;
    protected override string InitialState => "BombIdle";
    protected override bool Pushable => true;
    protected override bool Destructible => false;

    public override void SnapBehavior()
    {
        if(IsDestroyed) return;
        Sprite = Sprite.ConvertToAnimated("Explode");
        AudioManager.PlaySound("explosion");
        IsDestroyed = true;
    }
}