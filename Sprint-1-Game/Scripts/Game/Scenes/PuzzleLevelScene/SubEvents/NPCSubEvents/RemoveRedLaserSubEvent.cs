namespace Scripts.Game;

public class RemoveRedLaserSubEvent(ObjectFactory objectFactory, Coordinate lizardCoord) : ILevelSubEvent
{
    public void Execute(SpriteGrid spriteGrid, GridPointer gridPointer, LevelContext context)
    {
        var lizardDirection = gridPointer.GetObject(lizardCoord).Direction;

        // set lizard to idle
        var lizardSprite = spriteGrid.GetObjectSprite(lizardCoord);
        var idleName = RegionNames.ToIdleRegionName(lizardDirection);
        lizardSprite.SetState(idleName);
        lizardSprite.IdleName = idleName;

        // remove laser effect up until wall
        var dirCoord = lizardCoord + Coordinate.ToCoordinate(lizardDirection);
        var dirGrid = gridPointer.GetGrid(dirCoord);
        while (dirGrid.Object.Id != ObjectIds.Wall)
        {
            spriteGrid.RemoveEffect(dirCoord);

            dirCoord += Coordinate.ToCoordinate(lizardDirection);
            dirGrid = gridPointer.GetGrid(dirCoord);
        }
    }
}