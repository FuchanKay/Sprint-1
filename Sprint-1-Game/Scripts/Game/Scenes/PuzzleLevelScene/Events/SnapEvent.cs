using System.Collections.Generic;
using System.Linq;
namespace Scripts.Game;

public class SnapEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, List<GridPointer> undoHistory)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
        playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        spriteGrid.StartObjectAnimation(playerCoord);

        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory, undoHistory));

        var bombCoords = gridPointer.GetObjectPointers(ObjectIds.Bomb).Keys.ToList();
        var timedBombCoords = gridPointer.GetObjectPointers(ObjectIds.TimedBomb).Keys.ToList();
        SubEventStack.Push(new BombSnapSubEvent(bombCoords, objectFactory, SubEventStack));
        SubEventStack.Push(new TimedBombSnapSubEvent(timedBombCoords, objectFactory, SubEventStack));
        SubEventStack.Push(new RedLizardBlastSubEvent(objectFactory, undoHistory));
        SubEventStack.Push(new BugRotateSubEvent(objectFactory));
        SubEventStack.Push(new WarlockSwapEvent());

        context.IsIdle = false;
    }
}