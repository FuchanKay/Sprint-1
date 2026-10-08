namespace Scripts.Game;

public class RockObject() : IObject
{
    public ObjectIds Id => ObjectIds.Rock;
    public Directions Direction { get; set; }
    public bool isPushable => true;
    public bool isDestructible => true;
}