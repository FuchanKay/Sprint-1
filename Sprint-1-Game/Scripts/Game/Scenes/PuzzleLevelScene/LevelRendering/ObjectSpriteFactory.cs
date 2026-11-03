using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ObjectSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float ObjectLayerDepth = 1.0f;
    private readonly static float EffectLayerDepth = 0.5f;
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
            case ObjectIds.Seed:
                sprite = new StaticSprite(textureAtlas);
                var seed = obj as SeedObject;
                regionName = seed.RegionName;
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
                if(door.IsOpen) regionName = RegionNames.OpenDoor;
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
                spriteScale = context.GridScale * 6.0f;
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
        sprite.Scale = spriteScale;

        return sprite;
    }

    public ISprite CreateEffectSprite(EffectIds effectId, Vector2 coord, LevelContext context)
    {
        ISprite sprite = new AnimatedSprite(textureAtlas);
        var spriteScale = context.GridScale;
        switch (effectId)
        {
            case EffectIds.Explosion:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 8.0f;
                break;
            case EffectIds.PlayerDeath:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 3.5f;
                sprite.Color = Color.SpringGreen;
                break;
            case EffectIds.RedLaserBeam:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.LaserBeam);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.RedLizard);
                sprite.IdleName = RegionNames.LaserBeam;
                spriteScale = context.GridScale * 4.0f;
                break;
            case EffectIds.BlueLaserBeam:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.LaserBeam);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.BlueLizard);
                sprite.IdleName = RegionNames.LaserBeam;
                spriteScale = context.GridScale * 4.0f;
                break;
            case EffectIds.EnemyDeath:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 3.5f;
                sprite.Color = Color.DarkRed;
                break;
            case EffectIds.LavaSplash:
                sprite.SetState(AnimationNames.LavaSplash);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.EffectSprites);
                spriteScale = context.GridScale * 4.0f;
                break;
            case EffectIds.WaterSplash:
                sprite.SetState(AnimationNames.WaterSplash);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.EffectSprites);
                spriteScale = context.GridScale * 4.0f;
                break;
            case EffectIds.WarlockSpell:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 3.5f;
                sprite.Color = Color.Purple;
                break;
            case EffectIds.Slash:
                sprite.SetState(AnimationNames.Slash);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Slash);
                sprite.IdleName = RegionNames.Slash;
                spriteScale = context.GridScale * 0.8f;
                sprite.Color = Color.White;
                break;
            default:
                break;
        }
        sprite.CurrentPosition = coord;
        sprite.SetTargetPosition(coord);
        sprite.LayerDepth = EffectLayerDepth;
        sprite.Scale = spriteScale;
        return sprite;
    }
}