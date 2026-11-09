namespace Scripts.Game;

public class SeedObject() : IObject
{
    public IObject Clone()
    {
        return new SeedObject
        {
            Direction = Direction,
            RegionName = RegionName,
        };
    }
    public ObjectIds Id => ObjectIds.Seed;
    public Directions Direction { get; set; }
    public string RegionName { get; set; }
    public string TextureName { get; set; }
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public bool IsPickable => false;

}