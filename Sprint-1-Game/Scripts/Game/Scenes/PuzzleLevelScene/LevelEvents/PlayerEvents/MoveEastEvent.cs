using Microsoft.Xna.Framework;
using Scripts.Game;
<<<<<<< HEAD
=======
using Scripts.GameComponents;
>>>>>>> origin/main

public class MoveEastEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var playerCoord = gridPointer.GetPlayerCoord();
        var playerObj = gridPointer.GetObject(playerCoord) as PlayerObject;

<<<<<<< HEAD
        var isFacingEast = playerObj.Direction == Directions.East;
        if (isFacingEast)
        {
            var eastCoord = playerCoord + new Vector2(1, 0);
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
            playerObj.Direction = Directions.East;
            context.ShouldUpdate = true;
        }
=======
        var playerSprite = spriteGrid.GetObjectSprite(playerCoord);

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

    private static void MoveEast(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Vector2 playerCoord, IObject playerObj, ISprite playerSprite)
    {
        // get box east of player
        var coordXPlus1 = playerCoord + new Vector2(1, 0);
        var xPlus1Grid = gridPointer.GetGrid(coordXPlus1);

        // if box east of player is empty,
        if (xPlus1Grid.Object.Id == ObjectIds.Empty)
        {
            // convert sprite to animated sprite and set target coord
            playerSprite = playerSprite.ConvertToAnimated(AnimationNames.PlayerWalkEast);
            // set target coordinate
            spriteGrid.SetObjectSprite(playerCoord, coordXPlus1, playerSprite);
            // start animating toward target coord
            spriteGrid.StartObjectAnimation(playerSprite);

            // make current player coord empty and move player obj to next grid
            gridPointer.SetObject(playerCoord, new EmptyObject());
            gridPointer.SetObject(coordXPlus1, playerObj);
        }
        context.IsIdle = false;
    }

    private static void FaceEast(SpriteGrid spriteGrid, LevelContext context, Vector2 playerCoord, IObject playerObj, ISprite playerSprite)
    {
        playerObj.Direction = Directions.East;
        playerSprite = playerSprite.ConvertToStatic(RegionNames.PlayerIdleEast);
        playerSprite.IdleName = RegionNames.PlayerIdleEast;
        // replace old sprite with new static player sprite at pos
        spriteGrid.SetObjectSprite(playerCoord, playerCoord, playerSprite);
        context.IsIdle = true;
>>>>>>> origin/main
    }
}