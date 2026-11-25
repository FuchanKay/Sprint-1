namespace Scripts.Game;

public class WaterTile : ITile
{
    public ITile Clone()
    {
        return new WaterTile
        {

        };
    }
    public TileIds Id => TileIds.Water;
    public string RegionName => AnimationNames.WaterIdle;
    public bool IsWalkable => false;
    public bool IsRotatable => false;
    public Directions Direction { get; set; }
}