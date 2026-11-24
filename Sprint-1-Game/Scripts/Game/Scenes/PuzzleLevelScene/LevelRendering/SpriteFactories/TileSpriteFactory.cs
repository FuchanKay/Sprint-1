using Scripts.GameComponents;
using Microsoft.Xna.Framework;

namespace Scripts.Game;

public class TileSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float TileLayerDepth = 0.0f;
    public ISprite CreateTileSprite(ITile tile, Vector2 coord, LevelContext context)
    {
        ISprite sprite;
        var id = tile.Id;
        switch (id)
        {
            case TileIds.Floor:
                sprite = new StaticSprite(textureAtlas);
                var floor = tile as FloorTile;
                if(floor.RegionName == AnimationNames.GrassTileIdle) sprite = new AnimatedSprite(textureAtlas);
                sprite.SetState(floor.RegionName);
                sprite.IdleName = floor.RegionName;
                sprite.Rotation = Utilities.ToRadians(tile.Direction);
                break;
            case TileIds.Pit:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.PitTile);
                sprite.IdleName = RegionNames.PitTile;
                break;
            case TileIds.Water:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.WaterTile);
                sprite.IdleName = RegionNames.WaterTile;
                break;
            case TileIds.Ice:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.IceTile);
                break;
            case TileIds.Lava:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.LavaTile);
                sprite.IdleName = RegionNames.LavaTile;
                break;
            case TileIds.Bug:
                sprite = new AnimatedSprite(textureAtlas);
                sprite.SetState(AnimationNames.BugTileIdle);
                sprite.IdleName = AnimationNames.BugTileIdle;
                break;
            case TileIds.CCBug:
                sprite = new AnimatedSprite(textureAtlas);
                sprite.SetState(AnimationNames.CCBugTileIdle);
                sprite.IdleName = AnimationNames.CCBugTileIdle;
                break;
            case TileIds.VineSeed:
                sprite = new AnimatedSprite(textureAtlas);
                sprite.SetState(AnimationNames.VineSeedTileIdle);
                sprite.IdleName = AnimationNames.VineSeedTileIdle;
                break;
            case TileIds.PumpkinSeed:
                sprite = new AnimatedSprite(textureAtlas);
                sprite.SetState(AnimationNames.PumpkinSeedTileIdle);
                sprite.IdleName = AnimationNames.PumpkinSeedTileIdle;
                break;
            case TileIds.Empty:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.InventorySlot);
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