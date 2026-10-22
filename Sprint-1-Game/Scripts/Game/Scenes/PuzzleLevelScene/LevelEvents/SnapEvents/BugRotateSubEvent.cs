namespace Scripts.Game;

public class BugRotateSubEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var clockwiseBugs = gridPointer.GetTilePointers(TileIds.Bug);
        var counterclockwiseBugs = gridPointer.GetTilePointers(TileIds.CCBug);

        foreach (var bugCoord in clockwiseBugs.Keys)
        {
            RotateClockwise(spriteGrid, gridPointer, context, bugCoord);
        }
        foreach (var bugCoord in counterclockwiseBugs.Keys)
        {
            RotateCounterclockwise(spriteGrid, gridPointer, context, bugCoord);
        }
    }

    private void RotateClockwise(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Coordinate bugCoord)
    {
        var bug = spriteGrid.GetTileSprite(bugCoord);
        bug = bug.ConvertToAnimated(AnimationNames.BugTileRotate);
        spriteGrid.SetTileSprite(bugCoord, bug);
        spriteGrid.StartTileAnimation(bugCoord);

        var grid = gridPointer.GetGrid(bugCoord);
        var gridObj = grid.Object;
        if (gridObj.IsRotatable)
        {
            if(gridObj.Id == ObjectIds.RedLizard)
            {
                var removeLaser = new RemoveRedLaserSubEvent(objectFactory, bugCoord);
                removeLaser.Execute(spriteGrid, gridPointer, context);
            }

            var currDirection = gridObj.Direction;
            var nextDirection = (Directions)(((int)currDirection + 1) % 4);
            gridObj.Direction = nextDirection;

            var sprite = spriteGrid.GetObjectSprite(bugCoord);
            sprite = sprite.ConvertToAnimated(AnimationNames.ObjectRotateClockwiseFrom(currDirection));
            sprite.IdleName = RegionNames.ToIdleRegionName(nextDirection);
            spriteGrid.SetObjectSprite(bugCoord, sprite);
            spriteGrid.StartObjectAnimation(bugCoord);
        }
    }

    private void RotateCounterclockwise(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context, Coordinate bugCoord)
    {
        var bug = spriteGrid.GetTileSprite(bugCoord);
        bug = bug.ConvertToAnimated(AnimationNames.BugTileRotateCC);
        spriteGrid.SetTileSprite(bugCoord, bug);
        spriteGrid.StartTileAnimation(bugCoord);

        var grid = gridPointer.GetGrid(bugCoord);
        var gridObj = grid.Object;
        if (gridObj.IsRotatable)
        {
            if(gridObj.Id == ObjectIds.RedLizard)
            {
                var removeLaser = new RemoveRedLaserSubEvent(objectFactory, bugCoord);
                removeLaser.Execute(spriteGrid, gridPointer, context);
            }
            
            var currDirection = gridObj.Direction;
            var nextDirection = (Directions)(((int)currDirection + 7) % 4);
            gridObj.Direction = nextDirection;

            var sprite = spriteGrid.GetObjectSprite(bugCoord);
            sprite = sprite.ConvertToAnimated(AnimationNames.ObjectRotateCounterclockwiseFrom(currDirection));
            sprite.IdleName = RegionNames.ToIdleRegionName(nextDirection);
            spriteGrid.SetObjectSprite(bugCoord, sprite);
            spriteGrid.StartObjectAnimation(bugCoord);
        }

    }
}