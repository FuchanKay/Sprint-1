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
        subEventStack.Push(new SkeletonsAttackSubEvent(skeletonCoords, objectFactory, subEventStack));
        Console.WriteLine("Evaluated!");
        subEventStack.Push(new RedLizardBlastSubEvent(objectFactory));
        subEventStack.Push(new RedLizardBlastSubEvent(objectFactory));
    }
}