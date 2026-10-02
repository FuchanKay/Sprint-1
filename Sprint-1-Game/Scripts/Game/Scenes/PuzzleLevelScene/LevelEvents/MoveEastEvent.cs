using Microsoft.Xna.Framework;
using Scripts.Game;

public class MoveEastEvent : ILevelEvent
{
    public void Execute(GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;
        if (playerObj.Direction != Directions.East)
        {
            playerObj.Direction = Directions.East;
            context.ShouldUpdate = true;
        }
        else
        {
            var eastCoord = new Vector2(playerCoord.X + 1, playerCoord.Y);
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