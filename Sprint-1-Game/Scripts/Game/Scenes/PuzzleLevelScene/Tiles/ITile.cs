namespace Scripts.Game;

public interface ITile
{
    TileIds Id { get; }
    bool IsWalkable { get; }
}