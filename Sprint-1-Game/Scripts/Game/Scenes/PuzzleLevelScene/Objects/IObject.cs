namespace Scripts.Game;

public interface IObject
{
    ObjectIds Id { get; }
    Directions Direction { get; set; }
    bool isDestructible { get; }
    bool isPushable { get; }
}