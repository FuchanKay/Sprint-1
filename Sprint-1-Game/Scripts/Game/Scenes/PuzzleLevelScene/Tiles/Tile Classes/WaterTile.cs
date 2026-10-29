namespace Scripts.Game;

public class WaterTile : ITile
{
    public ITile Clone()
    {
        return new WaterTile
        {

        };
    }
    public TileIds Id => TileIds.Water;
    public bool IsWalkable => false;
    public Directions Direction { get; set; }
}