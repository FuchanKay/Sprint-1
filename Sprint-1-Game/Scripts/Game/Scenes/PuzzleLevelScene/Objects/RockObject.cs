using Scripts.GameComponents;

namespace Scripts.Game;

public class RockObject() : IObject
{
    public ObjectIds Id => ObjectIds.Rock;
    public Directions Direction { get; set; }
    public ISprite Sprite { get; set; }
}