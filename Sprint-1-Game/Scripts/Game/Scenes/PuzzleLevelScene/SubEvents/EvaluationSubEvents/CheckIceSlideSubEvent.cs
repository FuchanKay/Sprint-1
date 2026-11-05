using System.Collections.Generic;
using Scripts.GameComponents;
namespace Scripts.Game;

public class CheckIceSlideSubEvent(Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var iteratingList = new Dictionary<Directions, Dictionary<Coordinate, IObject>>(gridPointer.ObjectsJustMoved);
        foreach (var pair in iteratingList)
        {
            var direction = pair.Key;
            var objsMovedinDirection = new Dictionary<Coordinate, IObject>(pair.Value);
            foreach (var objs in objsMovedinDirection)
            {
                var objCoord = objs.Key;
                var standingTile = gridPointer.GetTile(objCoord);
                var obj = gridPointer.GetObject(objCoord);
                var currentCoord = objCoord;
                var destinationCoord = objCoord + Coordinate.ToCoordinate(direction);
                var isDestinationEmpty = gridPointer.GetObject(destinationCoord).Id == ObjectIds.Empty;
                if (standingTile.Id == TileIds.Ice && isDestinationEmpty)
                {
                    SlideObject(spriteGrid, gridPointer, currentCoord, destinationCoord);
                    gridPointer.ObjectsJustMoved[direction].Remove(currentCoord);
                    gridPointer.ObjectsJustMoved[direction].Add(destinationCoord, obj);
                    subEventStack.Push(new CheckIceSlideSubEvent(subEventStack));
                }
            }
        }
    }

    private void SlideObject(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate currentCoord, Coordinate destinationCoord)
    {
        var objectSprite = spriteGrid.GetObjectSprite(currentCoord);
        objectSprite = objectSprite.ConvertToStatic(objectSprite.IdleName);
        objectSprite.Duration = spriteGrid.PushDuration;    
        spriteGrid.SetObjectSprite(currentCoord, destinationCoord, objectSprite);
        spriteGrid.StartObjectAnimation(currentCoord);

        var obj = gridPointer.GetObject(currentCoord);
        gridPointer.SetObject(currentCoord, ObjectFactory.CreateEmpty());
        gridPointer.SetObject(destinationCoord, obj);
    }
}