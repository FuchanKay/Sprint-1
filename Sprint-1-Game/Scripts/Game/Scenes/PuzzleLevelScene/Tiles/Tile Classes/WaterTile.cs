namespace Scripts.Game;

public class WaterTile : ITile
{
    public ITile Clone()
    {
        return new WaterTile
        {

        };
    }
    public bool Equals(ITile other)
    {
        return other.Id == TileIds.Water;
    }
    public TileIds Id => TileIds.Water;
    public string RegionName => RegionNames.WaterTile;
    public bool IsWalkable => false;
    public bool IsRotatable => false;
    public Directions Direction { get; set; }
}