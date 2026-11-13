namespace Scripts.Game;

public class BombObject : IObject
{
    public IObject Clone()
    {
        return new BombObject
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Bomb;
    public Directions Direction { get; set; } = Directions.South;
    public string RegionName => RegionNames.Bomb;
    public bool IsPushable => true;
    public bool IsDestructible => false;
    public bool IsRotatable => false;
    public bool IsPickable => true;

}