using Scripts.Game;

public class SnapEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord.ToVector2());

        playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerSnap);
        spriteGrid.SetObjectSprite(playerCoord.ToVector2(), playerCoord.ToVector2(), playerSprite);
        spriteGrid.StartObjectAnimation(playerSprite);

        // new BombSnapEvent().Execute(spriteGrid, gridPointer, context);
        // new TimedBombSnapEvent().Execute(spriteGrid, gridPointer, context);

        context.IsIdle = false;
    }
}