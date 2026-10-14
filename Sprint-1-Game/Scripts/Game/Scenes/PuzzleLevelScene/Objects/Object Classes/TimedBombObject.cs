namespace Scripts.Game;

public class TimedBombObject : IObject
{
    public ObjectIds Id => ObjectIds.TimedBomb;
    public Directions Direction { get; set; } = Directions.South;
    public int TimeRemaining { get; set; } = 3;
    public bool IsPushable => true;
    public bool IsDestructible => false;
}