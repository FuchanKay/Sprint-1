using System;
using Microsoft.Xna.Framework;
using Scripts.Game;

public class CycleTileLeftEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var objDemoCoord = new Coordinate(5, 5);
        var currentTile = gridPointer.GetTile(objDemoCoord);

        switch (currentTile.Id)
        {
            case TileIds.Brick:
                gridPointer.SetTile(objDemoCoord, new LavaTile());
                break;
            case TileIds.Grass:
                gridPointer.SetTile(objDemoCoord, new BrickTile());
                break;
            case TileIds.Water:
                gridPointer.SetTile(objDemoCoord, new GrassTile());
                break;
            case TileIds.Lava:
                gridPointer.SetTile(objDemoCoord, new WaterTile());
                break;
            default:
                gridPointer.SetTile(objDemoCoord, new BrickTile());
                break;
        }
        spriteGrid.CycleTileSprite(objDemoCoord, gridPointer);
        context.ShouldUpdate = true;
        Console.WriteLine($"Cycled tile to {gridPointer.GetTile(objDemoCoord).Id}");
    }
}