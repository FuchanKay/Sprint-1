namespace Scripts.Game;

public class EmptyTile : ITile
{
    public ITile Clone()
    {
        return new EmptyTile
        {

        };
    }
    public TileIds Id => TileIds.Empty;
    public bool IsWalkable => false;
    public bool IsRotatable => false;
    public Directions Direction { get; set; }
}