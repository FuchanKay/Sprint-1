using System;
using System.Linq;
namespace Scripts.Game;

public class SnapEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public SubEventCallStack SubEventStack { get; } = new();
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

        // playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        // spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        // spriteGrid.StartObjectAnimation(playerSprite);

        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack, objectFactory));

        var warlockSwapEvent = new WarlockSwapEvent();
        warlockSwapEvent.Execute(spriteGrid, gridPointer, context);

        var rotationSubEvent = new BugRotateSubEvent();
        rotationSubEvent.Execute(spriteGrid, gridPointer, context);

        var bombCoords = gridPointer.GetObjectPointers(ObjectIds.Bomb).Keys.ToList();
        var timedBombCoords = gridPointer.GetObjectPointers(ObjectIds.TimedBomb).Keys.ToList();
        SubEventStack.Push(new BombSnapSubEvent(bombCoords, objectFactory, SubEventStack));
        SubEventStack.Push(new TimedBombSnapSubEvent(timedBombCoords, objectFactory, SubEventStack));
        SubEventStack.Push(new RedLizardBlastSubEvent(objectFactory));

        context.IsIdle = false;
    }
}