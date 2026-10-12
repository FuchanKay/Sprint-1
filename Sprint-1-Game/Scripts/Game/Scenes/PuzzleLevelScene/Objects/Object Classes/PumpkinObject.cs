namespace Scripts.Game;

public class PumpkinObject : IObject
{
    public ObjectIds Id => ObjectIds.Pumpkin;
    public Directions Direction { get; set; } = Directions.South;
    public bool isPushable => true;
    public bool isDestructible => true;
    public bool isGrown { get; set; } = false;
}