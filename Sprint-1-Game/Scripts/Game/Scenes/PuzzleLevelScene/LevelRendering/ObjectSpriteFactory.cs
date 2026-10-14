using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ObjectSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float ObjectLayerDepth = 1.0f;
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
                var idleState = RegionNames.ToPlayerIdleRegionName(obj.Direction);
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
                break;
            case ObjectIds.TimedBomb:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.TimedBomb);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.Color = Color.Red;
                break;
            case ObjectIds.Explosion:
                sprite = new AnimatedSprite(textureAtlas);
                sprite.SetState(AnimationNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                spriteScale = context.GridScale * 8.0f;
                sprite.CurrentPosition = coord;
                break;
            case ObjectIds.Skeleton:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Skeleton);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Skeleton);
                break;
            case ObjectIds.Warlock:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Warlock);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Warlock);
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
        sprite.SetTargetPosition(coord);
        sprite.LayerDepth = ObjectLayerDepth;
        sprite.Scale = spriteScale;

        return sprite;
    }
}