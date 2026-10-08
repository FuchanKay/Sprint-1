namespace Scripts.Game;

public class TimedBombObject : IObject
{
    public ObjectIds Id => ObjectIds.TimedBomb;
    public Directions Direction { get; set; } = Directions.South;
    public int timeRemaining { get; set; } = 3;
    public bool isPushable => true;
    public bool isDestructible => false;
}