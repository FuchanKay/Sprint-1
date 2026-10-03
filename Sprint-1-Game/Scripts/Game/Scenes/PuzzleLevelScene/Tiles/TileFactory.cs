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

    public ITile CreateBrickTile()
    {
        var sprite = new StaticSprite(textureAtlas)
        {
            Texture = textureAtlas.GetTexture(TextureNames.BrickTile),
            Scale = 0.2f
        };
        sprite.SetState(RegionNames.BrickTile);
        return new BrickTile
        {
            Sprite = sprite
        };
    }
}