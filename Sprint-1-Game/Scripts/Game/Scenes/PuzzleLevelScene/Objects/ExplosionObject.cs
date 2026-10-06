namespace Scripts.Game;

public class ExplosionObject() : IObject
{
    public ObjectIds Id => ObjectIds.Explosion;
    public Directions Direction { get; set; }
}