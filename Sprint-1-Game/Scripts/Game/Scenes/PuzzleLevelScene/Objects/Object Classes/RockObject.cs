namespace Scripts.Game;

public class RockObject() : IObject
{
    public ObjectIds Id => ObjectIds.Rock;
    public Directions Direction { get; set; }
    public string TextureName { get; set; }
    public bool IsPushable => true;
    public bool IsDestructible => true;
}