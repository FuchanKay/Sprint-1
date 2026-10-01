using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
namespace Scripts.Game;

public class Pylon(Vector2 pos, IAudioManager am, ITextureAtlas textureAtlas) : Object(pos, am , textureAtlas)
{
    public static Texture2D ObjectTexture { get; set; }
    protected override Texture2D Texture => ObjectTexture;
    protected override string InitialState => "PylonFullPower";
    protected override bool Pushable => false;
    protected override bool Destructible => false;
    public int SnapPower = 2;

    public override void SnapBehavior()
    {
        if (SnapPower <= 0) return;
        SnapPower--;
        if(SnapPower == 1) Sprite.SetState("PylonHalfPower");
        else if(SnapPower == 0) Sprite.SetState("PylonNoPower");
    }
}