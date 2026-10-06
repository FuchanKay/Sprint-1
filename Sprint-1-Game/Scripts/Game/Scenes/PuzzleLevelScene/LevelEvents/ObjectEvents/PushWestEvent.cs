using System;
using Microsoft.Xna.Framework;
using Scripts.Game;

public class PushWestEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;

        var isFacingWest = playerObj.Direction == Directions.West;
        if (isFacingWest)
        {
            var westCoord = playerCoord + new Vector2(-1, 0);
            var westGrid = gridPointer.GetGrid(westCoord);
            var westObj = westGrid.Object;

            var westCoordPlus1 = westCoord + new Vector2(-1, 0);
            var westGridPlus1 = gridPointer.GetGrid(westCoordPlus1);

            if (westObj.isPushable && westGridPlus1.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(westCoord, new EmptyObject());
                gridPointer.SetObject(westCoordPlus1, westObj);
            }
            context.ShouldUpdate = true;
        }
        else
        {
            playerObj.Direction = Directions.West;
            context.ShouldUpdate = true;
        }
    }
}