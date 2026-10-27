using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Scripts.Game;

public class RedLizardBlastSubEvent(ObjectFactory objectFactory, List<LevelStateHistory> levelStateHistory, Stack<ILevelSubEvent> subEventStack) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, InventoryManager inventoryManager, LevelContext context)
    {
        var redLizards = gridPointer.GetObjectPointers(ObjectIds.RedLizard);

        foreach (var lizard in redLizards)
        {
            var lizardCoord = lizard.Key;
            var lizardDirection = lizard.Value.Direction;
            var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);

            var furthestObjectCoord = GetBlastTargetCoord(gridPointer, lizardCoord, lizardDirection);
            var currObjectAhead = gridPointer.GetObject(furthestObjectCoord);
            var blastableObjectAhead = currObjectAhead.Id != ObjectIds.Wall;

            var prev = levelStateHistory.Count - 1;
            var prevGridPointer = levelStateHistory[prev].GridPointer;
            var prevCoord = GetBlastTargetCoord(prevGridPointer, lizardCoord, lizardDirection);
            var prevObject = prevGridPointer.GetObject(furthestObjectCoord);
            var isNewObject = prevObject.Id != currObjectAhead.Id || prevCoord.X != furthestObjectCoord.X || prevCoord.Y != furthestObjectCoord.Y;

            var isIdle = lizardSprite.IdleName.Contains("Idle");

            if (blastableObjectAhead && (isIdle || isNewObject))
            {
                LaserBlast(spriteGrid, gridPointer, lizardCoord, furthestObjectCoord, lizardDirection);
            }
        }
    }

    public static Coordinate GetBlastTargetCoord(GridPointer gridPointer, Coordinate lizardCoord, Directions lizardDirection)
    {
        var prevCoord = GetNextObjectCoord(gridPointer, lizardCoord, lizardDirection);
        var currCoord = prevCoord;

        var obj = gridPointer.GetObject(currCoord);

        while(obj.Id != ObjectIds.Wall && obj.Id != ObjectIds.Rock)
        {
            prevCoord = currCoord;
            currCoord = GetNextObjectCoord(gridPointer, prevCoord, lizardDirection);
            obj = gridPointer.GetObject(currCoord);
        }

        var returnCoord = prevCoord;
        if(obj.Id == ObjectIds.Rock)
        {
            returnCoord = currCoord;
        }

        return returnCoord;
    }

    private static Coordinate GetNextObjectCoord(GridPointer gridPointer, Coordinate lizardCoord, Directions lizardDirection)
    {
        var dirCoord = lizardCoord + Coordinate.ToCoordinate(lizardDirection);
        var dirGrid = gridPointer.GetGrid(dirCoord);

        while (dirGrid.Object.Id == ObjectIds.Empty)
        {
            dirCoord += Coordinate.ToCoordinate(lizardDirection);
            dirGrid = gridPointer.GetGrid(dirCoord);
        }
        
        return dirCoord;
    }

    private void LaserBlast(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate startCoord, Coordinate endCoord, Directions lizardDirection)
    {
        var lizardSprite = spriteGrid.GetObjectSprite(startCoord);
        var mouthOpen = RegionNames.ToLizardMouthOpenName(lizardDirection);
        lizardSprite.SetState(mouthOpen);
        lizardSprite.IdleName = mouthOpen;

        // N/S -> rotation = 0
        // E/W -> rotation = 90
        var rotation = MathHelper.ToRadians(((int)lizardDirection % 2) * 90);

        var dirCoord = startCoord + Coordinate.ToCoordinate(lizardDirection);
        var startDirCoord = dirCoord;
        var dirGrid = gridPointer.GetGrid(dirCoord);
        while (dirCoord.X != endCoord.X || dirCoord.Y != endCoord.Y)
        {
            spriteGrid.AddIterableEffectSprite(EffectIds.LaserBeam, startDirCoord, dirCoord, rotation);

            if(dirGrid.Object.Id == ObjectIds.Player)
            {
                subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
            } else
            {
                gridPointer.SetObject(dirCoord, objectFactory.CreateEmpty());
                spriteGrid.RemoveObjectSprite(dirCoord);
            }

            dirCoord += Coordinate.ToCoordinate(lizardDirection);
            dirGrid = gridPointer.GetGrid(dirCoord);
        }

        if(dirGrid.Object.IsDestructible)
        {
            gridPointer.SetObject(dirCoord, objectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(dirCoord);
        }

        if(dirGrid.Object.Id == ObjectIds.Player)
        {
            subEventStack.Push(new PlayerDeathSubEvent(objectFactory));
        }

        spriteGrid.AddIterableEffectSprite(EffectIds.PlayerDeath, startDirCoord, dirCoord, 0);

        spriteGrid.StartIterableEffect(startDirCoord);
    }
}