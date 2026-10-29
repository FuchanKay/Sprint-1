using Microsoft.Xna.Framework;

namespace Scripts.Game;

public class SeedGrowthSubEvent : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var vineSeedCoords = gridPointer.GetTilePointers(TileIds.VineSeed).Keys;
        foreach (var coord in vineSeedCoords)
        {
            GrowVine(spriteGrid, gridPointer, coord);
        }
    }

    private static void GrowVine(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate seedCoord)
    {
        var direction = gridPointer.GetTile(seedCoord).Direction;
        var vineRotation = MathHelper.ToRadians((int)direction * 90);

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

        while(!tile.IsWalkable)
        {
            gridPointer.SetTile(nextCoord, vineTile);
            spriteGrid.AddIterableTileSprite(vineTile, seedCoord, nextCoord, vineRotation);
            nextCoord += dirCoord;
            tile = gridPointer.GetTile(nextCoord);
        }

        gridPointer.SetTile(nextCoord, vineTile);
        spriteGrid.AddIterableTileSprite(vineTile, seedCoord, nextCoord, vineRotation);
        spriteGrid.StartIterableTile(seedCoord);
    }
} 