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
    public TileIds Id => TileIds.Floor;
    public string RegionName { get; set; }
    public bool IsWalkable => true;
    public Directions Direction { get; set; }
}