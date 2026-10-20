namespace Scripts.Game;

public class LavaTile : ITile
{
    public TileIds Id => TileIds.Lava;
    public bool IsWalkable => true;
}