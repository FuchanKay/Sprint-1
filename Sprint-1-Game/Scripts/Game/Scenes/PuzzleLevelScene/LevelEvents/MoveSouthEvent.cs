using Microsoft.Xna.Framework;
using Scripts.Game;

public class MoveSouthEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerGrid = gridPointer.GetGrid(playerCoord);
        var playerObj = playerGrid.Object as PlayerObject;

        var isFacingSouth = playerObj.Direction == Directions.South;
        if (isFacingSouth)
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
        else
        {
            playerObj.Direction = Directions.South;
            context.ShouldUpdate = true;
        }
    }
}