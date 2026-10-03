using Scripts.GameComponents;

namespace Scripts.Game;

public class WallObject() : IObject
{
    public ObjectIds Id => ObjectIds.Wall;
    public Directions Direction { get; set; }
}