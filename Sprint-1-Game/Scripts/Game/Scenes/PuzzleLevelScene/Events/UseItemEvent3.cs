using System;
using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEvent3(ObjectFactory objectFactory) : ILevelEvent
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
        bool itemEquiped = PlayerItemsComponent.playerItems[2].Id != ObjectIds.Empty;
        bool isFrontCleared = targetObj.Id == ObjectIds.Empty;
        if (!(itemEquiped && isFrontCleared)) return;
        switch (PlayerItemsComponent.playerItems[2].Id)
        {
            case ObjectIds.Bomb:
            gridPointer.SetObject(targetCoord, objectFactory.CreateBomb());
            break;
        }
        
        spriteGrid.SetObjectSprite(targetCoord,targetCoord,PlayerItemsComponent.playerItemSprites[0]);
        PlayerItemsComponent.playerItems[2] = objectFactory.CreateEmpty();
        Console.WriteLine($"use item 1");


    }
}