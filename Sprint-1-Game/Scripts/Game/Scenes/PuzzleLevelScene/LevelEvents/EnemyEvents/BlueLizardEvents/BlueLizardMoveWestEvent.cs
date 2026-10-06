using Microsoft.Xna.Framework;
using Scripts.Game;
using Scripts.GameComponents;

public class BlueLizardMoveNorthEvent : ILevelEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var blueLizardCoord = gridPointer.GetBlueLizardCoord();
        var blueLizardObj = gridPointer.GetObject(blueLizardCoord) as BlueLizardObject;

        var blueLizardSprite = spriteGrid.GetObjectSprite(blueLizardCoord);

        var isFacingEast = blueLizardObj.Direction == Directions.North;
        if (isFacingEast)
        {
            MoveNorth(spriteGrid, gridPointer, context, blueLizardCoord, blueLizardObj, blueLizardSprite);
        }
        else
        {
            FaceEast(spriteGrid, context, blueLizardCoord, blueLizardObj, blueLizardSprite);
        }

        context.ShouldUpdate = true;
    }

    private static void MoveNorth(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Vector2 objCoord, IObject obj, ISprite sprite)
    {
        // get box east of player
        var newCoord = new Vector2(objCoord.X, objCoord.Y - 1);
        var gridInFront = gridPointer.GetGrid(newCoord);

        // if box east of player is empty,
        if (gridInFront.Object.Id == ObjectIds.Empty)
        {
            // convert sprite to animated sprite and set target coord
            sprite = sprite.ConvertToAnimated(AnimationNames.PlayerWalkEast);
            // set target coordinate
            spriteGrid.SetObjectSprite(objCoord, newCoord, sprite);
            // start animating toward target coord
            spriteGrid.StartObjectAnimation(sprite);

            // make current player coord empty and move player obj to next grid
            gridPointer.SetObject(objCoord, new EmptyObject());
            gridPointer.SetObject(newCoord, obj);
        }
        context.IsIdle = false;
    }

    private static void FaceEast(SpriteGrid spriteGrid, LevelContext context, Vector2 objCoord, IObject obj, ISprite sprite)
    {
        obj.Direction = Directions.North;
        sprite = sprite.ConvertToStatic(RegionNames.PlayerIdleEast);
        sprite.IdleName = RegionNames.PlayerIdleEast;
        // replace old sprite with new static player sprite at pos
        spriteGrid.SetObjectSprite(objCoord, objCoord, sprite);
        context.IsIdle = true;
    }
}