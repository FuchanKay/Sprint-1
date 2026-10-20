namespace Scripts.Game;

public interface ITile
{
    ITile Clone();
    TileIds Id { get; }
    bool IsWalkable { get; }
}