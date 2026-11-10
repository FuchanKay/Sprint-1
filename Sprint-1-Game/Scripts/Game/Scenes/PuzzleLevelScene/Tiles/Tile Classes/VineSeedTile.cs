namespace Scripts.Game;

public class VineSeedTile : ITile
{
    public ITile Clone()
    {
        return new VineSeedTile
        {
            Direction = Direction
        };
    }
    public TileIds Id => TileIds.VineSeed;
    public bool IsRotatable => true;
    public bool IsWalkable => true;
    public Directions Direction { get; set; } = Directions.North;
}