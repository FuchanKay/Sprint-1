using Microsoft.Xna.Framework;

namespace Scripts.Game;

public class Utilities
{
    public static Directions RotateClockwise(Directions direction)
    {
        return direction switch
        {
            Directions.North => Directions.East,
            Directions.East => Directions.South,
            Directions.South => Directions.West,
            Directions.West => Directions.North,
            _ => Directions.North
        };
    }
    public static Directions RotateCClockwise(Directions direction)
    {
        return direction switch
        {
            Directions.North => Directions.West,
            Directions.East => Directions.North,
            Directions.South => Directions.East,
            Directions.West => Directions.South,
            _ => Directions.North
        };
    }

    public static Directions Rotate180(Directions direction)
    {
        return (Directions)(((int)direction + 2) % 4);
    }

    public static float ToRadians(Directions direction)
    {
        return MathHelper.ToRadians((int)direction * 90);
    }
}