namespace Scripts.Game;

public class PlayerChangeDirectionSubEvent(Directions direction) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);

        var isFacingDirection = playerObj.Direction == direction;
        if (!isFacingDirection)
        {
            playerObj.Direction = direction;

            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            var playerIdleSpriteRegion = RegionNames.ToIdleRegionName(direction);
            playerSprite = playerSprite.ConvertToStatic(playerIdleSpriteRegion);
            playerSprite.IdleName = playerIdleSpriteRegion;
            spriteGrid.SetObjectSprite(playerCoord, playerSprite);
        }
    }
}