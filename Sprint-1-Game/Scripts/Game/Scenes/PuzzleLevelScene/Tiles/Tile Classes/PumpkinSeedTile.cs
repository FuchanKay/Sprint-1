namespace Scripts.Game;

public class PumpkinSeedTile : ITile
{
    public ITile Clone()
    {
        return new PumpkinSeedTile
        {
            Direction = Direction
        };
    }
    public bool Equals(ITile other)
    {
        return other.Id == TileIds.PumpkinSeed && Direction == other.Direction;
    }
    public TileIds Id => TileIds.PumpkinSeed;
    public string RegionName => RegionNames.PumpkinSeedTile;
    public bool IsRotatable => true;
    public bool IsWalkable => true;
    public Directions Direction { get; set; }
}