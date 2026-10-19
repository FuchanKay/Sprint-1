namespace Scripts.Game;

public class PitTile : ITile
{
    public TileIds Id => TileIds.Pit;
    public bool IsWalkable => false;
}