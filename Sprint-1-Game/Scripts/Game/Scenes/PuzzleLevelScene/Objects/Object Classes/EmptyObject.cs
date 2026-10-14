namespace Scripts.Game;

public class EmptyObject : IObject
{
    public ObjectIds Id => ObjectIds.Empty;
    public Directions Direction { get; set; }
    public bool IsPushable => false;
    public bool IsDestructible => false;

}