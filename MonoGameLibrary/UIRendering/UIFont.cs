using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MonoGameLibrary.UIRendering;

public class UIFont
{
    public SpriteFont Font{ get; set; }
    public string Text{ get; set; }
    public Vector2 Pos{ get; set; }
    public Color Color{ get; set; } = Color.White;
    public float Rotation{ get; set; } = 0.0f;
    public Vector2 Origin{ get; set; } = Vector2.Zero;
    public Vector2 Scale{ get; set; } = Vector2.One;
    public SpriteEffects Effects{ get; set; } = SpriteEffects.None;
    public float LayerDepth{ get; set; } = 0.0f;
    public UIFont(UITextureRegion region)
    {
        Font = UIFontAtlas;
    }
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