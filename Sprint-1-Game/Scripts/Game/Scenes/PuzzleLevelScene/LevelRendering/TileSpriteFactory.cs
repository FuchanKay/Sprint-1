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
            case TileIds.Dirt:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.DirtTile);
                break;
            case TileIds.Grass:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.GrassTile);
                break;
            case TileIds.Brick:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.BrickTile);
                break;
            case TileIds.MossyBrick:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.MossyBrickTile);
                break;
            case TileIds.Pit:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.PitTile);
                break;
            case TileIds.Water:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.WaterTile);
                break;
            case TileIds.Lava:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.LavaTile);
                break;
            case TileIds.Bug:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.BugTile);
                sprite.IdleName = RegionNames.BugTile;
                break;
            case TileIds.CCBug:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.BugTileCC);
                sprite.IdleName = RegionNames.BugTileCC;
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
        sprite.Texture = textureAtlas.GetTexture(TextureNames.TileSprites);
        sprite.SetTargetPosition(coord);
        sprite.Scale = context.GridScale * 4.0f;
        sprite.LayerDepth = TileLayerDepth;

        return sprite;
    }
}