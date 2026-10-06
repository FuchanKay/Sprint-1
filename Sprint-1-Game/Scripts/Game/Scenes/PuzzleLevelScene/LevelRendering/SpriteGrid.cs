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
    public LevelContext Context;

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
            var coord = new Vector2(x, y);

            var grid = gridPointer.GetGrid(coord);
            var obj = grid.Object;
            var tile = grid.Tile;

            var spriteCoord = CalculateLiteralPos(coord);

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
                ObjectSprites.Add(spriteCoord, objectSpriteFactory.CreateObjectSprite(obj.Id, spriteCoord, context));
            }
            if (tile.Id != TileIds.Empty)
            {
                TileSprites.Add(spriteCoord, tileSpriteFactory.CreateTileSprite(tile.Id, spriteCoord, context));
            }
        }
    }

    public ISprite GetObjectSprite(Vector2 coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        if (!ObjectSprites.TryGetValue(realCoord, out var sprite))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return sprite;
    }

    public ISprite GetTileSprite(Vector2 coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        if (!TileSprites.TryGetValue(realCoord, out var sprite))
        {
            throw new ArgumentException("Grid could not be found");
        }
        return sprite;
    }

    public void SetObjectSprite(Vector2 currentCoord, Vector2 targetCoord, ISprite sprite)
    {
        var realCurrentCoord = CalculateLiteralPos(currentCoord);
        var realTargetCoord = CalculateLiteralPos(targetCoord);
        /* 
        Must remove and add each time a sprite is set in case 
        the type (static or animated) changed
        */
        ObjectSprites.Remove(realCurrentCoord);
        ObjectSprites.Add(realCurrentCoord, sprite);

        if(currentCoord != targetCoord)
        {
            sprite.SetTargetPosition(realTargetCoord);
        }
    }

    public void CycleObjectSprite(Vector2 coord, GridPointer gridPointer)
    {
        var realCoord = CalculateLiteralPos(coord);
        ObjectSprites.Remove(realCoord);
        var sprite = objectSpriteFactory.CreateObjectSprite(gridPointer.GetObject(coord).Id, realCoord, Context);
        ObjectSprites.Add(realCoord, sprite);
    }

    public void SetTileSprite(Vector2 currentCoord, Vector2 targetCoord, ISprite sprite)
    {
        var realCurrentCoord = CalculateLiteralPos(currentCoord);
        var realTargetCoord = CalculateLiteralPos(targetCoord);
        ObjectSprites.Remove(realCurrentCoord);
        ObjectSprites.Add(realCurrentCoord, sprite);

        if(currentCoord != targetCoord)
        {
            sprite.SetTargetPosition(realTargetCoord);
        }
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
        bool animObjEmpty = UpdateAnimatingList(dtMs, AnimatingObjectSprites, ObjectSprites);
        bool animTileEmpty = UpdateAnimatingList(dtMs, AnimatingTileSprites, TileSprites);

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

    private bool UpdateAnimatingList(int dtMs, List<ISprite> currentList, Dictionary<Vector2, ISprite> currentDict)
    {
        foreach(var sprite in currentList)
        {
            Vector2 currPos = sprite.CurrentPosition;
            currentDict.Remove(currPos);

            sprite.Update(dtMs);
            currentDict.Add(sprite.CurrentPosition, sprite);
        }
        var finishedSprites = currentList.FindAll(sprite => sprite.IsFinished);
        ResetToIdle(finishedSprites, currentDict);

        currentList.RemoveAll(sprite => sprite.IsFinished);

        return currentList.Count == 0;
    }

    private static void ResetToIdle(List<ISprite> finishedSprites, Dictionary<Vector2, ISprite> currentDict)
    {
        foreach(var sprite in finishedSprites)
        {
            var staticSprite = sprite.ConvertToStatic(sprite.IdleName);

            // reset sprite stored at pos to new static sprite
            Vector2 currPos = staticSprite.CurrentPosition;
            currentDict.Remove(currPos);
            currentDict.Add(currPos, staticSprite);
        }

        finishedSprites.Clear();
    }

    private Vector2 CalculateLiteralPos(Vector2 coord)
    {
        var spriteWidth = (int)(Context.GridWidthPx * Context.GridScale);
        var spriteX = spriteWidth * coord.X + Context.PuzzleOffsetX;
        var spriteHeight = (int)(Context.GridHeightPx * Context.GridScale);
        var spriteY = spriteHeight * coord.Y + Context.PuzzleOffsetY;

        return new Vector2(spriteX, spriteY);
    }
}