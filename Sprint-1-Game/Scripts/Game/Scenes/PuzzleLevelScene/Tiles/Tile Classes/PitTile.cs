namespace Scripts.Game;

public class PitTile : ITile
{
    public ITile Clone()
    {
        return new PitTile
        {

        };
    }
    public bool Equals(ITile other)
    {
        return other.Id == TileIds.Pit;
    }
    public TileIds Id => TileIds.Pit;
    public string RegionName => RegionNames.PitTile;
    public bool IsRotatable => false;
    public bool IsWalkable => false;
    public Directions Direction { get; set; }
}