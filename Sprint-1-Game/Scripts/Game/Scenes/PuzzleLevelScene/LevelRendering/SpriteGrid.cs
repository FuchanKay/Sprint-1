using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SpriteGrid(ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    private readonly Dictionary<Vector2, ISprite> ObjectSprites = [];
    private readonly Dictionary<Vector2, ISprite> TileSprites = [];

    public void Init(GridPointer gridPointer, LevelContext context)
    {
        ObjectSprites.Clear();
        TileSprites.Clear();
        for (int i = 0; i < context.LevelWidth * context.LevelHeight; i++)
        {
            var x = i % context.LevelWidth;
            var y = i / context.LevelHeight;
            var coord = new Vector2(x, y);

            var grid = gridPointer.GetGrid(coord);
            var obj = grid.Object;
            var tile = grid.Tile;

            var spriteWidth = (int)(context.GridWidthPx * context.GridScale);
            var spriteX = spriteWidth * coord.X + context.PuzzleOffsetX;
            var spriteHeight = (int)(context.GridHeightPx * context.GridScale);
            var spriteY = spriteHeight * coord.Y + context.PuzzleOffsetY;

            var spriteCoord = new Vector2(spriteX, spriteY);

            /*
                Currently, this dictionary holds the literal window position corresponding to an ISprite object. 
                {
                    V the key where it is literally drawn V
                    (234, 234) => ISprite
                }
                ideally, the map should look like this:
                {
                    V the key can be used to lookup the sprite and cause some animation V
                    (2, 2) => {
                        Isprite,
                        LiteralPosition = (234 234),
                        ...
                    }
                }
            */
            if (obj.Id != ObjectIds.Empty)
            {
                ObjectSprites.Add(spriteCoord, objectSpriteFactory.CreateObjectSprite(obj.Id, context));
            }
            if (tile.Id != TileIds.Empty)
            {
                TileSprites.Add(spriteCoord, tileSpriteFactory.CreateTileSprite(tile.Id, context));
            }
        }
    }

    public void Update(int dtMs)
    {

    }

    public void Draw(SpriteBatch sb)
    {
        foreach (var coordAndTileSprite in TileSprites)
        {
            var coord = coordAndTileSprite.Key;
            var tileSprite = coordAndTileSprite.Value;
            tileSprite.Position = coord;
            tileSprite.Draw(sb);
        }
        foreach (var coordAndObjSprite in ObjectSprites)
        {
            var coord = coordAndObjSprite.Key;
            var objectSprite = coordAndObjSprite.Value;
            objectSprite.Position = coord;
            objectSprite.Draw(sb);
        }
    }
}