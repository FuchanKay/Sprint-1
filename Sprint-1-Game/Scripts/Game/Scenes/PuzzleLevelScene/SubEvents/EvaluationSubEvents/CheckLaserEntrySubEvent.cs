using Scripts.Game;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Scripts.GameComponents;

public class CheckLaserEntrySubEvent(ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var effectSprites = spriteGrid.GetEffectSprites();
        foreach(var sprite in effectSprites.Values)
        {
            if(sprite.IdleName == RegionNames.LaserBeam)
            {
                DestroyObject(spriteGrid, gridPointer, context, subEventStack, objectFactory, sprite.CurrentPosition);
            }
        }
    }

    private static void DestroyObject(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory, Vector2 position)
    {
        var coord = GetGridCoord(context, position);
        var obj = gridPointer.GetObject(coord);
        if(obj.IsDestructible)
        {
            gridPointer.SetObject(coord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(coord);
        }
        if(obj.Id == ObjectIds.Player)
        {
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        }
    }

    private static Coordinate GetGridCoord(LevelContext context, Vector2 position)
    {
        var spriteWidth = (int)(context.GridSpriteWidthPx * context.GridScale);
        var X = (int)(position.X - context.PuzzleOffsetX) / spriteWidth;
        var spriteHeight = (int)(context.GridSpriteHeightPx * context.GridScale);
        var Y = (int)(position.Y - context.PuzzleOffsetY) / spriteHeight;

        return new Coordinate(X, Y);
    }
}