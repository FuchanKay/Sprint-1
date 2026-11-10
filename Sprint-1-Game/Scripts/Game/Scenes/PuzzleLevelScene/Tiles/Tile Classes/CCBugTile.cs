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
    public bool IsRotatable => false;
    public Directions Direction { get; set; }
}