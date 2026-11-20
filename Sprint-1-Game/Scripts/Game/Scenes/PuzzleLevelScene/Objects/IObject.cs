namespace Scripts.Game;

public interface IObject
{
    IObject Clone();
    bool Equals(IObject other);
    ObjectIds Id { get; }
    Directions Direction { get; set; }
    string RegionName { get; }
    bool IsDestructible { get; }
    bool IsPushable { get; }
    bool IsRotatable { get; }
    bool IsPickable { get; }
}