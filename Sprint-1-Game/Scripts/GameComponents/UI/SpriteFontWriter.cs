using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

public class BasicSpriteFontWriter(SpriteFontManager spriteFontManager) : ISpriteFontWriter
{
    private SpriteFont Font;
    private Vector2 Pos;
    private string Text;
    private Color Color;
    public void Init(string fontName, Vector2 pos, string text, Color color)
    {
        Font = spriteFontManager.GetFont(fontName);
        Pos = pos;
        Text = text;
        Color = color;
    }
    public void Update(Vector2 pos)
    {
        Pos = pos;
    }
    public void Update(Vector2 pos, string text)
    {
        Pos = pos;
        Text = text;
    }
    public void Update(Vector2 pos, string text, Color color)
    {
        Pos = pos;
        Text = text;
        Color = color;
    }
    public void Update(SpriteFont font, Vector2 pos, string text, Color color)
    {
        Font = font;
        Pos = pos;
        Text = text;
        Color = color;
    }
    public void Draw(SpriteBatch sb)
    {
        sb.DrawString(Font, Text, Pos, Color);
    }

}