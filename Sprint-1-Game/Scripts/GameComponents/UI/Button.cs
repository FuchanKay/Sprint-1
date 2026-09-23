using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

public abstract class Button : IButton
{
    protected IInputManager Input;
    protected Vector2 Coords;
    protected int Width => Texture.Width;
    protected int Height => Texture.Height / 2;

    protected abstract Texture2D Texture { get; }
    protected abstract void OnClick();

    public void Init(int x, int y, IInputManager input)
    {
        Input = input;
        Coords = new Vector2(x, y);
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
            Color.White
        );
    }
    protected bool IsHot()
    {
        var mouseX = Input.MousePositionX;
        var mouseY = Input.MousePositionY;

        var hotX = Coords.X <= mouseX && mouseX <= Coords.X + Width;
        var hotY = Coords.Y <= mouseY && mouseY <= Coords.Y + Height;

        return hotX && hotY;
    }
}