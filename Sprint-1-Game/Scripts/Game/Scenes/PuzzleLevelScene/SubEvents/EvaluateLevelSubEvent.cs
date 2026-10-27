using System;
using System.Collections.Generic;
using System.Linq;

namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory, List<LevelStateHistory> levelStateHistory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        subEventStack.Push(new CheckLavaSubEvent(subEventStack, objectFactory));
        var skeletonCoords = gridPointer.GetObjectPointers(ObjectIds.Skeleton).Keys.ToList();
        subEventStack.Push(new SkeletonsAttackSubEvent(skeletonCoords, objectFactory, subEventStack));
        subEventStack.Push(new CheckRemoveRedLaserSubEvent(levelStateHistory));
        subEventStack.Push(new CheckLaserEntrySubEvent(objectFactory, subEventStack));
        subEventStack.Push(new RedLizardBlastSubEvent(objectFactory, levelStateHistory, subEventStack));


        subEventStack.Push(new EnemiesAttackSubEvent(objectFactory, subEventStack));
        Console.WriteLine($"There are {gridPointer.EnemyCollection.Count} enemies on screen");
    }
}