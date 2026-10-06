using Microsoft.Xna.Framework;
using Scripts.Game;

public class MoveNorthEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerGrid = gridPointer.GetGrid(playerCoord);
        var playerObj = playerGrid.Object as PlayerObject;

        var isFacingNorth = playerObj.Direction == Directions.North;
        if (isFacingNorth)
        {
            var northCoord = new Vector2(playerCoord.X, playerCoord.Y - 1);
            var northGrid = gridPointer.GetGrid(northCoord);
            if (northGrid.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(playerCoord, new EmptyObject());
                gridPointer.SetObject(northCoord, playerObj);
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