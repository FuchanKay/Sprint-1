using Microsoft.Xna.Framework;
using Scripts.Game;

public class MoveWestEvent : ILevelEvent
{
    public void Execute(GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerGrid = gridPointer.GetGrid(playerCoord);
        var playerObj = playerGrid.Object as PlayerObject;
        if (playerObj.Direction != Directions.West)
        {
            playerObj.Direction = Directions.West;
            context.ShouldUpdate = true;
        }
        else
        {
            var eastCoord = new Vector2(playerCoord.X - 1, playerCoord.Y);
            var eastGrid = gridPointer.GetGrid(eastCoord);
            if (eastGrid.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(playerCoord, new EmptyObject());
                gridPointer.SetObject(eastCoord, playerObj);
            }
            context.ShouldUpdate = true;
        }
    }
}