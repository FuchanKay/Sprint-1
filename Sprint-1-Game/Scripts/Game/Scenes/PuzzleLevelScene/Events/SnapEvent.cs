using System.Collections.Generic;
using System.Linq;
namespace Scripts.Game;

public class SnapEvent(ObjectFactory objectFactory, TileFactory tileFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, List<LevelStateHistory> levelStateHistory)
    {
        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory, tileFactory, levelStateHistory));

        var bombCoords = gridPointer.GetObjectPointers(ObjectIds.Bomb).Keys.ToList();
        var timedBombCoords = gridPointer.GetObjectPointers(ObjectIds.TimedBomb).Keys.ToList();

        SubEventStack.Push(new BlueLizardAttackSubEvent(objectFactory, SubEventStack, levelStateHistory));
        SubEventStack.Push(new BombSnapSubEvent(bombCoords, SubEventStack));
        SubEventStack.Push(new TimedBombSnapSubEvent(timedBombCoords, SubEventStack));
        SubEventStack.Push(new BugRotateSubEvent());
        SubEventStack.Push(new WarlockSwapEvent());
        SubEventStack.Push(new SnapAnimationSubEvent());
    }
}