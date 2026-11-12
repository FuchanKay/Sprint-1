namespace Scripts.Game;

using Scripts.GameComponents;

public class UseItemSubEvent(int inventoryId) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = playerCoord + Coordinate.ToCoordinate(playerDirection);

        var targetObj = gridPointer.GetObject(targetCoord);
        var targetTileSprite = spriteGrid.GetTileSprite(targetCoord);

        var itemObj = inventoryManager.GetItem(inventoryId);
        bool itemEquiped = itemObj.Id != ObjectIds.Empty;
        bool isFrontCleared = targetObj.Id == ObjectIds.Empty;
        if (itemEquiped && isFrontCleared)
        {
            var canPlantPumpkinSeed = targetTileSprite.IdleName == RegionNames.DirtTile && itemObj.Id == ObjectIds.PumpkinSeed;
            var canPlantVineSeed = targetTileSprite.IdleName == RegionNames.DirtTile && itemObj.Id == ObjectIds.VineSeed;
            var isNotSeed = itemObj.Id != ObjectIds.PumpkinSeed && itemObj.Id != ObjectIds.VineSeed;

            if(canPlantPumpkinSeed)
            {
                var pumpkinSeedTile = TileFactory.CreatePumpkinSeedTile();
                gridPointer.SetTile(targetCoord, pumpkinSeedTile);
                spriteGrid.SetNewTileSprite(targetCoord, pumpkinSeedTile);
                inventoryManager.EmptySlot(inventoryId);
                audioManager.PlaySound(SoundNames.Pickup);
                
            }
            else if (canPlantVineSeed)
            {
                var vineSeedTile = TileFactory.CreateVineSeedTile(playerDirection);
                gridPointer.SetTile(targetCoord, vineSeedTile);
                spriteGrid.SetNewTileSprite(targetCoord, vineSeedTile);
                inventoryManager.EmptySlot(inventoryId);
                audioManager.PlaySound(SoundNames.Pickup);
                
            }
            else if (isNotSeed)
            {
                gridPointer.SetObject(targetCoord, itemObj);
                spriteGrid.SetNewObjectSprite(targetCoord, itemObj);
                inventoryManager.EmptySlot(inventoryId);
                audioManager.PlaySound(SoundNames.Pickup);
            }
        }
    }
}