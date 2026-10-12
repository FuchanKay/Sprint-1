namespace Scripts.Game;

public class PlayerChangeDirectionSubEvent(Directions direction) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        playerObj.Direction = direction;

        var isFacingDirection = playerObj.Direction == direction;
        if (!isFacingDirection)
        {
            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            var playerIdleSpriteRegion = RegionNames.ToPlayerIdleRegionName(direction);
            playerSprite = playerSprite.ConvertToStatic(playerIdleSpriteRegion);
            playerSprite.IdleName = playerIdleSpriteRegion;
            spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
            context.IsIdle = false;
        }
    }
}