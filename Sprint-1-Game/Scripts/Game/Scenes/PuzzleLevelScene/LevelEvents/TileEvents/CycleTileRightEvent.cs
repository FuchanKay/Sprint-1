using System;
using Scripts.Game;

public class CycleTileRightEvent(TileFactory tileFactory) : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var objDemoCoord = new Coordinate(5, 5);
        var currentTile = gridPointer.GetTile(objDemoCoord);

        switch (currentTile.Id)
        {
            case TileIds.Brick:
                gridPointer.SetTile(objDemoCoord, tileFactory.CreateGrassTile());
                break;
            case TileIds.Grass:
                gridPointer.SetTile(objDemoCoord, tileFactory.CreateWaterTile());
                break;
            case TileIds.Water:
                gridPointer.SetTile(objDemoCoord, tileFactory.CreateLavaTile());
                break;
            case TileIds.Lava:
                gridPointer.SetTile(objDemoCoord, tileFactory.CreateBrickTile());
                break;
            default:
                gridPointer.SetTile(objDemoCoord, tileFactory.CreateBrickTile());
                break;
        }
        spriteGrid.CycleTileSprite(objDemoCoord, gridPointer);
        context.ShouldUpdate = true;
        Console.WriteLine($"Cycled tile to {gridPointer.GetTile(objDemoCoord).Id}");
    }
}