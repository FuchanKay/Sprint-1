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
    public bool Equals(ITile other)
    {
        return other.Id == TileIds.VineSeed && Direction == other.Direction;
    }
    public TileIds Id => TileIds.VineSeed;
    public string RegionName => RegionNames.VineSeedTile;
    public bool IsRotatable => true;
    public bool IsWalkable => true;
    public Directions Direction { get; set; } = Directions.North;
}