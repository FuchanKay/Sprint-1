using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;

namespace Scripts.GameComponents;

public abstract class Button : IButton
{
    protected MouseInputManager Input;
    protected Vector2 Coords;
    protected int Width, Height;
    protected abstract Texture2D Texture { get; }
    protected abstract void OnClick();

    public void Init(int x, int y, int width, int height, MouseInputManager input)
    {
        Input = input;

        Coords = new Vector2(x, y);
        Width = width;
        Height = height;
    }
    public void Update(int dtMs)
    {
        if (IsHot() && Input.IsPressed("Select"))
        {
            OnClick();
        }
    }

    public void Draw(SpriteBatch sb)
    {
        var topRectangle = new Rectangle(0, 0, Width, Height);
        var botRectangle = new Rectangle(0, Height, Width, Height);
        Rectangle textureRect = IsHot() ? topRectangle : botRectangle;

        sb.Draw(
            Texture,
            Coords,
            textureRect,
            Color.White,
            0.0f,
            Vector2.Zero,
            1.0f,
            SpriteEffects.None,
            0.0f
        );
    }
    protected bool IsHot()
    {
        var mouseX = Input.X;
        var mouseY = Input.Y;

        var hotX = Coords.X <= mouseX && mouseX <= Coords.X + Width;
        var hotY = Coords.Y <= mouseY && mouseY <= Coords.Y + Height;

        return hotX && hotY;
    }
}