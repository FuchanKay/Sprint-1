namespace Scripts.Game;

using Scripts.GameComponents;

public class PlayerChangeDirectionSubEvent(Directions direction) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);

        var isFacingDirection = playerObj.Direction == direction;
        if (!isFacingDirection)
        {
            playerObj.Direction = direction;

            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            var playerIdleSpriteRegion = AnimationNames.ToPlayerIdleAnimationName(direction);
            playerSprite = playerSprite.ConvertToAnimated(playerIdleSpriteRegion);
            playerSprite.IdleName = playerIdleSpriteRegion;
            spriteGrid.SetObjectSprite(playerCoord, playerSprite);
        }
    }
}