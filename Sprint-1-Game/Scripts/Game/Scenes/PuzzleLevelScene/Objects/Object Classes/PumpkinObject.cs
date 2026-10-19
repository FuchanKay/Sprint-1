namespace Scripts.Game;

public class PumpkinObject : IObject
{
    public ObjectIds Id => ObjectIds.Pumpkin;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable  => false;
    public bool IsGrown { get; set; } = false;
}