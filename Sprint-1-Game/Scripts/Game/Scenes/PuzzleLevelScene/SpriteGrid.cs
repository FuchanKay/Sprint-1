using System.Collections.Generic;
using System.Net.Mime;
using System.Numerics;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SpriteGrid (ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory, ITextureAtlas textureAtlas)
{
    private readonly Dictionary<Vector2, ISprite> ObjectSprites = [];
    private readonly Dictionary<Vector2, ISprite> TileSprites = [];


    public void Init(GridPointer gridPointer, LevelContext context)
    {
        for (int i = 0; i < context.LevelWidth * context.LevelHeight; i++)
        {
            var x = i % context.LevelWidth;
            var y = i / context.LevelHeight;
            var coord = new Vector2(x, y);

            var grid = gridPointer.GetGrid(coord);
            var obj = grid.Object;
            var tile = grid.Tile;

            var spriteWidth = (int) (context.GridWidthPx * context.GridScale);
            var spriteX = spriteWidth * x + context.PuzzleOffsetX;
            var spriteHeight = (int) (context.GridHeightPx * context.GridScale);
            var spriteY = spriteHeight * y + context.PuzzleOffsetY;

            var spriteCoord = new Vector2(spriteX, spriteY);

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