using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MonoGameLibrary.UIRendering;

public class UIFontRegion()
{
    public SpriteFont Font; 
    public string Text;
    public Vector2 Pos; 
    public Color Color;
    public float Rotation; 
    public Vector2 Origin; 
    public Vector2 Scale;
    public SpriteEffects Effects; 
    public float LayerDepth;
    public void Init(string fontName, Vector2 pos, string text, Color color)
    {
       // Font = spriteFontManager.GetFont(fontName);
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
        sb.DrawString(Font, Text, Pos, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
}