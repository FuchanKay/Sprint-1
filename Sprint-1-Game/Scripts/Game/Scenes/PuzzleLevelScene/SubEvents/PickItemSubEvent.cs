namespace Scripts.Game;

using System.Collections.Generic;
using Scripts.GameComponents;
public class PickItemSubEvent(ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
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
        } else if (itemObj.Id == ObjectIds.Key)
        {
            gridPointer.SetObject(targetCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(targetCoord);
            subEventStack.Push(new OpenExitSubEvent());
        }

    }
}