using Scripts.GameComponents;

namespace Scripts.Game;

public class TileFactory(ITextureAtlas textureAtlas)
{
    public ITile CreateEmptyTile()
    {
        return new EmptyTile
        {
            Sprite = new StaticSprite(textureAtlas)
        };
    }
}