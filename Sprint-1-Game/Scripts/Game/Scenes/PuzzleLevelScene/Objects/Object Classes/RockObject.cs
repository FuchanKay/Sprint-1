namespace Scripts.Game;

public class RockObject() : IObject
{
    public IObject Clone()
    {
        return new RockObject
        {
            Direction = Direction,
            RegionName = RegionName,
        };
    }
    public ObjectIds Id => ObjectIds.Rock;
    public Directions Direction { get; set; }
    public string RegionName { get; set; }
    public string TextureName { get; set; }
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public bool IsPickable => false;

}