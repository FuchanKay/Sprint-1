using Scripts.GameComponents;

namespace Scripts.Game;

public class PlayerObject : IObject
{
    public ObjectIds Id => ObjectIds.Player;
    public Directions Direction { get; set; } = Directions.South;
    public ISprite Sprite { get; set; }
}