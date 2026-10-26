using System.Collections.Generic;
using System.Linq;

namespace Scripts.Game;

public class EvaluateLevelSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory, List<GridPointer> undoHistory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        subEventStack.Push(new CheckLavaSubEvent(subEventStack, objectFactory));
        var skeletonCoords = gridPointer.GetObjectPointers(ObjectIds.Skeleton).Keys.ToList();
        subEventStack.Push(new SkeletonsAttackSubEvent(skeletonCoords, objectFactory, subEventStack));
        subEventStack.Push(new CheckRemoveRedLaserSubEvent(undoHistory));
        subEventStack.Push(new CheckLaserEntrySubEvent(objectFactory, subEventStack));
        subEventStack.Push(new RedLizardBlastSubEvent(objectFactory, undoHistory, subEventStack));
    }
}