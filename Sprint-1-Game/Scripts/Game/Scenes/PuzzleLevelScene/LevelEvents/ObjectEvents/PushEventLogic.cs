namespace Scripts.Game;

public class PushEventLogic
{
    public static void Push(Directions direction, SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, ObjectFactory objectFactory)
    {
        var directionAsCoordinate = Utilities.ToCoordinate(direction);
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;

        var isFacingEast = playerObj.Direction == direction;
        if (isFacingEast)
        {
            var eastCoord = playerCoord + directionAsCoordinate;
            var eastGrid = gridPointer.GetGrid(eastCoord);
            var eastObj = eastGrid.Object;

            var eastCoordPlus1 = eastCoord + directionAsCoordinate;
            var eastGridPlus1 = gridPointer.GetGrid(eastCoordPlus1);

            var canPush = eastObj.isPushable && eastGridPlus1.Object.Id == ObjectIds.Empty;
            if (canPush)
            {
                gridPointer.SetObject(eastCoord, new EmptyObject());
                gridPointer.SetObject(eastCoordPlus1, eastObj);
            }
            context.ShouldUpdate = true;
        }
        else
        {
            playerObj.Direction = direction;
            context.ShouldUpdate = true;
        }
    }



}