using System.Collections;
using System.Collections.Generic;

namespace Scripts.Game;

public class DirectionalInputSubEvent(Directions direction, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        ILevelSubEvent enemiesMove = new EnemiesMoveSubEvent();
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);

        var destinationCoord = playerCoord + Coordinate.ToCoordinate(direction);
        var destinationCoordIsWithinBounds = CoordIsWithinBounds(destinationCoord, context.LevelWidth, context.LevelHeight);

        var destinationPlus1Coord = playerCoord + Coordinate.ToCoordinate(direction) * 2;
        var destinationPlus1CoordIsWithinBounds = CoordIsWithinBounds(destinationPlus1Coord, context.LevelWidth, context.LevelHeight);
        
        if (destinationCoordIsWithinBounds && destinationPlus1CoordIsWithinBounds && CanPush(gridPointer.GetGrid(destinationCoord), gridPointer.GetObject(destinationPlus1Coord)))
        {
            var destinationGrid = gridPointer.GetGrid(destinationCoord);
            var destinationObject = destinationGrid.Object;
            
            var onePastDestinationCoord = playerCoord + Coordinate.ToCoordinate(direction) * 2;

            var objectSprite = spriteGrid.GetObjectSprite(destinationCoord);
            objectSprite.Duration = spriteGrid.PushDuration;

            spriteGrid.SetObjectSprite(destinationCoord, onePastDestinationCoord, objectSprite);
            spriteGrid.StartObjectAnimation(destinationCoord);

            gridPointer.SetObject(destinationCoord, ObjectFactory.CreateEmpty());
            gridPointer.SetObject(onePastDestinationCoord, destinationObject);

            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.ToPlayerPushAnimationName(direction));
            spriteGrid.SetObjectSprite(playerCoord, destinationCoord, playerSprite);
            spriteGrid.StartObjectAnimation(playerCoord);

            gridPointer.SetObject(playerCoord, ObjectFactory.CreateEmpty());
            gridPointer.SetObject(destinationCoord, playerObj);

            enemiesMove.Execute(spriteGrid, gridPointer, inventoryManager, context);
        }
        else if (destinationCoordIsWithinBounds && CanMove(playerObj, gridPointer.GetGrid(destinationCoord)))
        {
            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.ToPlayerMovingAnimationName(direction));
            spriteGrid.SetObjectSprite(playerCoord, destinationCoord, playerSprite);
            spriteGrid.StartObjectAnimation(playerCoord);

            gridPointer.SetObject(playerCoord, ObjectFactory.CreateEmpty());
            gridPointer.SetObject(destinationCoord, playerObj);
            enemiesMove.Execute(spriteGrid, gridPointer, inventoryManager, context);
        }
        // placeholder logic to collect a key by walking into it
        else if (gridPointer.GetObject(destinationCoord).Id == ObjectIds.Key)
        {
            subEventStack.Push(new OpenExitSubEvent());
            gridPointer.SetObject(destinationCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(destinationCoord);

            var playerSprite = spriteGrid.GetObjectSprite(playerCoord);
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.ToPlayerMovingAnimationName(direction));
            spriteGrid.SetObjectSprite(playerCoord, destinationCoord, playerSprite);
            spriteGrid.StartObjectAnimation(playerCoord);

            gridPointer.SetObject(playerCoord, ObjectFactory.CreateEmpty());
            gridPointer.SetObject(destinationCoord, playerObj);
        }
        else if (gridPointer.GetObject(destinationCoord).Id == ObjectIds.Door)
        {
            var door = gridPointer.GetObject(destinationCoord) as DoorObject;
            if (door.IsOpen)
            {
                context.LevelIsCompleted = true;
            }
        }
    }

    private bool CanMove(IObject playerObject, Grid destinationGrid)
    {
        var isDestinationEmpty = destinationGrid.Object.Id == ObjectIds.Empty;
        var isFacingDirection = playerObject.Direction == direction;

        return isFacingDirection && isDestinationEmpty;
    }

    private static bool CanPush(Grid destinationGrid, IObject destinationPlus1Object)
    {
        var destinationObject = destinationGrid.Object;
        var destinationTile = destinationGrid.Tile;

        var destinationPlus1ObjectIsEmpty = destinationPlus1Object.Id == ObjectIds.Empty;

        return destinationObject.IsPushable && destinationTile.IsWalkable && destinationPlus1ObjectIsEmpty;
    }

    private static bool CoordIsWithinBounds(Coordinate coord, int width, int height)
    {
        return coord.X >= 0 && coord.X < width
            && coord.Y >= 0 && coord.Y < height;
    }
}