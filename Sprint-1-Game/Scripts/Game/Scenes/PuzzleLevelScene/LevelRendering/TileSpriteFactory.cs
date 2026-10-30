using Scripts.GameComponents;
using Microsoft.Xna.Framework;

namespace Scripts.Game;

public class TileSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float TileLayerDepth = 0.0f;
    public ISprite CreateTileSprite(ITile tile, Vector2 coord, LevelContext context)
    {
        StaticSprite sprite;
        var id = tile.Id;
        switch (id)
        {
            case TileIds.Floor:
                sprite = new StaticSprite(textureAtlas);
                var floor = tile as FloorTile;
                sprite.SetState(floor.RegionName);
                sprite.Rotation = MathHelper.ToRadians((int)tile.Direction * 90);
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
            case TileIds.VineSeed:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.VineSeedTile);
                sprite.IdleName = RegionNames.VineSeedTile;
                break;
            case TileIds.PumpkinSeed:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.PumpkinSeedTile);
                sprite.IdleName = RegionNames.PumpkinSeedTile;
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
        sprite.Texture = textureAtlas.GetTexture(TextureNames.TileSprites);
        sprite.CurrentPosition = coord;
        sprite.SetTargetPosition(coord);
        sprite.Scale = context.GridScale * 4.0f;
        sprite.LayerDepth = TileLayerDepth;

        return sprite;
    }
}