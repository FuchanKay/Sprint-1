using System.Linq;
using Scripts.Game;

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

        var warlockSwapEvent = new WarlockSwapEvent();
        warlockSwapEvent.Execute(spriteGrid, gridPointer, context);

        var rotationSubEvent = new BugRotateSubEvent();
        rotationSubEvent.Execute(spriteGrid, gridPointer, context);

        var bombCoords = gridPointer.GetObjectPointers(ObjectIds.Bomb).Keys.ToList();
        var timedBombCoords = gridPointer.GetObjectPointers(ObjectIds.TimedBomb).Keys.ToList();

        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack));
        if(bombCoords.Count > 0) SubEventStack.Push(new BombSnapSubEvent(bombCoords, objectFactory, SubEventStack));
        if(timedBombCoords.Count > 0) SubEventStack.Push(new TimedBombSnapSubEvent(timedBombCoords, objectFactory, SubEventStack));

        context.IsIdle = false;
    }
}