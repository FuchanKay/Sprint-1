using Scripts.Game;

public class BugRotateSubEvent : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var clockwiseBugs = gridPointer.GetTilePointers(TileIds.Bug);
        var counterclockwiseBugs = gridPointer.GetTilePointers(TileIds.CCBug);

        foreach (var bugCoord in clockwiseBugs.Keys)
        {
            RotateClockwise(spriteGrid, gridPointer, bugCoord);
        }
        foreach (var bugCoord in counterclockwiseBugs.Keys)
        {
            RotateCounterclockwise(spriteGrid, gridPointer, bugCoord);
        }
    }

    private static void RotateClockwise(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate bugCoord)
    {
        // play bug animation
        var bug = spriteGrid.GetTileSprite(bugCoord);
        bug = bug.ConvertToAnimated(AnimationNames.BugTileRotate);
        spriteGrid.SetTileSprite(bugCoord, bugCoord, bug);
        spriteGrid.StartTileAnimation(bug);

        // play object rotation animation
        var grid = gridPointer.GetGrid(bugCoord);
        var gridObj = grid.Object;
        if (gridObj.IsRotatable)
        {

            // set object to next direction
            var currDirection = gridObj.Direction;

            // rotates direction clockwise without switch statement:
            // 0 + 1 % 4 = 1
            // 1 + 1 % 4 = 2
            // 2 + 1 % 4 = 3
            // 3 + 1 % 4 = 0
            var nextDirection = (Directions)(((int)currDirection + 1) % 4);
            gridObj.Direction = nextDirection;

            // animate towards next direction and set new idle state
            var sprite = spriteGrid.GetObjectSprite(bugCoord);
            sprite = sprite.ConvertToAnimated(AnimationNames.ObjectRotateClockwiseFrom(currDirection));
            sprite.IdleName = RegionNames.ToIdleRegionName(nextDirection);
            spriteGrid.SetObjectSprite(bugCoord, bugCoord, sprite);
            spriteGrid.StartObjectAnimation(sprite);
        }
    }

    private void RotateCounterclockwise(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate bugCoord)
    {
        // play bug animation
        var bug = spriteGrid.GetTileSprite(bugCoord);
        bug = bug.ConvertToAnimated(AnimationNames.BugTileRotateCC);
        spriteGrid.SetTileSprite(bugCoord, bugCoord, bug);
        spriteGrid.StartTileAnimation(bug);

        // play object rotation animation
        var grid = gridPointer.GetGrid(bugCoord);
        var gridObj = grid.Object;
        if (gridObj.IsRotatable)
        {
            // set object to next direction
            var currDirection = gridObj.Direction;

            // moves direction backwards without switch statement:
            // 0 + 7 % 4 = 3
            // 1 + 7 % 4 = 0
            // 2 + 7 % 4 = 1
            // 3 + 7 % 4 = 2
            var nextDirection = (Directions)(((int)currDirection + 7) % 4);
            gridObj.Direction = nextDirection;

            // animate towards next direction
            var sprite = spriteGrid.GetObjectSprite(bugCoord);
            sprite = sprite.ConvertToAnimated(AnimationNames.ObjectRotateCounterclockwiseFrom(currDirection));
            sprite.IdleName = RegionNames.ToIdleRegionName(nextDirection);
            spriteGrid.SetObjectSprite(bugCoord, bugCoord, sprite);
            spriteGrid.StartObjectAnimation(sprite);
        }
        
    }
}