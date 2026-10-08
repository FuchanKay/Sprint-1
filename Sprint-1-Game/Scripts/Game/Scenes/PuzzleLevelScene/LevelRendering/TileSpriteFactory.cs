using Scripts.GameComponents;
using Microsoft.Xna.Framework;

namespace Scripts.Game;

public class TileSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float TileLayerDepth = 0.0f;
    public ISprite CreateTileSprite(TileIds id, Vector2 coord, LevelContext context)
    {
        StaticSprite sprite;
        switch (id)
        {
            case TileIds.Brick:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.BrickTile);
                sprite.IdleName = RegionNames.BrickTile;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.BrickTile);
                break;
            case TileIds.Grass:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.GrassTile);
                sprite.IdleName = RegionNames.GrassTile;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.GrassTile);
                break;
            case TileIds.Water:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.WaterTile);
                sprite.IdleName = RegionNames.WaterTile;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.WaterTile);
                break;
            case TileIds.Lava:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.LavaTile);
                sprite.IdleName = RegionNames.LavaTile;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.LavaTile);
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
        sprite.SetTargetPosition(coord);
        sprite.Scale = context.GridScale;
        sprite.LayerDepth = TileLayerDepth;

        return sprite;
    }
}