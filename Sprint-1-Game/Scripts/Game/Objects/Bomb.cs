using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Bomb(IAudioManager am, ITextureAtlas textureAtlas) : Object(am, textureAtlas)
{
    protected override string TextureName => "Bomb";
    protected override string InitialSpriteState => "Bomb";
    public static Texture2D ObjectTexture { get; set; }
    protected override bool Pushable => true;
    protected override bool Destructible => false;

    public override void SnapBehavior()
    {
        if (IsDestroyed) return;
        //TODO: Play explosion animation and sound effect
        IsDestroyed = true;
    }
}