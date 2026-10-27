using System.Collections.Generic;

namespace Scripts.Game;

public class CheckRemoveRedLaserSubEvent(List<LevelStateHistory> undoHistory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var redLizards = gridPointer.GetObjectPointers(ObjectIds.RedLizard);
        var blueLizards = gridPointer.GetObjectPointers(ObjectIds.BlueLizard);

        foreach (var lizard in blueLizards)
        {
            var coord = lizard.Key;
            var direction = gridPointer.GetObject(coord).Direction;
            RemoveLaserBeam(spriteGrid, gridPointer, coord, coord, direction, direction);
        }

        foreach (var lizard in redLizards)
        {
            var previous = undoHistory.Count - 1;
            var prevGridPointer = undoHistory[previous].GridPointer;

            var coord = lizard.Key;
            var lizardObj = lizard.Value;
            var prevObjAtCoord = prevGridPointer.GetObject(coord);

            var objectCoordAhead = Pathfinder.GetBlastTargetCoord(gridPointer, coord, lizardObj.Direction);
            var objectAhead = gridPointer.GetObject(objectCoordAhead);
            var wallAhead = objectAhead.Id == ObjectIds.Wall;

            var prevCoord = coord;
            var positionChanged = prevObjAtCoord.Id != ObjectIds.RedLizard;
            if(positionChanged)
            {
                prevCoord = Pathfinder.GetPlayerCoord(gridPointer, coord);
            }

            var prevObjectCoordAhead = Pathfinder.GetBlastTargetCoord(prevGridPointer, coord, lizardObj.Direction);
            var prevNotWall = prevGridPointer.GetObject(prevObjectCoordAhead).Id != ObjectIds.Wall;
            var newObjectAppeared = prevNotWall && objectCoordAhead != prevObjectCoordAhead;

            var directionChanged = false;
            if(prevObjAtCoord.Id == ObjectIds.RedLizard)
            {
                directionChanged = prevObjAtCoord.Direction != lizardObj.Direction;
            }

            if(directionChanged && wallAhead)
            {
                RemoveLaserBeam(spriteGrid, gridPointer, coord, coord, lizardObj.Direction, prevObjAtCoord.Direction);
                RemoveLaserBeam(spriteGrid, gridPointer, coord, coord, lizardObj.Direction, lizardObj.Direction);
            } else if(wallAhead || positionChanged || newObjectAppeared)
            { // need to remove bc wall, remove at prev position, or to prep for new blast
                RemoveLaserBeam(spriteGrid, gridPointer, coord, prevCoord, lizardObj.Direction, lizardObj.Direction);
            }
        }
    }

    private static void RemoveLaserBeam(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate lizardCoord, Coordinate prevLizardCoord, Directions lizardDirection, Directions prevLizardDirection)
    {
        var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);
        var idleName = RegionNames.ToIdleRegionName(lizardDirection);
        lizardSprite.SetState(idleName);
        lizardSprite.IdleName = idleName;

        var dirCoord = prevLizardCoord + Coordinate.ToCoordinate(prevLizardDirection);
        var dirGrid = gridPointer.GetGrid(dirCoord);
        while (dirGrid.Object.Id != ObjectIds.Wall)
        {
            spriteGrid.RemoveEffect(dirCoord);

            dirCoord += Coordinate.ToCoordinate(prevLizardDirection);
            dirGrid = gridPointer.GetGrid(dirCoord);
        }
    }


}