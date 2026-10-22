using System;

namespace Scripts.Game;

public class RegionNames
{
    // objects
    public readonly static string Rock = "Rock";
    public readonly static string Wall = "Wall";
    public readonly static string Bomb = "Bomb";
    public readonly static string TimedBomb = "Timed Bomb";
    public readonly static string Explosion = "Explosion";

    // tiles
    public readonly static string DirtTile = "Dirt Tile";
    public readonly static string GrassTile = "Grass Tile";
    public readonly static string BrickTile = "Brick Tile";
    public readonly static string MossyBrickTile = "Mossy Brick Tile";
    public readonly static string PitTile = "Pit Tile";
    public readonly static string WaterTile = "Water Tile";
    public readonly static string LavaTile = "Lava Tile";
    public readonly static string BugTile = "Bug Tile";
    public readonly static string BugTileCC = "Bug Tile Counterclockwise";
    public readonly static string LaserBeam = "Laser Beam";

    // player/npc states
    public readonly static string IdleSouth = "Object Idle South";
    public readonly static string IdleNorth = "Object Idle North";
    public readonly static string IdleEast = "Object Idle East";
    public readonly static string IdleWest = "Object Idle West";
    public readonly static string LizardMouthOpenNorth = "Lizard Mouth Open North";
    public readonly static string LizardMouthOpenEast = "Lizard Mouth Open East";
    public readonly static string LizardMouthOpenSouth = "Lizard Mouth Open South";
    public readonly static string LizardMouthOpenWest = "Lizard Mouth Open West";

    public readonly static string Skeleton = "Skeleton";
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

    public static string ToLizardMouthOpenName(Directions direction)
    {
        return direction switch
        {
            Directions.North => RegionNames.LizardMouthOpenNorth,
            Directions.East => RegionNames.LizardMouthOpenEast,
            Directions.South => RegionNames.LizardMouthOpenSouth,
            Directions.West => RegionNames.LizardMouthOpenWest,
            _ => throw new ArgumentException("Direction not found"),
        };
    }
}