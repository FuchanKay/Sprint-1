namespace Scripts.Game;

public class PlayerObject : IObject
{
    public IObject Clone()
    {
        return new PlayerObject
        {
            Direction = Direction
        };
    }
    public bool Equals(IObject other)
    {
        return other.Id == ObjectIds.Player && Direction == other.Direction;
    }
    public ObjectIds Id => ObjectIds.Player;
    public Directions Direction { get; set; } = Directions.South;
    public string RegionName => RegionNames.ToIdleRegionName(Directions.South);
    public bool IsPushable => false;
    public bool IsDestructible => false;
    public bool IsRotatable => true;
    public bool IsPickable => false;

}