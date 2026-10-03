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
            var coordXPlus1 = playerCoord + new Vector2(1, 0);
            var xPlus1Grid = gridPointer.GetGrid(coordXPlus1);

            if (xPlus1Grid.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(playerCoord, new EmptyObject());
                gridPointer.SetObject(coordXPlus1, playerObj);
            }

            context.ShouldUpdate = true;
        }
    }
}