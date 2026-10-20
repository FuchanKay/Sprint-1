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
    public bool IsWalkable => true;
}