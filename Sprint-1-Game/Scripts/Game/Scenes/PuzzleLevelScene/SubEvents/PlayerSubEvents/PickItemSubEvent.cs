namespace Scripts.Game;

using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;
using Scripts.GameComponents;

public class PickItemSubEvent(Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {

        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord);
        var playerDirection = playerObj.Direction;
        var targetCoord = playerCoord + Coordinate.ToCoordinate(playerDirection);

        var itemGrid = gridPointer.GetGrid(targetCoord);
        var itemObj = itemGrid.Object;
        var itemTile = itemGrid.Tile;
        if (itemObj.IsPickable)
        {
            if (inventoryManager.AddItem(itemObj))
            {
                gridPointer.SetObject(targetCoord, ObjectFactory.CreateEmpty());
                spriteGrid.RemoveObjectSprite(targetCoord);
                audioManager.PlaySound(SoundNames.Pickup);
            }
            return;
        }
        else if (itemTile.Id == TileIds.VineSeed)
        {
            itemObj = ObjectFactory.CreateVineSeed(itemTile.Direction);
            if (inventoryManager.AddItem(itemObj))
            {
                var dirtTile = TileFactory.CreateDirtTile();
                gridPointer.SetTile(targetCoord, dirtTile);
                spriteGrid.SetNewTileSprite(targetCoord, dirtTile);
            }


        }
        else if (itemTile.Id == TileIds.PumpkinSeed)
        {
            itemObj = ObjectFactory.CreatePumpkinSeed();
            if (inventoryManager.AddItem(itemObj))
            {
                var dirtTile = TileFactory.CreateDirtTile();
                gridPointer.SetTile(targetCoord, dirtTile);
                spriteGrid.SetNewTileSprite(targetCoord, dirtTile);
            }

        }
        else if (itemObj.Id == ObjectIds.Key)
        {
            gridPointer.SetObject(targetCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(targetCoord);
            audioManager.PlaySound(SoundNames.Pickup);
            subEventStack.Push(new OpenExitSubEvent());
        }
    }
}