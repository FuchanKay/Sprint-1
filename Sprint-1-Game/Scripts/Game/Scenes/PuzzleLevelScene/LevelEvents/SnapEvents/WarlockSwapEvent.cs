using System;
using Scripts.Game;

public class WarlockSwapeEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var Warlock = new Coordinate(7, 5);
        var currentEnemy = gridPointer.GetObject(Warlock);
        var currentPlayer = gridPointer.GetPlayerCoord();
        

        spriteGrid.CycleObjectSprite(Warlock, gridPointer);
        var newObject = gridPointer.GetObject(Warlock);
        context.ShouldUpdate = true;
        Console.WriteLine($"Cycled object to {gridPointer.GetObject(Warlock).Id}");

    }
}