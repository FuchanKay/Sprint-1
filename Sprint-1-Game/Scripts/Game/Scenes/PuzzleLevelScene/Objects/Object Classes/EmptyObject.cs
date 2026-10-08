namespace Scripts.Game;

public class EmptyObject : IObject
{
    public ObjectIds Id => ObjectIds.Empty;
    public Directions Direction { get; set; }
    public bool isPushable => false;
    public bool isDestructible => false;

}