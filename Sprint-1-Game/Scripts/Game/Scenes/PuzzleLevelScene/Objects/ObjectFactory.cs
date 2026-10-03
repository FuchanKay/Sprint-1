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
        return new PlayerObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateRock()
    {
        return new RockObject
        {
            Direction = Directions.South
        };
    }

    public IObject CreateWall()
    {
        return new WallObject
        {
            Direction = Directions.South,
        };
    }
}