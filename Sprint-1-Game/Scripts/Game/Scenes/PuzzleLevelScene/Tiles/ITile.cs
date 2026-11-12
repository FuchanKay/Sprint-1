namespace Scripts.Game;

public interface ITile
{
    ITile Clone();
    TileIds Id { get; }
    string RegionName { get; }
    bool IsWalkable { get; }
    bool IsRotatable { get; }
    Directions Direction { get; set; }
}