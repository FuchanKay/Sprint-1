using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class EffectSpriteManager()
{
    public readonly Dictionary<Vector2, ISprite> EffectSprites = [];
    public readonly List<ISprite> AnimatingEffectSprites = [];
    public readonly Dictionary<Vector2, List<ISprite>> IterableEffectSprites = [];
    public readonly List<List<ISprite>> IteratingEffectSprites = [];
    private LevelContext Context;
    private ObjectSpriteFactory ObjectSpriteFactory;

    public void Init(LevelContext context, ObjectSpriteFactory objectSpriteFactory)
    {
        Context = context;
        ObjectSpriteFactory = objectSpriteFactory;

        EffectSprites.Clear();
        AnimatingEffectSprites.Clear();
        IterableEffectSprites.Clear();
        IteratingEffectSprites.Clear();
    }

    public void PlayEffect(EffectIds effectId, Coordinate coord)
    {
        var realCoord = CalculateLiteralPos(coord);
        ISprite sprite = ObjectSpriteFactory.CreateEffectSprite(effectId, realCoord, Context);
        
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

        var sprite = ObjectSpriteFactory.CreateEffectSprite(effectId, realCurrentCoord, Context);
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

    public void Update(int dtMs)
    {
        UpdateAnimatingEffectsList(dtMs);
        UpdateIterableEffects();
    }

    public void Draw(SpriteBatch sb)
    {
        foreach (var coordAndEffectSprite in EffectSprites)
        {
            var effectSprite = coordAndEffectSprite.Value;
            effectSprite.Draw(sb);
        }
    }

    private Vector2 CalculateLiteralPos(Coordinate coord)
    {
        var spriteWidth = (int)(Context.GridSpriteWidthPx * Context.GridScale);
        var spriteX = spriteWidth * coord.X + Context.PuzzleOffsetX;
        var spriteHeight = (int)(Context.GridSpriteHeightPx * Context.GridScale);
        var spriteY = spriteHeight * coord.Y + Context.PuzzleOffsetY;

        return new Vector2(spriteX, spriteY);
    }

    private void UpdateAnimatingEffectsList(int dtMs)
    {
        foreach (var sprite in AnimatingEffectSprites)
        {
            EffectSprites.Remove(sprite.CurrentPosition);
            sprite.Update(dtMs);
            EffectSprites.Add(sprite.CurrentPosition, sprite);
        }
        var finishedSprites = AnimatingEffectSprites.FindAll(sprite => sprite.IsFinished);

        foreach (var sprite in finishedSprites)
        {
            EffectSprites.Remove(sprite.CurrentPosition);
        }

        AnimatingEffectSprites.RemoveAll(sprite => sprite.IsFinished);
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
}