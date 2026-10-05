namespace Scripts.Game;

public class TimedBombObject : IObject
{
    public ObjectIds Id => ObjectIds.Bomb;
    public Directions Direction { get; set; } = Directions.South;
    public int timeRemaining { get; set; } = 3;
}