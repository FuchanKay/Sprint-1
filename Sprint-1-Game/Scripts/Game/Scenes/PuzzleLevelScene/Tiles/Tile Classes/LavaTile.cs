namespace Scripts.Game;

public class LavaTile : ITile
{
    public ITile Clone()
    {
        return new LavaTile
        {

        };
    }
    public TileIds Id => TileIds.Lava;
    public bool IsWalkable => false;
    public Directions Direction { get; set; }
}