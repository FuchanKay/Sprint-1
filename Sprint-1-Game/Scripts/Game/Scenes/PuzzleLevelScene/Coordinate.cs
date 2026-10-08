using Microsoft.Xna.Framework;
namespace Scripts.Game;

public struct Coordinate
{
    public int X { get; }
    public int Y { get; }

    // Constructor
    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(X, Y);
    }

    // Operator overloading for vector addition
    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }

    // Override ToString for readable logging
    public override string ToString() => $"({X}, {Y})";
}