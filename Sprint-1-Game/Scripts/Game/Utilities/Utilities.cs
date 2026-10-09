namespace Scripts.Game;

public class Utilities
{
    public static Coordinate ToCoordinate(Directions direction)
    {
        return direction switch
        {
            Directions.North => Coordinate.North,
            Directions.East => Coordinate.East,
            Directions.South => Coordinate.South,
            Directions.West => Coordinate.West,
            _ => Coordinate.Zero,
        };
    }


}