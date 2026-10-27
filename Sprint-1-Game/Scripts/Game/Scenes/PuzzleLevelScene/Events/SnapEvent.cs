using System.Collections.Generic;
using System.Linq;
namespace Scripts.Game;

public class SnapEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, List<GridPointer> undoHistory)
    {
        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory, undoHistory));

        var bombCoords = gridPointer.GetObjectPointers(ObjectIds.Bomb).Keys.ToList();
        var timedBombCoords = gridPointer.GetObjectPointers(ObjectIds.TimedBomb).Keys.ToList();

        SubEventStack.Push(new BlueLizardAttackSubEvent(objectFactory, SubEventStack));
        SubEventStack.Push(new BombSnapSubEvent(bombCoords, objectFactory, SubEventStack));
        SubEventStack.Push(new TimedBombSnapSubEvent(timedBombCoords, objectFactory, SubEventStack));
        SubEventStack.Push(new RedLizardBlastSubEvent(objectFactory, undoHistory, SubEventStack));
        SubEventStack.Push(new BugRotateSubEvent());
        SubEventStack.Push(new WarlockSwapEvent());
        SubEventStack.Push(new SnapAnimationSubEvent());
    }
}