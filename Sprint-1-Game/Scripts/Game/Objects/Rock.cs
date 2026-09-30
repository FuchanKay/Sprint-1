using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Rock(Vector2 pos, IAudioManager am, ITextureAtlas textureAtlas) : Object(pos, am , textureAtlas)
{
    public static Texture2D ObjectTexture { get; set; }
    protected override Texture2D Texture => ObjectTexture;
    protected override string IdleState => "BombIdle";
    protected override bool Pushable => true;
    protected override bool Destructible => true;

    public override void SnapBehavior()
    {
    }
}