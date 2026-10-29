using System.Linq;

namespace Scripts.Game;
using Scripts.GameComponents;

public class OpenExitSubEvent() : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var doorCoords = gridPointer.GetObjectPointers(ObjectIds.Door).Keys.ToList();
        foreach (var doorCoord in doorCoords)
        {
            DoorObject door = gridPointer.GetObject(doorCoord) as DoorObject;
            if(!door.IsOpen) 
            {
                OpenDoor(spriteGrid, doorCoord);
                door.IsOpen = true;
            }
        }
    }

    private static void OpenDoor(SpriteGrid spriteGrid, Coordinate doorCoord)
    {
        var doorSprite = spriteGrid.GetObjectSprite(doorCoord);
        doorSprite = doorSprite.ConvertToAnimated(AnimationNames.DoorOpening);
        spriteGrid.SetObjectSprite(doorCoord, doorSprite);
        spriteGrid.StartObjectAnimation(doorCoord);
        doorSprite.IdleName = RegionNames.OpenDoor;
    }
}