using Scripts.Game;

public class PushSouthEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;

        var isFacingSouth = playerObj.Direction == Directions.South;
        if (isFacingSouth)
        {
            var southCoord = playerCoord + new Coordinate(0, 1);
            var southGrid = gridPointer.GetGrid(southCoord);
            var southObj = southGrid.Object;

            var southCoordPlus1 = southCoord + new Coordinate(0, 1);
            var southGridPlus1 = gridPointer.GetGrid(southCoordPlus1);

            if (southObj.isPushable && southGridPlus1.Object.Id == ObjectIds.Empty)
            {
                gridPointer.SetObject(southCoord, new EmptyObject());
                gridPointer.SetObject(southCoordPlus1, southObj);
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