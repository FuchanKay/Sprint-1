using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Scripts.GameComponents;

public class UIFont(UIFontAtlas uIFontAtlas)
{
    private SpriteFont Font{ get; set; }
    public string Text{ get; set; }
    public Vector2 Pos{ get; set; }
    public Color Color{ get; set; } = Color.White;
    public float Rotation{ get; set; } = 0.0f;
    public Vector2 Origin{ get; set; } = Vector2.Zero;
    public Vector2 Scale{ get; set; } = Vector2.One;
    public SpriteEffects Effects{ get; set; } = SpriteEffects.None;
    public float LayerDepth{ get; set; } = 0.0f;
    public void Init(string fontName)
    {
        Font = uIFontAtlas.GetFont(fontName);
    } 
    public void Draw(SpriteBatch sb)
    {
        sb.DrawString(Font, Text, Pos, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
}