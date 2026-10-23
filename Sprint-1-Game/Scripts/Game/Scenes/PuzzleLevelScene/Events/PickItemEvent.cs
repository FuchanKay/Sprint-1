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
        var targetCoord = PlayerItemsComponent.aquireTarget(playerCoord,playerDirection);

        var itemObj = gridPointer.GetObject(targetCoord);

        if (!itemObj.IsPickable) return;
        var itemSprite = spriteGrid.GetObjectSprite(targetCoord);

        for (int i = 0; i < PlayerItemsComponent.playerItems.Length; i++)
        {
            if(PlayerItemsComponent.playerItems[i].Id == ObjectIds.Empty)
            {
                PlayerItemsComponent.playerItems[i] = itemObj;
                PlayerItemsComponent.playerItemSprites[i] = itemSprite;
                break;
            }
        }
        gridPointer.SetObject(targetCoord, objectFactory.CreateEmpty());
        spriteGrid.RemoveObjectSprite(targetCoord);
    
    }
}