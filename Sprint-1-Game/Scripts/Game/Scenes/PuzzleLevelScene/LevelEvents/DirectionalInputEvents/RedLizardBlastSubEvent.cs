using Microsoft.Xna.Framework;

namespace Scripts.Game;

public class RedLizardBlastSubEvent(ObjectFactory objectFactory) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var redLizards = gridPointer.GetObjectPointers(ObjectIds.RedLizard);

        foreach (var lizard in redLizards)
        {
            LaserBlast(spriteGrid, gridPointer, lizard.Key, lizard.Value.Direction);
        }
    }

    private void LaserBlast(SpriteGrid spriteGrid, GridPointer gridPointer, Coordinate lizardCoord, Directions lizardDirection)
    {
        // set lizard to open mouth animation
        var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);
        lizardSprite.SetState(RegionNames.ToLizardMouthOpenName(lizardDirection));

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

        if(gridPointer.GetObject(dirCoord).IsDestructible)
        {
            gridPointer.SetObject(dirCoord, objectFactory.CreateEmpty());
        }
        spriteGrid.AddIterableEffectSprite(EffectIds.PlayerDeath, startDirCoord, dirCoord, 0);

        spriteGrid.StartIterableEffect(startDirCoord);
    }
}