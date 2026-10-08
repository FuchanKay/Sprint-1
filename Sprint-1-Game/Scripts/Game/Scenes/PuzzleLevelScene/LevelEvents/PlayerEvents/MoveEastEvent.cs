using Microsoft.Xna.Framework;
using Scripts.Game;
using Scripts.GameComponents;

public class MoveEastEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;

        var playerSprite = spriteGrid.GetObjectSprite(playerCoord.ToVector2());

        var isFacingEast = playerObj.Direction == Directions.East;
        if (isFacingEast)
        {
            MoveEast(spriteGrid, gridPointer, context, playerCoord, playerObj, playerSprite);
        }
        else
        {
            FaceEast(spriteGrid, context, playerCoord, playerObj, playerSprite);
        }

        context.ShouldUpdate = true;
    }

    private static void MoveEast(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        // get box east of player
        var east = new Coordinate(1, 0);
        var playerEast = playerCoord + east;
        var eastGrid = gridPointer.GetGrid(playerEast);

        // if box east of player is empty,
        if (eastGrid.Object.Id == ObjectIds.Empty)
        {
            // convert sprite to animated sprite and set target coord
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkEast);
            // set target coordinate
            spriteGrid.SetObjectSprite(playerCoord.ToVector2(), playerEast.ToVector2(), playerSprite);
            // start animating toward target coord
            spriteGrid.StartObjectAnimation(playerSprite);

            // make current player coord empty and move player obj to next grid
            gridPointer.SetObject(playerCoord, new EmptyObject());
            gridPointer.SetObject(playerEast, playerObj);
        }
        context.IsIdle = false;
    }

    private static void FaceEast(SpriteGrid spriteGrid, LevelContext context, Coordinate playerCoord, IObject playerObj, ISprite playerSprite)
    {
        playerObj.Direction = Directions.East;
        playerSprite = playerSprite.ConvertToStatic(RegionNames.PlayerIdleEast);
        playerSprite.IdleName = RegionNames.PlayerIdleEast;
        // replace old sprite with new static player sprite at pos
        spriteGrid.SetObjectSprite(playerCoord.ToVector2(), playerCoord.ToVector2(), playerSprite);
        context.IsIdle = true;
    }
}