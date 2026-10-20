using System;
using System.Collections.Generic;

namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        if (gridPointer.GetTile(playerCoord).Id == TileIds.Lava) subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        Console.WriteLine("Evaluated!");
    }
}