using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class ObjectSpriteManager
{
    public readonly Dictionary<Vector2, ISprite> ObjectSprites = [];
    public readonly List<ISprite> AnimatingObjectSprites = [];
    private LevelContext Context;
    private ObjectSpriteFactory ObjectSpriteFactory;

    public void Init(GridPointer gridPointer, LevelContext context, ObjectSpriteFactory objectSpriteFactory)
    {
        Context = context;
        ObjectSpriteFactory = objectSpriteFactory;

        ObjectSprites.Clear();
        AnimatingObjectSprites.Clear();

        for (int y = 0; y < context.LevelHeight; y++)
        {
            for (int x = 0; x < context.LevelWidth; x++)
            {
                var coord = new Coordinate(x, y);

                var grid = gridPointer.GetGrid(coord);
                var obj = grid.Object;

                var spriteCoord = CalculateLiteralPos(coord);

                if (obj.Id != ObjectIds.Empty)
                {
                    ObjectSprites.Add(spriteCoord, objectSpriteFactory.CreateObjectSprite(obj, spriteCoord, context));
                }
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

    public void SetNewObjectSprite(Coordinate coord, IObject obj)
    {
        var realCoord = CalculateLiteralPos(coord);
        ObjectSprites.Remove(realCoord);
        var newSprite = ObjectSpriteFactory.CreateObjectSprite(obj, realCoord, Context);
        ObjectSprites[realCoord] = newSprite;
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

    public void Update(int dtMs)
    {
        UpdateAnimatingObjects(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        foreach (var coordAndObjSprite in ObjectSprites)
        {
            var objectSprite = coordAndObjSprite.Value;
            objectSprite.Draw(sb);
        }
    }

    private void UpdateAnimatingObjects(int dtMs)
    {
        foreach (var sprite in AnimatingObjectSprites)
        {
            ObjectSprites.Remove(sprite.CurrentPosition);
            sprite.Update(dtMs);
            ObjectSprites.Add(sprite.CurrentPosition, sprite);
        }
        var finishedSprites = AnimatingObjectSprites.FindAll(sprite => sprite.IsFinished);
        ResetToIdle(finishedSprites);
        AnimatingObjectSprites.RemoveAll(sprite => sprite.IsFinished);
    }

    private void ResetToIdle(List<ISprite> finishedSprites)
    {
        foreach (var sprite in finishedSprites)
        {
            var staticSprite = sprite.ConvertToStatic(sprite.IdleName);

            // reset sprite stored at pos to new static sprite
            Vector2 currPos = staticSprite.CurrentPosition;
            ObjectSprites.Remove(currPos);
            ObjectSprites.Add(currPos, staticSprite);
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