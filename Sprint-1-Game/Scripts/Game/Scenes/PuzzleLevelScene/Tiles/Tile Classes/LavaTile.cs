namespace Scripts.Game;

public class LavaTile : ITile
{
    public ITile Clone()
    {
        return new LavaTile
        {

        };
    }
    public bool Equals(ITile other)
    {
        return other.Id == TileIds.Lava;
    }
    public TileIds Id => TileIds.Lava;
    public string RegionName => RegionNames.LavaTile;
    public bool IsWalkable => false;
    public bool IsRotatable => false;

    public Directions Direction { get; set; }
}