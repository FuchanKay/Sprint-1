using System.Collections.Generic;
namespace Scripts.Game;

public class UseItemSubEvent(Inventory inventory, ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = playerCoord + Coordinate.ToCoordinate(playerDirection);

        var targetObj = gridPointer.GetObject(targetCoord);
        bool itemEquiped = PlayerItemsComponent.playerItems[(int) inventory].Id != ObjectIds.Empty;
        bool isFrontCleared = targetObj.Id == ObjectIds.Empty;
        
        if (!(itemEquiped && isFrontCleared)) return;
        switch (PlayerItemsComponent.playerItems[(int) inventory].Id)
        {
            case ObjectIds.Bomb:
            gridPointer.SetObject(targetCoord, objectFactory.CreateBomb());
            break;
        }
        PlayerItemsComponent.playerItemSprites[(int) inventory].CurrentPosition = spriteGrid.CalculateLiteralPos(targetCoord);
        spriteGrid.SetObjectSprite(targetCoord,targetCoord,PlayerItemsComponent.playerItemSprites[(int) inventory]);
        PlayerItemsComponent.playerItems[(int) inventory] = objectFactory.CreateEmpty();
    }
}