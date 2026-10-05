namespace Scripts.Game;

public class BombObject : IObject
{
    public ObjectIds Id => ObjectIds.Bomb;
    public Directions Direction { get; set; } = Directions.South;
    public bool isPushable => true;
    public bool isDestructible => false;
}