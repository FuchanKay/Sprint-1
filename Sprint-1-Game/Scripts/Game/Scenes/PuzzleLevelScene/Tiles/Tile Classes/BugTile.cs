namespace Scripts.Game;

public class BugTile : ITile
{
    public TileIds Id => TileIds.Bug;
    public bool IsWalkable => true;
}