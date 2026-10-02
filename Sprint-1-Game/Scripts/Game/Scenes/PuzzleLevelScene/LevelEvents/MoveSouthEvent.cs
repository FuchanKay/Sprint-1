using Microsoft.Xna.Framework;
using Scripts.Game;

public class MoveSouthEvent : ILevelEvent
{
    public void Execute(GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerGrid = gridPointer.GetGrid(playerCoord);
        var playerObj = playerGrid.Object as PlayerObject;
        if (playerObj.Direction != Directions.South)
        {
            playerObj.Direction = Directions.South;
            context.ShouldUpdate = true;
        }
        else
        {
            var eastCoord = new Vector2(playerCoord.X, playerCoord.Y + 1);
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