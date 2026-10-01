using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

public interface ISprite
{
    public Vector2 Position { get; set; }
    public Rectangle CurrentRegion { get; set; }
    public Color Color { get; set; }
    public float Rotation { get; set; }
    public Vector2 Origin => new Vector2(CurrentRegion.Width / 2, CurrentRegion.Height / 2);
    public float Scale { get; set; }
    public SpriteEffects Effects { get; set; }
    public float LayerDepth { get; set; }
    void Update(int dtMs);
    void Draw(SpriteBatch sb);
    void SetState(string name);
    void SetTexture(string name);
    StaticSprite ConvertToStatic(string name);
    AnimatedSprite ConvertToAnimated(string name);

}