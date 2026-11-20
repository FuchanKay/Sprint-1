namespace Scripts.Game;

public class VineSeedObject() : IObject
{
    public IObject Clone()
    {
        return new VineSeedObject
        {
            Direction = Direction
        };
    }
    public bool Equals(IObject other)
    {
        return other.Id == ObjectIds.VineSeed && Direction == other.Direction;
    }
    public ObjectIds Id => ObjectIds.VineSeed;
    public Directions Direction { get; set; }
    public string RegionName => RegionNames.VineSeed;
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public bool IsPickable => false;

}