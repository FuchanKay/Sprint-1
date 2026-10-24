namespace Scripts.Game;

public class KeyObject : IObject
{
    public IObject Clone()
    {
        return new KeyObject
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Key;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => false;
    public bool IsDestructible => false;
    public bool IsRotatable => false;
}