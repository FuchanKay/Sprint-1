using System;
using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemEvent1(ObjectFactory objectFactory) : ILevelEvent
{
    public Stack<ILevelSubEvent> SubEventStack { get; } = [];
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = PlayerItemsComponent.aquireTarget(playerCoord,playerDirection);

        var targetObj = gridPointer.GetObject(targetCoord);
        bool itemEquiped = PlayerItemsComponent.playerItems[0].Id != ObjectIds.Empty;
        bool isFrontCleared = targetObj.Id == ObjectIds.Empty;
        
        if (!(itemEquiped && isFrontCleared)) return;
        switch (PlayerItemsComponent.playerItems[0].Id)
        {
            case ObjectIds.Bomb:
            gridPointer.SetObject(targetCoord, objectFactory.CreateBomb());
            break;
        }
        PlayerItemsComponent.playerItemSprites[0].CurrentPosition = spriteGrid.CalculateLiteralPos(targetCoord);
        spriteGrid.SetObjectSprite(targetCoord,targetCoord,PlayerItemsComponent.playerItemSprites[0]);
        PlayerItemsComponent.playerItems[0] = objectFactory.CreateEmpty();
        Console.WriteLine($"use item 1");

    }
}