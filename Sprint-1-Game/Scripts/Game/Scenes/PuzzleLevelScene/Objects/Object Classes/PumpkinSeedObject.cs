namespace Scripts.Game;

public class PumpkinSeedObject() : IObject
{
    public IObject Clone()
    {
        return new PumpkinSeedObject
        {
            Direction = Direction
        };
    }
    public bool Equals(IObject other)
    {
        return other.Id == ObjectIds.PumpkinSeed;
    }
    public ObjectIds Id => ObjectIds.PumpkinSeed;
    public Directions Direction { get; set; }
    public string RegionName => RegionNames.PumpkinSeed;
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public bool IsPickable => false;

}