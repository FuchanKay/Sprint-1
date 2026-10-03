using Scripts.Game;
using Scripts.GameComponents;

public class ObjectFactory(ITextureAtlas textureAtlas)
{
    public IObject CreateEmpty()
    {
        return new EmptyObject
        {
            Direction = Directions.South,
            Sprite = new StaticSprite(textureAtlas)
        };
    }

    public IObject CreatePlayer()
    {
        var sprite = new StaticSprite(textureAtlas);
        sprite.SetState(RegionNames.Rock);
        sprite.Texture = textureAtlas.GetTexture(TextureNames.Rock);
        sprite.Scale = 0.2f;
        return new PlayerObject
        {
            Direction = Directions.South,
            Sprite = sprite
        };
    }

    public IObject CreateRock()
    {
        var sprite = new StaticSprite(textureAtlas);
        sprite.SetState(RegionNames.Rock);
        sprite.Texture = textureAtlas.GetTexture(TextureNames.Rock);
        sprite.Scale = 0.2f;
        return new RockObject
        {
            Direction = Directions.South,
            Sprite = sprite
        };
    }

    public IObject CreateWall()
    {
        var sprite = new StaticSprite(textureAtlas);
        sprite.SetState(RegionNames.Wall);
        sprite.Texture = textureAtlas.GetTexture(TextureNames.Wall);
        sprite.Scale = 0.2f;
        return new WallObject
        {
            Direction = Directions.South,
            Sprite = sprite
        };
    }
}