using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SpriteGrid(ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    private readonly Dictionary<Vector2, ISprite> ObjectSprites = [];
    private readonly Dictionary<Vector2, ISprite> TileSprites = [];
    private readonly List<ISprite> AnimatingObjectSprites = [];
    private readonly List<ISprite> AnimatingTileSprites = [];
    public bool IsIdle { get; private set; }
    private LevelContext Context;

    public void Init(GridPointer gridPointer, LevelContext context)
    {
        ObjectSprites.Clear();
        TileSprites.Clear();
        Context = context;

        /*
        For each grid that contains a nonempty object,
        adds the corresponding sprite to that grid
        */
        for (int i = 0; i < context.LevelWidth * context.LevelHeight; i++)
        {
            var x = i % context.LevelWidth;
            var y = i / context.LevelHeight;
            var coord = new Coordinate(x, y);

            var grid = gridPointer.GetGrid(coord);
            var obj = grid.Object;
            var tile = grid.Tile;

            var spriteCoord = CalculateLiteralPos(coord);

            if (obj.Id != ObjectIds.Empty)
            {
                ObjectSprites.Add(spriteCoord, objectSpriteFactory.CreateObjectSprite(obj, spriteCoord, context));
            }
            if (tile.Id != TileIds.Empty)
            {
                TileSprites.Add(spriteCoord, tileSpriteFactory.CreateTileSprite(tile, spriteCoord, context));
            }
        }
    }

    public ISprite GetObjectSprite(Coordinate coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        if (!ObjectSprites.TryGetValue(realCoord, out var sprite))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return sprite;
    }

    public ISprite GetTileSprite(Coordinate coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        if (!TileSprites.TryGetValue(realCoord, out var sprite))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return sprite;
    }

    public void SetObjectSprite(Coordinate currentCoord, Coordinate targetCoord, ISprite sprite)
    {
        var realCurrentCoord = CalculateLiteralPos(currentCoord);
        var realTargetCoord = CalculateLiteralPos(targetCoord);
        /* 
        Must remove and add each time a sprite is set in case 
        the type (static or animated) changed
        */
        if (!ObjectSprites.Remove(realCurrentCoord))
        {
            throw new ArgumentException("Object sprite not found at current coord");
        }
        ObjectSprites.Add(realCurrentCoord, sprite);

        sprite.SetTargetPosition(realTargetCoord);
    }

    public void SyncObjectSprite(Coordinate coord, GridPointer gridPointer)
    {
        var realCoord = CalculateLiteralPos(coord);
        ObjectSprites.Remove(realCoord);
        var obj = gridPointer.GetObject(coord);
        var sprite = objectSpriteFactory.CreateObjectSprite(obj, realCoord, Context);
        ObjectSprites.Add(realCoord, sprite);
    }

    public void RemoveObjectSprite(Coordinate coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        ObjectSprites.Remove(realCoord);
    }

    public void SetTileSprite(Coordinate currentCoord, Coordinate targetCoord, ISprite sprite)
    {
        var realCurrentCoord = CalculateLiteralPos(currentCoord);
        var realTargetCoord = CalculateLiteralPos(targetCoord);

        if (!TileSprites.Remove(realCurrentCoord))
        {
            throw new ArgumentException("Tile sprite not found at current coord");
        }
        TileSprites.Add(realCurrentCoord, sprite);

        sprite.SetTargetPosition(realTargetCoord);
    }

    public void CycleTileSprite(Coordinate coord, GridPointer gridPointer)
    {
        var realCoord = CalculateLiteralPos(coord);
        TileSprites.Remove(realCoord);
        var tile = gridPointer.GetTile(coord);
        var sprite = tileSpriteFactory.CreateTileSprite(tile, realCoord, Context);
        TileSprites.Add(realCoord, sprite);
    }

    public void StartObjectAnimation(ISprite sprite)
    {
        AnimatingObjectSprites.Add(sprite);
    }

    public void StartTileAnimation(ISprite sprite)
    {
        AnimatingTileSprites.Add(sprite);
    }

    public void Update(int dtMs)
    {
        UpdateAnimatingList(dtMs, AnimatingObjectSprites, ObjectSprites);
        UpdateAnimatingList(dtMs, AnimatingTileSprites, TileSprites);

        bool animObjEmpty = AnimatingObjectSprites.Count == 0;
        bool animTileEmpty = AnimatingTileSprites.Count == 0;

        IsIdle = animObjEmpty && animTileEmpty;
    }

    public void Draw(SpriteBatch sb)
    {
        foreach (var coordAndTileSprite in TileSprites)
        {
            var tileSprite = coordAndTileSprite.Value;
            tileSprite.Draw(sb);
        }
        foreach (var coordAndObjSprite in ObjectSprites)
        {
            var objectSprite = coordAndObjSprite.Value;
            objectSprite.Draw(sb);
        }
    }

    private static void UpdateAnimatingList(int dtMs, List<ISprite> currentList, Dictionary<Vector2, ISprite> currentDict)
    {
        foreach (var sprite in currentList)
        {
            currentDict.Remove(sprite.CurrentPosition);
            sprite.Update(dtMs);
            currentDict.Add(sprite.CurrentPosition, sprite);
        }
        var finishedSprites = currentList.FindAll(sprite => sprite.IsFinished);
        var finishedExplosions = finishedSprites.FindAll(sprite => sprite.IdleName == RegionNames.Explosion);
        ResetToIdle(finishedSprites, currentDict);

        currentList.RemoveAll(sprite => sprite.IsFinished);

        foreach (var explosion in finishedExplosions)
        {
            currentDict.Remove(explosion.CurrentPosition);
        }
    }

    private static void ResetToIdle(List<ISprite> finishedSprites, Dictionary<Vector2, ISprite> currentDict)
    {
        foreach (var sprite in finishedSprites)
        {
            var staticSprite = sprite.ConvertToStatic(sprite.IdleName);

            // reset sprite stored at pos to new static sprite
            Vector2 currPos = staticSprite.CurrentPosition;
            currentDict.Remove(currPos);
            currentDict.Add(currPos, staticSprite);
        }

        finishedSprites.Clear();
    }

    private Vector2 CalculateLiteralPos(Coordinate coord)
    {
        var spriteWidth = (int)(Context.GridSpriteWidthPx * Context.GridScale);
        var spriteX = spriteWidth * coord.X + Context.PuzzleOffsetX;
        var spriteHeight = (int)(Context.GridSpriteHeightPx * Context.GridScale);
        var spriteY = spriteHeight * coord.Y + Context.PuzzleOffsetY;

        return new Vector2(spriteX, spriteY);
    }
}