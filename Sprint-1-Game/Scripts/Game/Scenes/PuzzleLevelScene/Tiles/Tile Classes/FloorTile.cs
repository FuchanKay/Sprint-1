namespace Scripts.Game;

public class FloorTile : ITile
{
    public TileIds Id => TileIds.Floor;
    public string RegionName { get; set; }
    public bool IsWalkable => true;
}