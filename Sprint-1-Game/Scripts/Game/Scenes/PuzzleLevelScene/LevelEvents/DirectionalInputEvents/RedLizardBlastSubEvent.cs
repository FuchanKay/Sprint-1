using Microsoft.Xna.Framework;

namespace Scripts.Game;

public class RedLizardBlastSubEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var redLizards = gridPointer.GetObjectPointers(ObjectIds.RedLizard);

        foreach (var lizard in redLizards)
        {
            var lizardCoord = lizard.Key;
            var lizardDirection = lizard.Value.Direction;
            var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);
            
            var isIdle = lizardSprite.IdleName.Contains("Idle");
            var objectAhead = CheckObjectAhead(gridPointer, lizardCoord, lizardDirection);

            if (objectAhead && isIdle)
            {
                LaserBlast(spriteGrid, gridPointer, lizardCoord, lizardDirection);
            } else if(!objectAhead)
            {
                var removeLaser = new RemoveRedLaserSubEvent(objectFactory, lizardCoord);
                removeLaser.Execute(spriteGrid, gridPointer, context);
            }
        }
    }

    private bool CheckObjectAhead(GridPointer gridPointer, Coordinate lizardCoord, Directions lizardDirection)
    {
        var dirCoord = lizardCoord + Coordinate.ToCoordinate(lizardDirection);
        var dirGrid = gridPointer.GetGrid(dirCoord);

        // if something is destructible, will stop
        while (dirGrid.Object.Id == ObjectIds.Empty)
        {
            dirCoord += Coordinate.ToCoordinate(lizardDirection);
            dirGrid = gridPointer.GetGrid(dirCoord);
        }

        // keep blasting if non-wall object
        if(dirGrid.Object.Id == ObjectIds.Wall)
        {
            return false;
        }
        return true;
    }

    private void LaserBlast(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate lizardCoord, Directions lizardDirection)
    {
        // set lizard to open mouth animation
        var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);
        var mouthOpen = RegionNames.ToLizardMouthOpenName(lizardDirection);
        lizardSprite.SetState(mouthOpen);
        lizardSprite.IdleName = mouthOpen;

        // N/S -> rotation = 0
        // E/W -> rotation = 90
        var rotation = MathHelper.ToRadians(((int)lizardDirection % 2) * 90);

        // while grid in direction is not empty, create a new laser
        var dirCoord = lizardCoord + Coordinate.ToCoordinate(lizardDirection);
        var startDirCoord = dirCoord;
        var dirGrid = gridPointer.GetGrid(dirCoord);
        while (dirGrid.Object.Id == ObjectIds.Empty)
        {
            spriteGrid.AddIterableEffectSprite(EffectIds.LaserBeam, startDirCoord, dirCoord, rotation);

            dirCoord += Coordinate.ToCoordinate(lizardDirection);
            dirGrid = gridPointer.GetGrid(dirCoord);
        }

        if(dirGrid.Object.IsDestructible)
        {
            gridPointer.SetObject(dirCoord, objectFactory.CreateEmpty());
            spriteGrid.RemoveObjectSprite(dirCoord);
        }
        spriteGrid.AddIterableEffectSprite(EffectIds.PlayerDeath, startDirCoord, dirCoord, 0);

        spriteGrid.StartIterableEffect(startDirCoord);
    }
}