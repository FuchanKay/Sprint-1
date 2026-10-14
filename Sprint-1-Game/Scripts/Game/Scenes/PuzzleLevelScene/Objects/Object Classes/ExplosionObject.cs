namespace Scripts.Game;

public class ExplosionObject() : IObject
{
    public ObjectIds Id => ObjectIds.Explosion;
    public Directions Direction { get; set; }
    public bool IsPushable => false;
    public bool IsDestructible => false;
}