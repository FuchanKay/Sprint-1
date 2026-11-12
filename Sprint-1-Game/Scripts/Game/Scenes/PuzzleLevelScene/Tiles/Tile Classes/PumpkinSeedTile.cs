namespace Scripts.Game;

public class PumpkinSeedTile : ITile
{
    public ITile Clone()
    {
        return new PumpkinSeedTile
        {

        };
    }
    public TileIds Id => TileIds.PumpkinSeed;
    public string RegionName => RegionNames.PumpkinSeedTile;
    public bool IsRotatable => true;
    public bool IsWalkable => true;
    public Directions Direction { get; set; }
}