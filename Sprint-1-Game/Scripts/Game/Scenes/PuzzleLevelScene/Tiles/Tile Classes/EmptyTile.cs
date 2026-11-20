namespace Scripts.Game;

public class EmptyTile : ITile
{
    public ITile Clone()
    {
        return new EmptyTile
        {

        };
    }
    public bool Equals(ITile other)
    {
        return other.Id == TileIds.Empty;
    }
    public TileIds Id => TileIds.Empty;
    public string RegionName => "";
    public bool IsWalkable => false;
    public bool IsRotatable => false;
    public Directions Direction { get; set; }
}