using System.Collections.Generic;
using Scripts.GameComponents;
namespace Scripts.Game;

public class CheckNonWalkableTilesSubEvent(Stack<ILevelSubEvent> subEventStack, ObjectFactory objectFactory, TileFactory tileFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var lavaCoords = gridPointer.GetTilePointers(TileIds.Lava).Keys;
        var waterCoords = gridPointer.GetTilePointers(TileIds.Water).Keys;
        var pitCoords = gridPointer.GetTilePointers(TileIds.Pit).Keys;

        foreach (var coord in lavaCoords)
        {
            CheckLava(spriteGrid, gridPointer, objectFactory, subEventStack, coord);
        }
        foreach (var coord in waterCoords)
        {
            CheckWater(spriteGrid, gridPointer, objectFactory, tileFactory, subEventStack, coord);
        }
        foreach (var coord in pitCoords)
        {
            CheckPit(spriteGrid, gridPointer, objectFactory, tileFactory, subEventStack, coord);
        }
    }

    private static void CheckWater(SpriteGrid spriteGrid, GridPointer gridPointer, ObjectFactory objectFactory, TileFactory tileFactory, Stack<ILevelSubEvent> subEventStack, Coordinate waterCoord)
    {
        var obj = gridPointer.GetObject(waterCoord);

        if(obj.Id == ObjectIds.Player)
        {
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        } 
        else if (obj is IEnemy && obj.Id != ObjectIds.Bat)
        {
            subEventStack.Push(new EnemyDamageSubEvent(objectFactory, waterCoord));
        } 
        else if(obj.Id == ObjectIds.Rock)
        {
            var sprite = spriteGrid.GetObjectSprite(waterCoord);
            ITile tile;
            if(sprite.IdleName == RegionNames.Rock)
            {
                tile = tileFactory.CreateWaterBoulderTile();
            } else
            {
                tile = tileFactory.CreateWaterPumpkinTile();
            }
            spriteGrid.SetNewTileSprite(waterCoord, tile);
            gridPointer.SetTile(waterCoord, tile);
            spriteGrid.RemoveObjectSprite(waterCoord);
            gridPointer.SetObject(waterCoord, ObjectFactory.CreateEmpty());
            spriteGrid.PlayEffect(EffectIds.WaterSplash, waterCoord);
        }
        else if(obj.Id != ObjectIds.Empty && obj.Id != ObjectIds.Bat)
        {
            gridPointer.SetObject(waterCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(waterCoord);
            spriteGrid.PlayEffect(EffectIds.WaterSplash, waterCoord);
        }
    }

    private static void CheckPit(SpriteGrid spriteGrid, GridPointer gridPointer, ObjectFactory objectFactory, TileFactory tileFactory, Stack<ILevelSubEvent> subEventStack, Coordinate waterCoord)
    {
        var obj = gridPointer.GetObject(waterCoord);

        if(obj.Id == ObjectIds.Player)
        {
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        } 
        else if (obj is IEnemy && obj.Id != ObjectIds.Bat)
        {
            subEventStack.Push(new EnemyDamageSubEvent(objectFactory, waterCoord));
        } 
        else if(obj.Id == ObjectIds.Rock)
        {
            var sprite = spriteGrid.GetObjectSprite(waterCoord);
            ITile tile;
            if(sprite.IdleName == RegionNames.Rock)
            {
                tile = tileFactory.CreatePitBoulderTile();
            } else
            {
                tile = tileFactory.CreatePitPumpkinTile();
            }
            spriteGrid.SetNewTileSprite(waterCoord, tile);
            gridPointer.SetTile(waterCoord, tile);
            spriteGrid.RemoveObjectSprite(waterCoord);
            gridPointer.SetObject(waterCoord, ObjectFactory.CreateEmpty());
        }
        else if(obj.Id != ObjectIds.Empty && obj.Id != ObjectIds.Bat)
        {
            gridPointer.SetObject(waterCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(waterCoord);
        }
    }

    private static void CheckLava(SpriteGrid spriteGrid, GridPointer gridPointer, ObjectFactory objectFactory, Stack<ILevelSubEvent> subEventStack, Coordinate lavaCoord)
    {
        var obj = gridPointer.GetObject(lavaCoord);
        if (obj.Id == ObjectIds.Player)
        {
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        }
        else if (obj is IEnemy)
        {
            subEventStack.Push(new EnemyDamageSubEvent(objectFactory, lavaCoord));
        }
        else if (obj.Id != ObjectIds.Empty)
        {
            gridPointer.SetObject(lavaCoord, ObjectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(lavaCoord);
            spriteGrid.PlayEffect(EffectIds.LavaSplash, lavaCoord);
        }
    }
}