namespace Scripts.Game;

public class DoorObject : IObject
{
    public IObject Clone()
    {
        return new DoorObject
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Door;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => false;
    public bool IsDestructible => false;
    public bool IsRotatable => false;
    public bool IsOpen { get; set; } = false;
}