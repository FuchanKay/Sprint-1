using System;
using System.Linq;
using Scripts.Game;

public class SnapEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public SubEventCallStack SubEventStack { get; } = new();
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

        playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        spriteGrid.StartObjectAnimation(playerSprite);

        var bombCoords = gridPointer.GetObjectPointers(ObjectIds.Bomb).Keys.ToList();
        var timedBombCoords = gridPointer.GetObjectPointers(ObjectIds.TimedBomb).Keys.ToList();

        SubEventStack.Push(new EvaluateLevelSubEvent(SubEventStack));
        if(bombCoords.Count > 0) SubEventStack.Push(new BombSnapSubEvent(bombCoords, objectFactory));
        if(timedBombCoords.Count > 0) SubEventStack.Push(new TimedBombSnapSubEvent(timedBombCoords, objectFactory));

        context.IsIdle = false;
    }

}