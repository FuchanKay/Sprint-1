using System;
using Microsoft.Xna.Framework;
using Scripts.Game;

public class PushNorthEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;

        var isFacingNorth = playerObj.Direction == Directions.North;
        if (isFacingNorth)
        {
            var northCoord = playerCoord + new Vector2(0, -1);
            var northGrid = gridPointer.GetGrid(northCoord);
            var northObj = northGrid.Object;

            bool northIsPushable = Enum.IsDefined(typeof(PushableObjectIds), northObj.Id);

            var northCoordPlus1 = northCoord + new Vector2(0, -1);
            var northGridPlus1 = gridPointer.GetGrid(northCoordPlus1);

            if (northIsPushable && northGridPlus1.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(northCoord, new EmptyObject());
                gridPointer.SetObject(northCoordPlus1, northObj);
            }
            context.ShouldUpdate = true;
        }
        else
        {
            playerObj.Direction = Directions.North;
            context.ShouldUpdate = true;
        }
    }
}