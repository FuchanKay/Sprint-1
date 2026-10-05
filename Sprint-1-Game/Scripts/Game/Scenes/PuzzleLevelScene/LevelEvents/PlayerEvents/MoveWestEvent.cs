using Microsoft.Xna.Framework;
using Scripts.Game;

public class MoveWestEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerGrid = gridPointer.GetGrid(playerCoord);
        var playerObj = playerGrid.Object as PlayerObject;

        var isFacingWest = playerObj.Direction == Directions.West;
        if (isFacingWest)
        {
            var westCoord = new Vector2(playerCoord.X - 1, playerCoord.Y);
            var westGrid = gridPointer.GetGrid(westCoord);
            if (westGrid.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(playerCoord, new EmptyObject());
                gridPointer.SetObject(westCoord, playerObj);
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