namespace Scripts.Game;

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
        var bug = spriteGrid.GetTileSprite(bugCoord);
        bug = bug.ConvertToAnimated(AnimationNames.BugTileRotate);
        spriteGrid.SetTileSprite(bugCoord, bugCoord, bug);
        spriteGrid.StartTileAnimation(bug);

        var grid = gridPointer.GetGrid(bugCoord);
        var gridObj = grid.Object;
        if (gridObj.IsRotatable)
        {

            var currDirection = gridObj.Direction;
            var nextDirection = (Directions)(((int)currDirection + 1) % 4);
            gridObj.Direction = nextDirection;

            var sprite = spriteGrid.GetObjectSprite(bugCoord);
            sprite = sprite.ConvertToAnimated(AnimationNames.ObjectRotateClockwiseFrom(currDirection));
            sprite.IdleName = RegionNames.ToIdleRegionName(nextDirection);
            spriteGrid.SetObjectSprite(bugCoord, bugCoord, sprite);
            spriteGrid.StartObjectAnimation(sprite);
        }
    }

    private void RotateCounterclockwise(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate bugCoord)
    {
        var bug = spriteGrid.GetTileSprite(bugCoord);
        bug = bug.ConvertToAnimated(AnimationNames.BugTileRotateCC);
        spriteGrid.SetTileSprite(bugCoord, bugCoord, bug);
        spriteGrid.StartTileAnimation(bug);

        var grid = gridPointer.GetGrid(bugCoord);
        var gridObj = grid.Object;
        if (gridObj.IsRotatable)
        {
            var currDirection = gridObj.Direction;
            var nextDirection = (Directions)(((int)currDirection + 7) % 4);
            gridObj.Direction = nextDirection;

            var sprite = spriteGrid.GetObjectSprite(bugCoord);
            sprite = sprite.ConvertToAnimated(AnimationNames.ObjectRotateCounterclockwiseFrom(currDirection));
            sprite.IdleName = RegionNames.ToIdleRegionName(nextDirection);
            spriteGrid.SetObjectSprite(bugCoord, bugCoord, sprite);
            spriteGrid.StartObjectAnimation(sprite);
        }

    }
}