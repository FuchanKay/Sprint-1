namespace Scripts.Game;

public class WallObject() : IObject
{
    public IObject Clone()
    {
        return new WallObject
        {
            Direction = Direction,
            RegionName = RegionName
        };
    }
    public bool Equals(IObject other)
    {
        return other.Id == ObjectIds.Wall && RegionName == other.RegionName;
    }
    public ObjectIds Id => ObjectIds.Wall;
    public Directions Direction { get; set; } = Directions.North;
    public string RegionName { get; set; }
    public bool IsPushable => false;
    public bool IsDestructible => false;
    public bool IsRotatable => true;
    public bool IsPickable => false;
}