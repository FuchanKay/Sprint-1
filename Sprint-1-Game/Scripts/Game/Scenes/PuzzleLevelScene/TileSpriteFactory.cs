using Scripts.GameComponents;

namespace Scripts.Game;

public class TileSpriteFactory(ITextureAtlas textureAtlas)
{

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
        
        return sprite;
    }
}