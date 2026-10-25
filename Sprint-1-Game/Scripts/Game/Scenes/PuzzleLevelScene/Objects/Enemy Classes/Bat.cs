namespace Scripts.Game;

public class Bat : IEnemy
{
    public IObject Clone()
    {
        return new Bat
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Bat;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => false;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public int Health { get; set; } = 1;
}