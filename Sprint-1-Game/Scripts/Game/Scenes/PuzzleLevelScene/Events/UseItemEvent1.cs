using System;
using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEvent1(ObjectFactory objectFactory, ObjectSpriteFactory objectSpriteFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = new Coordinate();
        switch (playerDirection)
        {
            case Directions.North:
                targetCoord = playerCoord + Coordinate.North;
            break;
            case Directions.East:
                targetCoord = playerCoord + Coordinate.East;
            break;
            case Directions.South:
                targetCoord = playerCoord + Coordinate.South;
            break;
            case Directions.West:
                targetCoord = playerCoord + Coordinate.West;
            break;
        }
        var targetObj = gridPointer.GetObject(targetCoord);
        bool itemEquiped = true;
        bool isUsable = targetObj.Id == ObjectIds.Empty;
        if (!(itemEquiped && isUsable)) return;
        gridPointer.SetObject(targetCoord, objectFactory.CreateBomb());
        targetObj = gridPointer.GetObject(targetCoord);

        Console.WriteLine($"use item 1");

    }
}