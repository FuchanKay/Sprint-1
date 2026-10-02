using Scripts.GameComponents;

namespace Scripts.Game;

public class EmptyObject : IObject
{
    public ObjectIds Id => ObjectIds.Empty;
    public Directions Direction { get; set; }
    public ISprite Sprite { get; set; }

}