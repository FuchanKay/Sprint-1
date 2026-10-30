namespace Scripts.Game;

public class PitTile : ITile
{
    public ITile Clone()
    {
        return new PitTile
        {

        };
    }
    public TileIds Id => TileIds.Pit;
    public bool IsWalkable => false;
    public Directions Direction { get; set; }
}