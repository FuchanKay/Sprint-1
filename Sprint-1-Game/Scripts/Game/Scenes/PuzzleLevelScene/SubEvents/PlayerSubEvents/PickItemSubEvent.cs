namespace Scripts.Game;

public class PickItemSubEvent() : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {

        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = playerCoord + Coordinate.ToCoordinate(playerDirection);

        var itemGrid = gridPointer.GetGrid(targetCoord);
        var itemObj = itemGrid.Object;
        var itemTile = itemGrid.Tile;
        if (itemObj.IsPickable)
        {
            inventoryManager.AddItem(itemObj);
            gridPointer.SetObject(targetCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(targetCoord);
            return;
        } else if (itemTile.Id == TileIds.VineSeed)
        {
            itemObj = ObjectFactory.CreateVineSeed(itemTile.Direction);
            inventoryManager.AddItem(itemObj);
            var dirtTile = TileFactory.CreateDirtTile();
            gridPointer.SetTile(targetCoord, dirtTile);
            spriteGrid.SetNewTileSprite(targetCoord, dirtTile);
            
        } else if (itemTile.Id == TileIds.PumpkinSeed)
        {
            itemObj = ObjectFactory.CreatePumpkinSeed();
            inventoryManager.AddItem(itemObj);
            var dirtTile = TileFactory.CreateDirtTile();
            gridPointer.SetTile(targetCoord, dirtTile);
            spriteGrid.SetNewTileSprite(targetCoord, dirtTile);
        }

    }
}