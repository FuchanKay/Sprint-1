using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        if (gridPointer.GetTile(playerCoord).Id == TileIds.Lava) subEventStack.Push(new PlayerDeathSubEvent(objectFactory));

        var skeletonCoords = gridPointer.GetObjectPointers(ObjectIds.Skeleton).Keys.ToList();
        foreach (var skeletonCoord in skeletonCoords)
        {
            subEventStack.Push(new SkeletonAttackSubEvent(skeletonCoord, objectFactory, subEventStack));
        }
        Console.WriteLine("Evaluated!");
    }
}