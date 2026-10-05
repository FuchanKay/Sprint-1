using System;
using Microsoft.Xna.Framework;
using Scripts.Game;

public class PushEastEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;

        var isFacingEast = playerObj.Direction == Directions.East;
        if (isFacingEast)
        {
            var eastCoord = playerCoord + new Vector2(1, 0);
            var eastGrid = gridPointer.GetGrid(eastCoord);
            var eastObj = eastGrid.Object;

            bool eastIsPushable = Enum.IsDefined(typeof(PushableObjectIds), eastObj.Id);

            var eastCoordPlus1 = eastCoord + new Vector2(1, 0);
            var eastGridPlus1 = gridPointer.GetGrid(eastCoordPlus1);

            if (eastIsPushable && eastGridPlus1.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(eastCoord, new EmptyObject());
                gridPointer.SetObject(eastCoordPlus1, eastObj);
            }
            context.ShouldUpdate = true;
        }
        else
        {
            playerObj.Direction = Directions.East;
            context.ShouldUpdate = true;
        }
    }
}