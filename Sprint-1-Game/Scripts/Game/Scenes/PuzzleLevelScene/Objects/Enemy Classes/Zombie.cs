namespace Scripts.Game;

public class Zombie : IEnemy
{
    public IObject Clone()
    {
        return new Zombie
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Zombie;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public bool IsPickable => false;
    public int Health { get; set; } = 1;
    
}