using Microsoft.Xna.Framework;
namespace Scripts.Game;

public struct Coordinate
{
    public static Coordinate Zero => new(0, 0);
    public static Coordinate North => new(0, -1);
    public static Coordinate East => new(1, 0);
    public static Coordinate South => new(0, 1);
    public static Coordinate West => new(-1, 0);
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(X, Y);
    }

    public static Coordinate operator +(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X + b.X, a.Y + b.Y);
    }

    public static Coordinate operator -(Coordinate a, Coordinate b)
    {
        return new Coordinate(a.X - b.X, a.Y + b.Y);
    }

    public override string ToString() => $"({X}, {Y})";
}