namespace Scripts.Game;

public class Frog : IEnemy
{
    public IObject Clone()
    {
        return new Frog
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Frog;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => false;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public int Health { get; set; } = 1;
}