namespace Scripts.Game;

public class BugTile : ITile
{
    public ITile Clone()
    {
        return new BugTile
        {

        };
    }
    public TileIds Id => TileIds.Bug;
    public string RegionName => RegionNames.BugTile;
    public bool IsWalkable => true;
    public bool IsRotatable => false;
    public Directions Direction { get; set; }
}