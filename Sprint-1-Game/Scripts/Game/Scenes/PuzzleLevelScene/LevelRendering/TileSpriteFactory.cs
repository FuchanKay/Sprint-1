using Scripts.GameComponents;

namespace Scripts.Game;

public class TileSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float TileLayerDepth = 0.0f;
    public ISprite CreateTileSprite(TileIds id, LevelContext context)
    {
        StaticSprite sprite;
        switch (id)
        {
            case TileIds.Brick:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.BrickTile);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.BrickTile);
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
        sprite.Scale = context.GridScale;
        sprite.LayerDepth = TileLayerDepth;

        return sprite;
    }
}