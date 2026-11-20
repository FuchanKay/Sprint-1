namespace Scripts.Game;

public class FloorTile : ITile
{
    public ITile Clone()
    {
        return new FloorTile
        {
            RegionName = RegionName,
            Direction = Direction
        };
    }
    public bool Equals(ITile other)
    {
        return other.Id == TileIds.Floor && RegionName == other.RegionName && Direction == other.Direction;
    }
    public TileIds Id => TileIds.Floor;
    public string RegionName { get; set; }
    public bool IsWalkable => true;
    public bool IsRotatable => true;
    public Directions Direction { get; set; }
}