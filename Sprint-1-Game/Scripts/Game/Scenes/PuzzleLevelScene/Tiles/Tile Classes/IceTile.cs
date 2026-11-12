using System.Data.SqlTypes;

namespace Scripts.Game;

public class IceTile : ITile
{
    public ITile Clone()
    {
        return new IceTile
        {

        };
    }
    public TileIds Id => TileIds.Ice;
    public bool IsWalkable => true;
    public bool IsRotatable => false;
    public Directions Direction { get; set; }
}