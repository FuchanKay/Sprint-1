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
        var id = obj.Id;
        switch (id)
        {
            case ObjectIds.Rock:
                sprite = new StaticSprite(textureAtlas);
                var rock = obj as RockObject;
                sprite.SetState(rock.RegionName);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.ObjectSprites);
                sprite.IdleName = rock.RegionName;
                spriteScale = context.GridScale * 4.0f;
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
                spriteScale = context.GridScale * 0.5f;
                break;
            case ObjectIds.Key:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Key);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Key);
                sprite.IdleName = RegionNames.Key;
                spriteScale = context.GridScale * 0.25f;
                break;
            case ObjectIds.Door:
                sprite = new StaticSprite(textureAtlas);
                var door = obj as DoorObject;
                if (door.IsOpen) sprite.SetState(RegionNames.OpenDoor);
                else sprite.SetState(RegionNames.ClosedDoor);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Door);
                sprite.IdleName = RegionNames.ClosedDoor;
                spriteScale = context.GridScale * 12.0f;
                break;
            case ObjectIds.TimedBomb:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.TimedBomb);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.Color = Color.Red;
                sprite.IdleName = RegionNames.TimedBomb;
                spriteScale = context.GridScale * 0.5f;
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
                var directionState = RegionNames.ToIdleRegionName(obj.Direction);
                sprite.SetState(directionState);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.BlueLizard);
                spriteScale = context.GridScale * 4.0f;
                sprite.IdleName = directionState;
                break;
            case ObjectIds.RedLizard:
                sprite = new StaticSprite(textureAtlas);
                directionState = RegionNames.ToIdleRegionName(obj.Direction);
                sprite.SetState(directionState);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.RedLizard);
                spriteScale = context.GridScale * 4.0f;
                sprite.IdleName = directionState;
                break;
            case ObjectIds.Bat:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Bat);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bat);
                spriteScale = context.GridScale * 8.0f;
                sprite.IdleName = RegionNames.Bat;
                break;
            case ObjectIds.Frog:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.FrogIdleS);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Frog);
                spriteScale = context.GridScale * 6.5f;
                sprite.IdleName = RegionNames.FrogIdleS;
                break;
            case ObjectIds.Zombie:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.ZombieIdleS);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Zombie);
                spriteScale = context.GridScale * 5.0f;
                sprite.IdleName = RegionNames.ZombieIdleS;
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
        sprite.CurrentPosition = coord;
        sprite.SetTargetPosition(coord);
        sprite.LayerDepth = ObjectLayerDepth;
        if (obj.Id == ObjectIds.Door) sprite.LayerDepth = 0.45f;
        sprite.Scale = spriteScale;

        return sprite;
    }
}