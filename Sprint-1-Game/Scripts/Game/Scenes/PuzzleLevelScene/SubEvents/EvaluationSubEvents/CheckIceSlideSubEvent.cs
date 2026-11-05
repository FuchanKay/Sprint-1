using System.Collections.Generic;
using Scripts.GameComponents;
namespace Scripts.Game;

public class CheckIceSlideSubEvent(Coordinate objCoord, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var standingTile = gridPointer.GetTile(objCoord);
        var obj = gridPointer.GetObject(objCoord);
        if (standingTile.Id == TileIds.Ice && obj.Id != ObjectIds.Empty && obj.Id != ObjectIds.Wall && obj.Id != ObjectIds.Door)
        {
            var currentCoord = objCoord;
            var destinationCoord = objCoord + Coordinate.ToCoordinate(obj.Direction);
            var isDestinationEmpty = gridPointer.GetObject(destinationCoord).Id == ObjectIds.Empty;
            if(isDestinationEmpty)
            {
                var objectSprite = spriteGrid.GetObjectSprite(currentCoord);
                
                objectSprite = objectSprite.ConvertToStatic(objectSprite.IdleName);
                objectSprite.Duration = spriteGrid.PushDuration;    
                spriteGrid.SetObjectSprite(currentCoord, destinationCoord, objectSprite);
                spriteGrid.StartObjectAnimation(currentCoord);

                gridPointer.SetObject(currentCoord, ObjectFactory.CreateEmpty());
                gridPointer.SetObject(destinationCoord, obj);
                subEventStack.Push(new CheckIceSlideSubEvent(destinationCoord, subEventStack));
            }
        }
    }
}