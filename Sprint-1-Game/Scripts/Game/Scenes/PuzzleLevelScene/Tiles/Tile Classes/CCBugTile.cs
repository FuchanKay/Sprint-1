namespace Scripts.Game;

public class CCBugTile : ITile
{
    public ITile Clone()
    {
        return new CCBugTile
        {
            
        };
    }
    public TileIds Id => TileIds.CCBug;
    public bool IsWalkable => true;
}