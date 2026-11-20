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
    public bool Equals(ITile other)
    {
        return other.Id == TileIds.Ice;
    }
    public TileIds Id => TileIds.Ice;
    public string RegionName => RegionNames.IceTile;
    public bool IsWalkable => true;
    public bool IsRotatable => false;
    public Directions Direction { get; set; }
}