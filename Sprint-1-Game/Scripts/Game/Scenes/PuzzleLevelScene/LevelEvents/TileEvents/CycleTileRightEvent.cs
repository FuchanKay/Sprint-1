using System;
using Microsoft.Xna.Framework;
using Scripts.Game;

public class CycleTileRightEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        Vector2 objDemoCoord = new Vector2(5, 5);
        var currentTile = gridPointer.GetTile(objDemoCoord);

        switch (currentTile.Id)
        {
            case TileIds.Brick:
                gridPointer.SetTile(objDemoCoord, new GrassTile());
                break;
            case TileIds.Grass:
                gridPointer.SetTile(objDemoCoord, new WaterTile());
                break;
            case TileIds.Water:
                gridPointer.SetTile(objDemoCoord, new LavaTile());
                break;
            case TileIds.Lava:
                gridPointer.SetTile(objDemoCoord, new BrickTile());
                break;
            default:
                gridPointer.SetTile(objDemoCoord, new BrickTile());
                break;
        }
        context.ShouldUpdate = true;
        Console.WriteLine($"Cycled tile to {gridPointer.GetTile(objDemoCoord).Id}");
    }
}