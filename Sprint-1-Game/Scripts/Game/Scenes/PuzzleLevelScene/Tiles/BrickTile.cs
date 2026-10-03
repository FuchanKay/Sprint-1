using Scripts.GameComponents;

namespace Scripts.Game;

public class BrickTile : ITile
{
    public TileIds Id => TileIds.Brick;
    public ISprite Sprite { get; set; }
}