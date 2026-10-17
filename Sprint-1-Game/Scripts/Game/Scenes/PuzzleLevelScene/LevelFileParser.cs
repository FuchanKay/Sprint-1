using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class LevelFileParser(ObjectFactory objectFactory, TileFactory tileFactory, ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    public PuzzleLevel parseLevelFile(string levelFilePath)
    {
        var level = new PuzzleLevel(objectFactory, tileFactory, objectSpriteFactory, tileSpriteFactory);
        return level;
    }
}
