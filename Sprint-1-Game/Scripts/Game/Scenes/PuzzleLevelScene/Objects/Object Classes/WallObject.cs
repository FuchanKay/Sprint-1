namespace Scripts.Game;

public class WallObject() : IObject
{
    public ObjectIds Id => ObjectIds.Wall;
    public Directions Direction { get; set; }
    public bool IsPushable => false;
    public bool IsDestructible => false;
    public bool IsRotatable => false;
}