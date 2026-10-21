using System.Collections.Generic;
namespace Scripts.Game;
using System;

public class PickItemEvent(ObjectFactory objectFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var itemCoord = new Coordinate();
        switch (playerDirection)
        {
            case Directions.North:
                itemCoord = playerCoord + Coordinate.North;
            break;
            case Directions.East:
                itemCoord = playerCoord + Coordinate.East;
            break;
            case Directions.South:
                itemCoord = playerCoord + Coordinate.South;
            break;
            case Directions.West:
                itemCoord = playerCoord + Coordinate.West;
            break;
        }
        var itemObj = gridPointer.GetObject(itemCoord);

        Console.WriteLine($"itemObj Is Pickable  {itemObj.IsPickable} ");

        if (itemObj.IsPickable)
        {
            gridPointer.SetObject(itemCoord, objectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(itemCoord);
        }
        context.IsIdle = false;
    }
}