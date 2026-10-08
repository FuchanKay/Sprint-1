using Scripts.Game;
using Scripts.GameComponents;

public class MoveWestEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerGrid = gridPointer.GetGrid(playerCoord);
        var playerObj = playerGrid.Object as PlayerObject;

        var playerSprite = spriteGrid.GetObjectSprite(playerCoord.ToVector2());

        var isFacingWest = playerObj.Direction == Directions.West;
        if (isFacingWest)
        {
            MoveWest(spriteGrid, gridPointer, context, playerCoord, playerObj, playerSprite);
        }
        else
        {
            FaceWest(spriteGrid, context, playerCoord, playerObj, playerSprite);
        }
    }

        private static void MoveWest(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        var west = new Coordinate(-1, 0);
        var playerWest = playerCoord + west;
        var westGrid = gridPointer.GetGrid(playerWest);

        if (westGrid.Object.Id == ObjectIds.Empty)
        {
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkWest);
            spriteGrid.SetObjectSprite(playerCoord.ToVector2(), playerWest.ToVector2(), playerSprite);
            spriteGrid.StartObjectAnimation(playerSprite);

            gridPointer.SetObject(playerCoord, new EmptyObject());
            gridPointer.SetObject(playerWest, playerObj);
        }
        context.IsIdle = false;
    }

    private static void FaceWest(SpriteGrid spriteGrid, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        playerObj.Direction = Directions.West;

        playerSprite = playerSprite.ConvertToStatic(RegionNames.PlayerIdleWest);
        playerSprite.IdleName = RegionNames.PlayerIdleWest;
        spriteGrid.SetObjectSprite(playerCoord.ToVector2(), playerCoord.ToVector2(), playerSprite);
        
        context.IsIdle = true;
    }
}