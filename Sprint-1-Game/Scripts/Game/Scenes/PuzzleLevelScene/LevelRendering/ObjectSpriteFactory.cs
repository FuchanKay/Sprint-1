using Scripts.GameComponents;
using Microsoft.Xna.Framework;

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