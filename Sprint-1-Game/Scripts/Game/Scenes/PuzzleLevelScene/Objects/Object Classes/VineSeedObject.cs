namespace Scripts.Game;

public class VineSeedObject() : IObject
{
    public IObject Clone()
    {
        return new VineSeedObject
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.VineSeed;
    public Directions Direction { get; set; }
    public bool IsPushable => true;
    public bool IsDestructible => true;
    public bool IsRotatable => false;
    public bool IsPickable => false;

}