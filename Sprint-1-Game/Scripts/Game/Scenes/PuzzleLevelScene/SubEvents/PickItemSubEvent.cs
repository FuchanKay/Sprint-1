namespace Scripts.Game;
public class PickItemSubEvent(ObjectFactory objectFactory, InventoryManager inventoryManager) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {

        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = playerCoord + Coordinate.ToCoordinate(playerDirection);

        var itemObj = gridPointer.GetObject(targetCoord);

        if (itemObj.IsPickable)
        {
            inventoryManager.AddItem(itemObj);
            gridPointer.SetObject(targetCoord, objectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(targetCoord);
        }

    }
}