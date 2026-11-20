namespace Scripts.Game;

public class EmptyObject : IObject
{
    public IObject Clone()
    {
        return new EmptyObject
        {
            Direction = Direction
        };
    }
    public bool Equals(IObject other)
    {
        return other.Id == ObjectIds.Empty;
    }
    public ObjectIds Id => ObjectIds.Empty;
    public Directions Direction { get; set; }
    public string RegionName => "";
    public bool IsPushable => false;
    public bool IsDestructible => false;
    public bool IsRotatable => false;
    public bool IsPickable => false;
}