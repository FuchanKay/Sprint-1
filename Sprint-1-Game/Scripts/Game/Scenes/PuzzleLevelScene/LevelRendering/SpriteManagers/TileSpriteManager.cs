using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class TileSpriteManager
{
    public readonly Dictionary<Vector2, ISprite> TileSprites = [];
    public readonly List<ISprite> AnimatingTileSprites = [];
    public readonly Dictionary<Vector2, List<ISprite>> IterableTileSprites = [];
    public readonly List<List<ISprite>> IteratingTileSprites = [];
    private int IterableTimer;
    private LevelContext Context;
    private TileSpriteFactory TileSpriteFactory;

    public void Init(GridPointer gridPointer, LevelContext context, TileSpriteFactory tileSpriteFactory)
    {
        Context = context;
        TileSpriteFactory = tileSpriteFactory;
        
        TileSprites.Clear();
        AnimatingTileSprites.Clear();
        IterableTileSprites.Clear();
        IteratingTileSprites.Clear();

        for (int y = 0; y < context.LevelHeight; y++)
        {
            for (int x = 0; x < context.LevelWidth; x++)
            {
                var coord = new Coordinate(x, y);

                var grid = gridPointer.GetGrid(coord);
                var tile = grid.Tile;

                var spriteCoord = CalculateLiteralPos(coord);
                
                if (tile.Id != TileIds.Empty)
                {
                    TileSprites.Add(spriteCoord, tileSpriteFactory.CreateTileSprite(tile, spriteCoord, context));
                }
            }
        }
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

    public void SetTileSprite(Coordinate coord, ISprite sprite)
    {
        var realCoord = CalculateLiteralPos(coord);

        sprite.CurrentPosition = realCoord;
        sprite.SetTargetPosition(realCoord);

        TileSprites.Remove(realCoord);
        TileSprites.Add(realCoord, sprite);
    }

    public void SetNewTileSprite(Coordinate coord, ITile tile)
    {
        var realCoord = CalculateLiteralPos(coord);

        TileSprites.Remove(realCoord);
        var newSprite = TileSpriteFactory.CreateTileSprite(tile, realCoord, Context);
        TileSprites[realCoord] = newSprite;
    }

    public void StartTileAnimation(Coordinate coord)
    {
        var sprite = GetTileSprite(coord);

        AnimatingTileSprites.Add(sprite);
    }

    public void AddIterableTileSprite(ITile tile, Coordinate startCoord, Coordinate currentCoord, float rotation)
    {
        var realStartCoord = CalculateLiteralPos(startCoord);
        var realCurrentCoord = CalculateLiteralPos(currentCoord);

        var sprite = TileSpriteFactory.CreateTileSprite(tile, realCurrentCoord, Context);
        sprite.Rotation = rotation;

        if(!IterableTileSprites.TryGetValue(realStartCoord, out var spriteList))
        {
            spriteList = [];
            IterableTileSprites.Add(realStartCoord, spriteList);
        }

        spriteList.Add(sprite);
    }

    public void StartIterableTile(Coordinate startCoord)
    {
        var realStartCoord = CalculateLiteralPos(startCoord);

        var spriteList = IterableTileSprites[realStartCoord];
        IterableTileSprites.Remove(realStartCoord);

        IteratingTileSprites.Add(spriteList);
    }

    public void Update(int dtMs)
    {
        IterableTimer++;
        UpdateAnimatingTiles(dtMs);
        if (IterableTimer % 4 == 0)
        {
            UpdateIterableTiles();
        }
    }

    public void Draw(SpriteBatch sb)
    {
        foreach (var coordAndTileSprite in TileSprites)
        {
            var tileSprite = coordAndTileSprite.Value;
            tileSprite.Draw(sb);
        }
    }

    private void UpdateAnimatingTiles(int dtMs)
    {
        foreach (var sprite in AnimatingTileSprites)
        {
            TileSprites.Remove(sprite.CurrentPosition);
            sprite.Update(dtMs);
            TileSprites.Add(sprite.CurrentPosition, sprite);
        }
        var finishedSprites = AnimatingTileSprites.FindAll(sprite => sprite.IsFinished);
        ResetToIdle(finishedSprites);
        AnimatingTileSprites.RemoveAll(sprite => sprite.IsFinished);
    }

    private void ResetToIdle(List<ISprite> finishedSprites)
    {
        foreach (var sprite in finishedSprites)
        {
            var staticSprite = sprite.ConvertToStatic(sprite.IdleName);

            // reset sprite stored at pos to new static sprite
            Vector2 currPos = staticSprite.CurrentPosition;
            TileSprites.Remove(currPos);
            TileSprites.Add(currPos, staticSprite);
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

    private void UpdateIterableTiles()
    {
        foreach (var spriteList in IteratingTileSprites)
        {
            var sprite = spriteList[0];
            spriteList.Remove(sprite);

            TileSprites.Remove(sprite.CurrentPosition);
            TileSprites[sprite.CurrentPosition] = sprite;
        }
        var finishedIterables = IteratingTileSprites.RemoveAll(spriteList => spriteList.Count == 0);
    }
}