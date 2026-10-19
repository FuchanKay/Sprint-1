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
        
        
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
        var warlockSprite = spriteGrid.GetObjectSprite(targetWarLockCoord);

        gridPointer.SetObject(targetWarLockCoord, playerObj);
        gridPointer.SetObject(playerCoord, targetWarLockObj);

        spriteGrid.CycleObjectSprite(targetWarLockCoord, gridPointer);
        spriteGrid.CycleObjectSprite(playerCoord, gridPointer);


        // gridPointer.SetObject(playerCoord, targetWarLockObj);
        // gridPointer.SetObject(targetWarLockCoord, playerObj);

        // spriteGrid.SetObjectSprite(playerCoord, targetWarLockCoord, playerSprite);
        // spriteGrid.SetObjectSprite(targetWarLockCoord, playerCoord, warlockSprite);

        context.IsIdle = true;
        context.ShouldUpdate = true;

    }
}