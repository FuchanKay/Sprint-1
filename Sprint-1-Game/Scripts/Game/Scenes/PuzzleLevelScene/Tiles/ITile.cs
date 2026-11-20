namespace Scripts.Game;

public interface ITile
{
    ITile Clone();
    bool Equals(ITile other);
    TileIds Id { get; }
    string RegionName { get; }
    bool IsWalkable { get; }
    bool IsRotatable { get; }
    Directions Direction { get; set; }
}