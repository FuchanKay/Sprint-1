using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SpriteGrid(ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    // SINGLE SPRITE HANDLING
    private readonly Dictionary<Vector2, ISprite> ObjectSprites = [];
    private readonly Dictionary<Vector2, ISprite> TileSprites = [];
    private readonly Dictionary<Vector2, ISprite> EffectSprites = [];
    private readonly List<ISprite> AnimatingObjectSprites = [];
    private readonly List<ISprite> AnimatingTileSprites = [];
    private readonly List<ISprite> AnimatingEffectSprites = [];

    // ITERABLES
    private readonly Dictionary<Vector2, List<ISprite>> IterableEffectSprites = [];
    private readonly List<List<ISprite>> IteratingEffectSprites = [];
    private readonly Dictionary<Vector2, List<ISprite>> IterableTileSprites = [];
    private readonly List<List<ISprite>> IteratingTileSprites = [];


    public bool IsIdle { get; private set; }
    private LevelContext Context;
    private int iterableTimer;

    public readonly int PushDuration = 400;

    public void Init(GridPointer gridPointer, LevelContext context)
    {
        ObjectSprites.Clear();
        TileSprites.Clear();
        EffectSprites.Clear();
        AnimatingObjectSprites.Clear();
        AnimatingTileSprites.Clear();
        AnimatingEffectSprites.Clear();

        IsIdle = true;

        Context = context;

        /*
        For each grid that contains a nonempty object,
        adds the corresponding sprite to that grid
        */
        for (int i = 0; i < context.LevelWidth * context.LevelHeight; i++)
        {
            var x = i % context.LevelWidth;
            var y = i / context.LevelWidth;
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

    // OBJECT SPRITE METHODS
    public ISprite GetObjectSprite(Coordinate coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        if (!ObjectSprites.TryGetValue(realCoord, out var sprite))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return sprite;
    }

    public void SetObjectSprite(Coordinate coord, ISprite sprite)
    {
        var realCoord = CalculateLiteralPos(coord);

        sprite.CurrentPosition = realCoord;
        sprite.SetTargetPosition(realCoord);

        ObjectSprites.Remove(realCoord);
        ObjectSprites.Add(realCoord, sprite);
    }

    public void SetObjectSprite(Coordinate startCoord, Coordinate targetCoord, ISprite sprite)
    {
        var realStartCoord = CalculateLiteralPos(startCoord);
        var realTargetCoord = CalculateLiteralPos(targetCoord);

        sprite.CurrentPosition = realStartCoord;
        sprite.SetTargetPosition(realTargetCoord);
        
        ObjectSprites.Remove(realStartCoord);
        ObjectSprites.Add(realStartCoord, sprite);
        
    }

    public void RemoveObjectSprite(Coordinate coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        ObjectSprites.Remove(realCoord);
    }

    public void StartObjectAnimation(Coordinate coord)
    {
        var sprite = GetObjectSprite(coord);
        AnimatingObjectSprites.Add(sprite);
    }

    // TILE SPRITE METHODS
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

    public void StartTileAnimation(Coordinate coord)
    {
        var sprite = GetTileSprite(coord);

        AnimatingTileSprites.Add(sprite);
    }

    public void AddIterableTileSprite(TileIds tileId, string tileSkin, Coordinate startCoord, Coordinate currentCoord, float rotation)
    {
        var realStartCoord = CalculateLiteralPos(startCoord);
        var realCurrentCoord = CalculateLiteralPos(currentCoord);

        var sprite = tileSpriteFactory.CreateTileSpriteFromID(tileId, tileSkin, realCurrentCoord, Context);
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

    // EFFECT METHODS
    public void PlayEffect(EffectIds effectId, Coordinate coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        ISprite sprite = objectSpriteFactory.CreateEffectSprite(effectId, realCoord, Context);
        
        EffectSprites[realCoord] = sprite;
        AnimatingEffectSprites.Add(sprite);
    }

    public Dictionary<Vector2, ISprite> GetEffectSprites()
    {
        return EffectSprites;
    }

    public void RemoveEffect(Coordinate coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        EffectSprites.Remove(realCoord);
    }

    public void AddIterableEffectSprite(EffectIds effectId, Coordinate startCoord, Coordinate currentCoord, float rotation)
    {
        var realStartCoord = CalculateLiteralPos(startCoord);
        var realCurrentCoord = CalculateLiteralPos(currentCoord);

        var sprite = objectSpriteFactory.CreateEffectSprite(effectId, realCurrentCoord, Context);
        sprite.Rotation = rotation;

        if(!IterableEffectSprites.TryGetValue(realStartCoord, out var spriteList))
        {
            spriteList = [];
            IterableEffectSprites.Add(realStartCoord, spriteList);
        }

        spriteList.Add(sprite);
    }

    public void StartIterableEffect(Coordinate startCoord)
    {
        var realStartCoord = CalculateLiteralPos(startCoord);
        var spriteList = IterableEffectSprites[realStartCoord];
        IterableEffectSprites.Remove(realStartCoord);

        IteratingEffectSprites.Add(spriteList);
    }

    // UPDATE AND DRAW METHODS
    public void Update(int dtMs)
    {
        iterableTimer++;
        UpdateAnimatingList(dtMs, AnimatingObjectSprites, ObjectSprites);
        UpdateAnimatingList(dtMs, AnimatingTileSprites, TileSprites);
        UpdateAnimatingEffectsList(dtMs, AnimatingEffectSprites, EffectSprites);
        UpdateIterableEffects();
        if(iterableTimer % 3 == 0)
        {
            UpdateIterableTiles();
        }

        bool animObjEmpty = AnimatingObjectSprites.Count == 0;
        bool animTileEmpty = AnimatingTileSprites.Count == 0;
        bool animEffectsEmpty = AnimatingEffectSprites.Count == 0;

        IsIdle = animObjEmpty && animTileEmpty && animEffectsEmpty;
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
        foreach (var coordAndEffectSprite in EffectSprites)
        {
            var effectSprite = coordAndEffectSprite.Value;
            effectSprite.Draw(sb);
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
        ResetToIdle(finishedSprites, currentDict);
        currentList.RemoveAll(sprite => sprite.IsFinished);
    }

    private static void UpdateAnimatingEffectsList(int dtMs, List<ISprite> animatingEffects, Dictionary<Vector2, ISprite> effectSprites)
    {
        foreach (var sprite in animatingEffects)
        {
            effectSprites.Remove(sprite.CurrentPosition);
            sprite.Update(dtMs);
            effectSprites.Add(sprite.CurrentPosition, sprite);
        }
        var finishedSprites = animatingEffects.FindAll(sprite => sprite.IsFinished);

        foreach (var sprite in finishedSprites)
        {
            effectSprites.Remove(sprite.CurrentPosition);
        }

        animatingEffects.RemoveAll(sprite => sprite.IsFinished);
    }

    private void UpdateIterableEffects()
    {
        foreach (var spriteList in IteratingEffectSprites)
        {
            var sprite = spriteList[0];
            spriteList.Remove(sprite);
            if(sprite.IdleName == RegionNames.Explosion)
            {
                AnimatingEffectSprites.Add(sprite);
            } else
            {
                EffectSprites.Remove(sprite.CurrentPosition);
                EffectSprites.Add(sprite.CurrentPosition, sprite);
            }
        }
        var finishedIterables = IteratingEffectSprites.RemoveAll(spriteList => spriteList.Count == 0);
    }

    private void UpdateIterableTiles()
    {
        foreach (var spriteList in IteratingTileSprites)
        {
            var sprite = spriteList[0];
            spriteList.Remove(sprite);

            // replace current tile with new tile!
            TileSprites[sprite.CurrentPosition] = sprite;
        }
        // once all tiles in list have been added, remove list from iterating
        var finishedIterables = IteratingTileSprites.RemoveAll(spriteList => spriteList.Count == 0);
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