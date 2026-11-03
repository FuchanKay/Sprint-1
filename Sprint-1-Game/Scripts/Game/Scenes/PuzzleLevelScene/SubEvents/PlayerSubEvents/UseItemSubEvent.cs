namespace Scripts.Game;

public class UseItemSubEvent(int inventoryId) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = playerCoord + Coordinate.ToCoordinate(playerDirection);

        var targetObj = gridPointer.GetObject(targetCoord);
        var targetTileSprite = spriteGrid.GetTileSprite(targetCoord);

        var itemObj = inventoryManager.GetItem(inventoryId);
        var seedObj = itemObj as SeedObject;
        bool itemEquiped = itemObj.Id != ObjectIds.Empty;
        bool isFrontCleared = targetObj.Id == ObjectIds.Empty;
        if (itemEquiped && isFrontCleared)
        {
            var canPlantPumpkinSeed = targetTileSprite.IdleName == RegionNames.DirtTile && itemObj.Id == ObjectIds.Seed && seedObj.RegionName == RegionNames.PumpkinSeed;
            var canPlantVineSeed = targetTileSprite.IdleName == RegionNames.DirtTile && itemObj.Id == ObjectIds.Seed && seedObj.RegionName == RegionNames.VineSeed;
            if(canPlantPumpkinSeed)
            {
                var pumpkinSeedTile = TileFactory.CreatePumpkinSeedTile();
                gridPointer.SetTile(targetCoord, pumpkinSeedTile);
                spriteGrid.SetNewTileSprite(targetCoord, pumpkinSeedTile);
                
            }
            else if (canPlantVineSeed)
            {
                var vineSeedTile = TileFactory.CreateVineSeedTile(playerDirection);
                gridPointer.SetTile(targetCoord, vineSeedTile);
                spriteGrid.SetNewTileSprite(targetCoord, vineSeedTile);
                
            }
            else if (itemObj.Id != ObjectIds.Seed)
            {
                gridPointer.SetObject(targetCoord, itemObj);
                spriteGrid.SetNewObjectSprite(targetCoord, itemObj);
            }
            inventoryManager.EmptySlot(inventoryId);
        }
    }
}