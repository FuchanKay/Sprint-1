namespace Scripts.Game;

public class EmptyObject : IObject
{
    public ObjectIds Id => ObjectIds.Empty;
    public Directions Direction { get; set; }

}