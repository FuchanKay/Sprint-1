namespace Scripts.Game;

public class LavaTile : ITile
{
    public ITile Clone()
    {
        return new LavaTile
        {

        };
    }
    public TileIds Id => TileIds.Lava;
    public string RegionName => RegionNames.LavaTile;
    public bool IsWalkable => false;
    public bool IsRotatable => false;

    public Directions Direction { get; set; }
}