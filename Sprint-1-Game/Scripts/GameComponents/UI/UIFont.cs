using System;
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
    public float Width => Font.MeasureString(Text).X;
    public float Height => Font.MeasureString(Text).Y;
    public enum GenericOrigin
    {
        TopCenter,
        TopLeft,
        TopRight,
        Center,
        BottomCenter,
        BottomLeft,
        BottomRight,
    }
    public static Vector2 ToVector2(GenericOrigin origin)
    {
        return origin switch
        {
            GenericOrigin.TopCenter => new Vector2(0.5f, 0f),
            GenericOrigin.TopLeft => new Vector2(0f, 0f),
            GenericOrigin.TopRight => new Vector2(1f, 0f),
            GenericOrigin.Center => new Vector2(0.5f, 0.5f),
            GenericOrigin.BottomCenter => new Vector2(0.5f, 1f),
            GenericOrigin.BottomLeft => new Vector2(0f, 1f),
            GenericOrigin.BottomRight => new Vector2(1f, 1f),
        _ => throw new ArgumentOutOfRangeException(nameof(origin))
        };
    }
    public void Init(string fontName)
    {
        Font = uIFontAtlas.GetFont(fontName);
    } 
    public void Draw(SpriteBatch sb)
    {
        sb.DrawString(Font, Text, Pos, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
}