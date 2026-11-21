using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;
namespace Scripts.GameComponents;

public class UIFont(UIFontAtlas uIFontAtlas)
{
    private SpriteFont Font{ get; set; } = uIFontAtlas.GetFont(FontNames.DefaultFont);
    public string Text{ get; set; } = "";
    public Vector2 Pos{ get; set; } = Vector2.Zero;
    public Color Color{ get; set; } = Color.White;
    public float Rotation{ get; set; } = 0.0f;
    public GenericOrigin OriginEnum { get; set; } = GenericOrigin.TopLeft;
    public Vector2? CustomOrigin { get; set; }
    private Vector2 Origin => CustomOrigin ?? ToVector2(OriginEnum) * Size;
    public Vector2 Scale{ get; set; } = Vector2.One;
    public SpriteEffects Effects{ get; set; } = SpriteEffects.None;
    public float LayerDepth{ get; set; } = 1.0f;
    public float Width => Font.MeasureString(Text).X;
    public float Height => Font.MeasureString(Text).Y;
    public Vector2 Size => new Vector2(Width, Height);
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
    private static Vector2 ToVector2(GenericOrigin origin)
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