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
        var id = obj.Id;
        switch (id)
        {
            case ObjectIds.Rock:
                sprite = new StaticSprite(textureAtlas);
                var rock = obj as RockObject;
                sprite.SetState(rock.RegionName);
                sprite.Texture = textureAtlas.GetTexture(rock.TextureName);
                sprite.IdleName = RegionNames.Rock;
                break;
            case ObjectIds.Player:
                sprite = new StaticSprite(textureAtlas);
                var idleState = RegionNames.ToIdleRegionName(obj.Direction);
                sprite.SetState(idleState);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Player);
                sprite.IdleName = idleState;
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Wall:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Wall);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Wall);
                sprite.IdleName = RegionNames.Wall;
                break;
            case ObjectIds.Bomb:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Bomb);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.IdleName = RegionNames.Bomb;
                break;
            case ObjectIds.Key:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Key);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Key);
                sprite.IdleName = RegionNames.Key;
                spriteScale = context.GridScale * 0.25f;
                break;
            case ObjectIds.TimedBomb:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.TimedBomb);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.Color = Color.Red;
                sprite.IdleName = RegionNames.TimedBomb;
                break;
            case ObjectIds.Skeleton:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Skeleton);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Skeleton);
                sprite.IdleName = RegionNames.Skeleton;
                break;
            case ObjectIds.Warlock:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Warlock);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Warlock);
                sprite.IdleName = RegionNames.Warlock;
                break;
            case ObjectIds.BlueLizard:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.BlueLizard);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.BlueLizard);
                break;
            case ObjectIds.RedLizard:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.RedLizard);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.RedLizard);
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
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
            case EffectIds.EnemyDeath:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 3.5f;
                sprite.Color = Color.DarkRed;
                break;
            case EffectIds.LavaExplosion:
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 3.5f;
                sprite.Color = Color.OrangeRed;
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