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
<<<<<<< HEAD
=======
        string regionName = RegionNames.Bomb;
>>>>>>> origin/main
        var id = obj.Id;
        switch (id)
        {
            case ObjectIds.Rock:
                sprite = new StaticSprite(textureAtlas);
                var rock = obj as RockObject;
<<<<<<< HEAD
                sprite.SetState(rock.RegionName);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.ObjectSprites);
                sprite.IdleName = rock.RegionName;
=======
                regionName = rock.RegionName;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.ObjectSprites);
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Seed:
                sprite = new StaticSprite(textureAtlas);
                var seed = obj as SeedObject;
                regionName = seed.RegionName;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.ObjectSprites);
>>>>>>> origin/main
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Player:
                sprite = new StaticSprite(textureAtlas);
<<<<<<< HEAD
                var idleState = RegionNames.ToIdleRegionName(obj.Direction);
                sprite.SetState(idleState);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Player);
                sprite.IdleName = idleState;
=======
                regionName = RegionNames.ToIdleRegionName(obj.Direction);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Player);
>>>>>>> origin/main
                spriteScale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Wall:
                sprite = new StaticSprite(textureAtlas);
<<<<<<< HEAD
                sprite.SetState(RegionNames.Wall);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Wall);
                sprite.IdleName = RegionNames.Wall;
                break;
            case ObjectIds.Bomb:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Bomb);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.IdleName = RegionNames.Bomb;
=======
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
>>>>>>> origin/main
                spriteScale = context.GridScale * 0.5f;
                break;
            case ObjectIds.Key:
                sprite = new StaticSprite(textureAtlas);
<<<<<<< HEAD
                sprite.SetState(RegionNames.Key);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Key);
                sprite.IdleName = RegionNames.Key;
=======
                regionName = RegionNames.Key;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Key);
>>>>>>> origin/main
                spriteScale = context.GridScale * 0.25f;
                break;
            case ObjectIds.Door:
                sprite = new StaticSprite(textureAtlas);
                var door = obj as DoorObject;
<<<<<<< HEAD
                if (door.IsOpen) sprite.SetState(RegionNames.OpenDoor);
                else sprite.SetState(RegionNames.ClosedDoor);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Door);
                sprite.IdleName = RegionNames.ClosedDoor;
=======
                if(door.IsOpen) regionName = RegionNames.OpenDoor;
                else regionName = RegionNames.ClosedDoor;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Door);
>>>>>>> origin/main
                spriteScale = context.GridScale * 12.0f;
                break;
            case ObjectIds.TimedBomb:
                sprite = new StaticSprite(textureAtlas);
<<<<<<< HEAD
                sprite.SetState(RegionNames.TimedBomb);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.Color = Color.Red;
                sprite.IdleName = RegionNames.TimedBomb;
=======
                regionName = RegionNames.TimedBomb;
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.Color = Color.Red;
>>>>>>> origin/main
                spriteScale = context.GridScale * 0.5f;
                break;
            case ObjectIds.Skeleton:
                sprite = new StaticSprite(textureAtlas);
<<<<<<< HEAD
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
=======
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
>>>>>>> origin/main
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
<<<<<<< HEAD
        sprite.CurrentPosition = coord;
        sprite.SetTargetPosition(coord);
=======
        sprite.SetState(regionName);
        sprite.IdleName = regionName;

        sprite.CurrentPosition = coord;
        sprite.SetTargetPosition(coord);

>>>>>>> origin/main
        sprite.LayerDepth = ObjectLayerDepth;
        sprite.Scale = spriteScale;

        return sprite;
    }
}