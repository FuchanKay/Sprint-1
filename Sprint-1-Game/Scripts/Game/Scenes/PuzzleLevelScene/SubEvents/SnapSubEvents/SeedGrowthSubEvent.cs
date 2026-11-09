using Microsoft.Xna.Framework;

namespace Scripts.Game;
using Scripts.GameComponents;

public class SeedGrowthSubEvent : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context, IAudioManager audioManager)
    {
        var vineSeedCoords = gridPointer.GetTilePointers(TileIds.VineSeed).Keys;
        var pumpkinSeedCoords = gridPointer.GetTilePointers(TileIds.PumpkinSeed).Keys;

        foreach (var coord in vineSeedCoords)
        {
            GrowVine(spriteGrid, gridPointer, coord);
        }
        foreach (var coord in pumpkinSeedCoords)
        {
            GrowPumpkin(spriteGrid, gridPointer, coord);
        }


    }

    private static void GrowPumpkin(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate seedCoord)
    {
        var dirtTile = TileFactory.CreateDirtTile();
        gridPointer.SetTile(seedCoord, dirtTile);
        spriteGrid.SetNewTileSprite(seedCoord, dirtTile);

        var pumpkinObj = ObjectFactory.CreatePumpkin();
        gridPointer.SetObject(seedCoord, pumpkinObj);
        spriteGrid.SetNewObjectSprite(seedCoord, pumpkinObj);

        var pumpkinSprite = spriteGrid.GetObjectSprite(seedCoord);
        var growthAnimation = pumpkinSprite.ConvertToAnimated(AnimationNames.PumpkinGrowth);
        spriteGrid.SetObjectSprite(seedCoord, growthAnimation);
        spriteGrid.StartObjectAnimation(seedCoord);
    }

    private static void GrowVine(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate seedCoord)
    {
        var direction = gridPointer.GetTile(seedCoord).Direction;
        var vineRotation = Utilities.ToRadians(direction);

        var startVineTile = TileFactory.CreateVineStartTile();
        startVineTile.Direction = direction;
        gridPointer.SetTile(seedCoord, startVineTile);
        spriteGrid.AddIterableTileSprite(startVineTile, seedCoord, seedCoord, vineRotation);

        // until tile is walkable
        var dirCoord = Coordinate.ToCoordinate(direction);
        var nextCoord = seedCoord + dirCoord;
        var tile = gridPointer.GetTile(nextCoord);
        var vineTile = TileFactory.CreateVineTile();
        vineTile.Direction = direction;

        while (!tile.IsWalkable)
        {
            gridPointer.SetTile(nextCoord, vineTile);
            spriteGrid.AddIterableTileSprite(vineTile, seedCoord, nextCoord, vineRotation);
            nextCoord += dirCoord;
            tile = gridPointer.GetTile(nextCoord);
        }

        var oppositeDirection = Utilities.Rotate180(direction);
        var oppositeRotation = Utilities.ToRadians(oppositeDirection);

        var endVineTile = TileFactory.CreateVineStartTile();
        endVineTile.Direction = oppositeDirection;
        gridPointer.SetTile(nextCoord, endVineTile);
        spriteGrid.AddIterableTileSprite(endVineTile, seedCoord, nextCoord, oppositeRotation);
        spriteGrid.StartIterableTile(seedCoord);
    }
}