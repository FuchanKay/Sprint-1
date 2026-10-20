namespace Scripts.Game;

public class PlayerObject : IObject
{
    public IObject Clone()
    {
        return new PlayerObject
        {
            Direction = Direction
        };
    }
    public ObjectIds Id => ObjectIds.Player;
    public Directions Direction { get; set; } = Directions.South;
    public bool IsPushable => false;
    public bool IsDestructible => false;
    public bool IsRotatable => true;
}