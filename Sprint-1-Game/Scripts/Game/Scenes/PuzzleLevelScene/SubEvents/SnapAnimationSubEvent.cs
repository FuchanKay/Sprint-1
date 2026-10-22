using System.Collections.Generic;
namespace Scripts.Game;

public class SnapAnimationSubEvent(Stack<ILevelSubEvent> subeventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
        playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        spriteGrid.StartObjectAnimation(playerSprite);
    }
}