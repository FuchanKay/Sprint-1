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
    public ObjectIds Id => ObjectIds.Wall;
    public Directions Direction { get; set; } = Directions.North;
    public string RegionName { get; set; }
    public bool IsPushable => false;
    public bool IsDestructible => false;
    public bool IsRotatable => false;
    public bool IsPickable => false;
}