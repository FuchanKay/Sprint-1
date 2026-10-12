using System;
using System.Linq;
using Scripts.Game;

public class WarlockSwapEvent() : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var warlockPointer = gridPointer.GetObjectPointers(ObjectIds.Warlock);
        var targetWarLockCoord = warlockPointer.Keys.First();
        
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var targetWarLockObj = gridPointer.GetObject(targetWarLockCoord);

        gridPointer.SetObject(playerCoord, targetWarLockObj);
        gridPointer.SetObject(targetWarLockCoord, playerObj);

        spriteGrid.CycleObjectSprite(playerCoord, gridPointer);
        spriteGrid.CycleObjectSprite(targetWarLockCoord, gridPointer);

        


        // var warlockPointer = gridPointer.GetObjectPointers(ObjectIds.Warlock);
        // var warlockCoord = warlockPointer.Keys.First();
        
        // var playerCoord = gridPointer.GetPlayerCoord();
        // var playerObj = gridPointer.GetObject(playerCoord);
        // var targetWarLockObj = gridPointer.GetObject(warlockCoord);

        // var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
        // var warlockSprite = spriteGrid.GetObjectSprite(warlockCoord);

        // gridPointer.SetObject(playerCoord, targetWarLockObj);
        // gridPointer.SetObject(warlockCoord, playerObj);

        // spriteGrid.SetObjectSprite(playerCoord, warlockCoord, playerSprite);
        // spriteGrid.SetObjectSprite(warlockCoord, playerCoord, warlockSprite);

        context.IsIdle = true;
        context.ShouldUpdate = true;

    }
}