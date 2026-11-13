using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ObjectSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float ObjectLayerDepth = 0.5f;
    public ISprite CreateObjectSprite(IObject obj, Vector2 coord, LevelContext context)
    {
        ISprite sprite;
        var spriteScale = context.GridScale;
        string regionName = RegionNames.Bomb;
        var id = obj.Id;
        switch (id)
        {
            case ObjectIds.Rock:
                sprite = new StaticSprite(textureAtlas);
                var rock = obj as RockObject;
                regionName = rock.RegionName;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.ObjectSprites);
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.PumpkinSeed:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.PumpkinSeed;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.ObjectSprites);
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.VineSeed:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.VineSeed;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.ObjectSprites);
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Player:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.ToIdleRegionName(obj.Direction);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Player);
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Wall:
                sprite = new StaticSprite(textureAtlas);
                var wall = obj as WallObject;
                regionName = wall.RegionName;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.ObjectSprites);
                sprite.Rotation = MathHelper.ToRadians((int)obj.Direction * 90);
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Bomb:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.Bomb;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                spriteScale = context.GridScale * 0.5f;
                break;
            case ObjectIds.Key:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.Key;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Key);
                spriteScale = context.GridScale * 0.25f;
                break;
            case ObjectIds.Door:
                sprite = new StaticSprite(textureAtlas);
                var door = obj as DoorObject;
                if (door.IsOpen) regionName = RegionNames.OpenDoor;
                else regionName = RegionNames.ClosedDoor;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Door);
                spriteScale = context.GridScale * 12.0f;
                break;
            case ObjectIds.TimedBomb:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.TimedBomb;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.Color = Color.Red;
                spriteScale = context.GridScale * 0.5f;
                break;
            case ObjectIds.Skeleton:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.Skeleton;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Skeleton);
                break;
            case ObjectIds.Warlock:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.Warlock;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Warlock);
                break;
            case ObjectIds.BlueLizard:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.ToIdleRegionName(obj.Direction);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.BlueLizard);
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.RedLizard:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.ToIdleRegionName(obj.Direction);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.RedLizard);
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Bat:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.Bat;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bat);
                spriteScale = context.GridScale * 8.0f;
                break;
            case ObjectIds.Frog:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.FrogIdleS;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Frog);
                spriteScale = context.GridScale * 6.5f;
                break;
            case ObjectIds.Zombie:
                sprite = new StaticSprite(textureAtlas);
                regionName = RegionNames.ZombieIdleS;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Zombie);
                spriteScale = context.GridScale * 5.0f;
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
        sprite.SetState(regionName);
        sprite.IdleName = regionName;

        sprite.CurrentPosition = coord;
        sprite.SetTargetPosition(coord);

        sprite.LayerDepth = ObjectLayerDepth;
        if (obj.Id == ObjectIds.Door) sprite.LayerDepth = 0.45f;
        sprite.Scale = spriteScale;

        return sprite;
    }
}