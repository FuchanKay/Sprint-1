using Scripts.Game;

public class SnapEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

        playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        spriteGrid.StartObjectAnimation(playerSprite);

        // Only executes snap behavior for bomb objects
        new BombSnapEvent().Execute(spriteGrid, gridPointer, context);
        new TimedBombSnapEvent().Execute(spriteGrid, gridPointer, context);

        context.IsIdle = false;
    }
}