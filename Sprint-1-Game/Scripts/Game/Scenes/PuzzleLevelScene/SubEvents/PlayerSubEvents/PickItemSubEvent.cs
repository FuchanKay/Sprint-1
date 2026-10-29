namespace Scripts.Game;
public class PickItemSubEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {

        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = playerCoord + Coordinate.ToCoordinate(playerDirection);

        var itemObj = gridPointer.GetObject(targetCoord);
        if (itemObj.IsPickable)
        {
            inventoryManager.AddItem(itemObj);
            gridPointer.SetObject(targetCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(targetCoord);
            return;
        }

    }
}