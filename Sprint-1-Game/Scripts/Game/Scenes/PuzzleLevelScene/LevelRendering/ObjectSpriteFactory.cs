using Microsoft.Xna.Framework;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ObjectSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float ObjectLayerDepth = 1.0f;
    public ISprite CreateObjectSprite(ObjectIds id, Vector2 coord, LevelContext context)
    {
        ISprite sprite;
        switch (id)
        {
            case ObjectIds.Rock:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Rock);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Rock);
                sprite.IdleName = RegionNames.Rock;
                sprite.Scale = context.GridScale;
                break;
            case ObjectIds.Player:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.PlayerIdleSouth);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Player);
                sprite.IdleName = RegionNames.PlayerIdleSouth;
                sprite.Scale = context.GridScale * 4.0f;
                break;
            case ObjectIds.Wall:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Wall);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Wall);
                sprite.IdleName = RegionNames.Wall;
                sprite.Scale = context.GridScale;
                break;
            case ObjectIds.Bomb:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Bomb);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.Scale = context.GridScale;
                break;
            case ObjectIds.TimedBomb:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.TimedBomb);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Bomb);
                sprite.Scale = context.GridScale;
                sprite.Color = Color.Red;
                break;
            case ObjectIds.Explosion:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Explosion);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Explosion);
                sprite.IdleName = RegionNames.Explosion;
                sprite.Scale = context.GridScale * 2.0f;
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                sprite.Scale = context.GridScale;
                break;
        }
        sprite.CurrentPosition = coord;
        sprite.LayerDepth = ObjectLayerDepth;

        return sprite;
    }
}