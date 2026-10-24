namespace Scripts.Game;
using System;

public class UseItemSubEvent(Inventory inventory, InventoryManager inventoryManager, ObjectSpriteFactory objectSpriteFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = playerCoord + Coordinate.ToCoordinate(playerDirection);

        var targetObj = gridPointer.GetObject(targetCoord);
        var itemObj = inventoryManager.GetItem((int) inventory);
        bool itemEquiped = itemObj.Id != ObjectIds.Empty;
        bool isFrontCleared = targetObj.Id == ObjectIds.Empty;
        if (itemEquiped && isFrontCleared)
        {
            gridPointer.SetObject(targetCoord, itemObj);
            var itemSprite = objectSpriteFactory.CreateObjectSprite(itemObj, spriteGrid.CalculateLiteralPos(targetCoord), context);
            spriteGrid.SetObjectSprite(targetCoord,targetCoord,itemSprite);
            inventoryManager.EmptySlot((int) inventory);
        }
    }
}