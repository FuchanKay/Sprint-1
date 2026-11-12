using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace MonoGameLibrary.UIRendering;

public class UIFont
{
    private SpriteFont Font;
    private string Text;
    public Vector2 Pos;
    private Color Color;
    private float Rotation;
    private Vector2 Origin;
    private Vector2 Scale;
    private SpriteEffects Effects;
    private float LayerDepth;
    private float Width = 0.0f;
    private float Height = 0.0f;
    public UIFont(SpriteFont font)
    {
        Font = font;
        Rotation = 0.0f;
        Origin = Vector2.Zero;
        Scale = Vector2.One;
        Effects = SpriteEffects.None;
        LayerDepth = 0.0f;
    }
    public void Init(string text, Color color, Vector2 scale)
    {
        Text = text;
        Color = color;
        Scale = scale;
        Width = FontSize().X;
        Height = FontSize().Y;
    }

    public void UpdateText(string text)
    {
        Text = text;
        Width = FontSize().X;
        Height = FontSize().Y;
    }
    public void DrawAtOrigin(SpriteBatch sb, Vector2 pos)
    {
        CenterOrigin();
        sb.DrawString(Font, Text, pos, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
    public void Draw(SpriteBatch sb, Vector2 pos)
    {
        Pos = pos;
        sb.DrawString(Font, Text, Pos, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }
    private void CenterOrigin()
    {
        Origin = new Vector2(Width, Height) * 0.5f;
    }
    public Vector2 FontSize()
    {
        return new Vector2(Font.MeasureString(Text).X, Font.MeasureString(Text).Y);
    }
}