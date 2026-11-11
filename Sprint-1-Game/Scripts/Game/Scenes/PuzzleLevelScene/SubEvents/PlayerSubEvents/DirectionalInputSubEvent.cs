using System.Collections;
using System.Collections.Generic;
using Scripts.GameComponents;
namespace Scripts.Game;

public class DirectionalInputSubEvent(Directions direction) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
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

            if(destinationObject.Id == ObjectIds.Rock) audioManager.PlaySound(SoundNames.StoneScrape);

            var onePastDestinationCoord = playerCoord + Coordinate.ToCoordinate(direction) * 2;

            var objectSprite = spriteGrid.GetObjectSprite(destinationCoord);
            objectSprite.Duration = spriteGrid.PushDuration;

            spriteGrid.SetObjectSprite(destinationCoord, onePastDestinationCoord, objectSprite);
            spriteGrid.StartObjectAnimation(destinationCoord);

            gridPointer.SetObject(destinationCoord, ObjectFactory.CreateEmpty());
            gridPointer.SetObject(onePastDestinationCoord, destinationObject);

            MovePlayerSprite(spriteGrid, gridPointer, playerCoord, direction, true);
            enemiesMove.Execute(spriteGrid, gridPointer, inventoryManager, context, audioManager);

            if(gridPointer.ObjectsJustMoved.ContainsKey(direction) == false) gridPointer.ObjectsJustMoved[direction] = new Dictionary<Coordinate, IObject>();
            gridPointer.ObjectsJustMoved[direction].Add(onePastDestinationCoord, destinationObject);
        }
        else if (destinationCoordIsWithinBounds && CanMove(playerObj, gridPointer.GetGrid(destinationCoord)))
        {
            MovePlayerSprite(spriteGrid, gridPointer, playerCoord, direction, false);
            enemiesMove.Execute(spriteGrid, gridPointer, inventoryManager, context, audioManager);
        }
        else if (gridPointer.GetObject(destinationCoord).Id == ObjectIds.Door)
        {
            var door = gridPointer.GetObject(destinationCoord) as DoorObject;
            if (door.IsOpen)
            {
                context.LevelIsCompleted = true;
            }
        }

        if(gridPointer.ObjectsJustMoved.ContainsKey(direction) == false) gridPointer.ObjectsJustMoved[direction] = new Dictionary<Coordinate, IObject>();
        gridPointer.ObjectsJustMoved[direction].Add(gridPointer.GetPlayerCoord(), playerObj);
    }

    private void MovePlayerSprite(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate playerCoord, Directions direction, bool isPushing)
    {
        var destinationCoord = playerCoord + Coordinate.ToCoordinate(direction);
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

        if (isPushing) playerSprite = playerSprite.ConvertToAnimated(AnimationNames.ToPlayerPushAnimationName(direction));
        else playerSprite = playerSprite.ConvertToAnimated(AnimationNames.ToPlayerMovingAnimationName(direction));

        spriteGrid.SetObjectSprite(playerCoord, destinationCoord, playerSprite);
        spriteGrid.StartObjectAnimation(playerCoord);

        var playerObj = gridPointer.GetObject(playerCoord);
        gridPointer.SetObject(playerCoord, ObjectFactory.CreateEmpty());
        gridPointer.SetObject(destinationCoord, playerObj);
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