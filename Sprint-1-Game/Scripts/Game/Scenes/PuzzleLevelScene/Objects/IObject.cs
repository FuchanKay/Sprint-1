namespace Scripts.Game;

public interface IObject
{
    ObjectIds Id { get; }
    Directions Direction { get; set; }
    bool IsDestructible { get; }
    bool IsPushable { get; }
    bool IsRotatable { get; }
}