namespace Scripts.Game;

public class WaterTile : ITile
{
    public TileIds Id => TileIds.Water;
    public bool IsWalkable => true;
}