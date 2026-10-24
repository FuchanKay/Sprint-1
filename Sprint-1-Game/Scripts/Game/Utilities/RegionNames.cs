namespace Scripts.Game;

public class RegionNames
{
    public readonly static string Rock = "Rock";
    public readonly static string Wall = "Wall";
    public readonly static string ClosedDoor = "Closed Door";
    public readonly static string OpenDoor = "Open Door";
    public readonly static string Bomb = "Bomb";
    public readonly static string Key = "Key";
    public readonly static string TimedBomb = "Timed Bomb";
    public readonly static string Explosion = "Explosion";

    public readonly static string DirtTile = "Dirt Tile";
    public readonly static string GrassTile = "Grass Tile";
    public readonly static string BrickTile = "Brick Tile";
    public readonly static string MossyBrickTile = "Mossy Brick Tile";
    public readonly static string PitTile = "Pit Tile";
    public readonly static string WaterTile = "Water Tile";
    public readonly static string LavaTile = "Lava Tile";
    public readonly static string BugTile = "Bug Tile";
    public readonly static string BugTileCC = "Bug Tile Counterclockwise";

    public readonly static string IdleSouth = "Object Idle South";
    public readonly static string IdleNorth = "Object Idle North";
    public readonly static string IdleEast = "Object Idle East";
    public readonly static string IdleWest = "Object Idle West";

    public readonly static string RedLizard = "RedLizard";
    public readonly static string BlueLizard = "BlueLizard";
    public readonly static string Skeleton = "Skeleton";
    public readonly static string Slash = "Slash";
    public readonly static string Warlock = "Warlock";

    public static string ToIdleRegionName(Directions direction)
    {
        return direction switch
        {
            Directions.North => RegionNames.IdleNorth,
            Directions.East => RegionNames.IdleEast,
            Directions.South => RegionNames.IdleSouth,
            Directions.West => RegionNames.IdleWest,
            _ => RegionNames.IdleEast,
        };
    }
}