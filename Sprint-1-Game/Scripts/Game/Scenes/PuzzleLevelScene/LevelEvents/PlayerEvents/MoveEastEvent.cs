using Microsoft.Xna.Framework;
using Scripts.Game;

public class MoveEastEvent : ILevelEvent
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

            if (eastGrid.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(playerCoord, new EmptyObject());
                gridPointer.SetObject(eastCoord, playerObj);
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