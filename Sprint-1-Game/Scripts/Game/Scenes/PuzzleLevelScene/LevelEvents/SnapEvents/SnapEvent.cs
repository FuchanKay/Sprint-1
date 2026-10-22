using System;
using System.Linq;
namespace Scripts.Game;

public class SnapEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public SubEventCallStack SubEventStack { get; } = new();
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory));

        var bombCoords = gridPointer.GetObjectPointers(ObjectIds.Bomb).Keys.ToList();
        var timedBombCoords = gridPointer.GetObjectPointers(ObjectIds.TimedBomb).Keys.ToList();
        SubEventStack.Push(new BombSnapSubEvent(bombCoords, objectFactory, SubEventStack));
        SubEventStack.Push(new TimedBombSnapSubEvent(timedBombCoords, objectFactory, SubEventStack));
        SubEventStack.Push(new RedLizardBlastSubEvent(objectFactory));
        SubEventStack.Push(new BugRotateSubEvent(objectFactory));
        SubEventStack.Push(new WarlockSwapEvent());

        context.IsIdle = false;
    }
}