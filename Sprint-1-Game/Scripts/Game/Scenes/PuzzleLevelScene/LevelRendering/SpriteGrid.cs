using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SpriteGrid(ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory)
{
    private readonly ObjectSpriteManager ObjectSpriteManager = new();
    private readonly TileSpriteManager TileSpriteManager = new();
    private readonly EffectSpriteManager EffectSpriteManager = new();
    public readonly int PushDuration = 400;

    public void Init(GridPointer gridPointer, LevelContext context)
    {
        ObjectSpriteManager.Init(gridPointer, context, objectSpriteFactory);
        TileSpriteManager.Init(gridPointer, context, tileSpriteFactory);
        EffectSpriteManager.Init(context, objectSpriteFactory);
    }

    // OBJECT SPRITE METHODS
    public ISprite GetObjectSprite(Coordinate coord)
    {
        return ObjectSpriteManager.GetObjectSprite(coord);
    }

    public void SetObjectSprite(Coordinate coord, ISprite sprite)
    {
        ObjectSpriteManager.SetObjectSprite(coord, sprite);
    }

    public void SetObjectSprite(Coordinate startCoord, Coordinate targetCoord, ISprite sprite)
    {
        ObjectSpriteManager.SetObjectSprite(startCoord, targetCoord, sprite);
    }

    public void SetNewObjectSprite(Coordinate coord, IObject obj)
    {
        ObjectSpriteManager.SetNewObjectSprite(coord, obj);
    }

    public void RemoveObjectSprite(Coordinate coord)
    {
        ObjectSpriteManager.RemoveObjectSprite(coord);
    }

    public void StartObjectAnimation(Coordinate coord)
    {
        ObjectSpriteManager.StartObjectAnimation(coord);
    }

    // TILE SPRITE METHODS
    public ISprite GetTileSprite(Coordinate coord)
    {
        return TileSpriteManager.GetTileSprite(coord);
    }

    public void SetTileSprite(Coordinate coord, ISprite sprite)
    {
        TileSpriteManager.SetTileSprite(coord, sprite);
    }

    public void SetNewTileSprite(Coordinate coord, ITile tile)
    {
        TileSpriteManager.SetNewTileSprite(coord, tile);
    }

    public void StartTileAnimation(Coordinate coord)
    {
        TileSpriteManager.StartTileAnimation(coord);
    }

    public void AddIterableTileSprite(ITile tile, Coordinate startCoord, Coordinate currentCoord, float rotation)
    {
        TileSpriteManager.AddIterableTileSprite(tile, startCoord, currentCoord, rotation);
    }

    public void StartIterableTile(Coordinate startCoord)
    {
        TileSpriteManager.StartIterableTile(startCoord);
    }

    // EFFECT METHODS
    public void PlayEffect(EffectIds effectId, Coordinate coord)
    {
        EffectSpriteManager.PlayEffect(effectId, coord);
    }

    public Dictionary<Vector2, ISprite> GetEffectSprites()
    {
        return EffectSpriteManager.GetEffectSprites();
    }

    public void RemoveEffect(Coordinate coord)
    {
        EffectSpriteManager.RemoveEffect(coord);
    }

    public void AddIterableEffectSprite(EffectIds effectId, Coordinate startCoord, Coordinate currentCoord, float rotation)
    {
        EffectSpriteManager.AddIterableEffectSprite(effectId, startCoord, currentCoord, rotation);
    }

    public void StartIterableEffect(Coordinate startCoord)
    {
        EffectSpriteManager.StartIterableEffect(startCoord);
    }
    
    public void Update(int dtMs)
    {
        ObjectSpriteManager.Update(dtMs);
        TileSpriteManager.Update(dtMs);
        EffectSpriteManager.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        TileSpriteManager.Draw(sb);
        ObjectSpriteManager.Draw(sb);
        EffectSpriteManager.Draw(sb);
    }

    public bool IsIdle()
    {
        bool animObjEmpty = ObjectSpriteManager.AnimatingObjectSprites.Count == 0;
        bool animTileEmpty = TileSpriteManager.AnimatingTileSprites.Count == 0;
        bool animEffectsEmpty = EffectSpriteManager.AnimatingEffectSprites.Count == 0;

        bool iteratingEffectsEmpty = EffectSpriteManager.IteratingEffectSprites.Count == 0;
        bool iteratingTilesEmpty = TileSpriteManager.IteratingTileSprites.Count == 0;

        return animObjEmpty && animTileEmpty && animEffectsEmpty && iteratingEffectsEmpty && iteratingTilesEmpty;
    }
}