namespace Scripts.Game;

public class EmptyTile : ITile
{
    public TileIds Id => TileIds.Empty;
    public bool IsWalkable => false;
}