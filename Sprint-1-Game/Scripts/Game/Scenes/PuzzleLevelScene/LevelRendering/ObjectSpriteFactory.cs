using Scripts.GameComponents;

namespace Scripts.Game;

public class ObjectSpriteFactory(ITextureAtlas textureAtlas)
{
    private readonly static float ObjectLayerDepth = 3.0f;
    public ISprite CreateObjectSprite(ObjectIds id, LevelContext context)
    {
        ISprite sprite;
        switch (id)
        {
            case ObjectIds.Rock:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Rock);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Rock);
                break;
            case ObjectIds.Player:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.PlayerIdle);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Player);
                sprite.Scale = 4f;
                break;
            case ObjectIds.Wall:
                sprite = new StaticSprite(textureAtlas);
                sprite.SetState(RegionNames.Wall);
                sprite.Texture = textureAtlas.GetTexture(TextureNames.Wall);
                break;
            default:
                sprite = new StaticSprite(textureAtlas);
                break;
        }
        sprite.Scale = context.GridScale;
        sprite.LayerDepth = ObjectLayerDepth;

        return sprite;
    }
}