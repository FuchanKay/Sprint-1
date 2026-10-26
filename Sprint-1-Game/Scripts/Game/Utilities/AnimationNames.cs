using System;

namespace Scripts.Game;

public class AnimationNames
{
    public readonly static string PlayerWalkNorth = "Player Walk North";
    public readonly static string PlayerWalkSouth = "Player Walk South";
    public readonly static string PlayerWalkEast = "Player Walk Right";
    public readonly static string PlayerWalkWest = "Player Walk West";
    public readonly static string PlayerPushNorth = "Player Push North";
    public readonly static string PlayerPushSouth = "Player Push South";
    public readonly static string PlayerPushEast = "Player Push East";
    public readonly static string PlayerPushWest = "Player Push West";

    public readonly static string FrogJumpNorth = "Frog Jump North";
    public readonly static string FrogJumpEast = "Frog Jump East";
    public readonly static string FrogJumpSouth = "Frog Jump South";
    public readonly static string FrogJumpWest = "Frog Jump West";

    public readonly static string ZombieWalkNorth = "Zombie Walk North";
    public readonly static string ZombieWalkEast = "Zombie Walk East";
    public readonly static string ZombieWalkSouth = "Zombie Walk South";
    public readonly static string ZombieWalkWest = "Zombie Walk West";
 

    public readonly static string RotateSE = "Object Rotate SE";
    public readonly static string RotateEN = "Object Rotate EN";
    public readonly static string RotateNW = "Object Rotate NW";
    public readonly static string RotateWS = "Object Rotate WS";
    public readonly static string RotateSW = "Object Rotate SW";
    public readonly static string RotateWN = "Object Rotate WN";
    public readonly static string RotateNE = "Object Rotate NE";
    public readonly static string RotateES = "Object Rotate ES";
    public readonly static string PlayerSnap = "Player Snap";
    public readonly static string BatFly = "Bat Fly";
    public readonly static string BugTileRotate = "Bug Tile Rotate";
    public readonly static string BugTileRotateCC = "Bug Tile Rotate Counterclockwise";
    public readonly static string DoorOpening = "Door Opening";
    public readonly static string Explosion = "Explosion";
    public readonly static string Slash = "Slash";

    public static string ObjectRotateClockwiseFrom(Directions direction)
    {
        return direction switch
        {
            Directions.North => RotateNE,
            Directions.East => RotateES,
            Directions.South => RotateSW,
            Directions.West => RotateWN,
            _ => throw new ArgumentException("Direction not valid"),
        };
    }

    public static string ObjectRotateCounterclockwiseFrom(Directions direction)
    {
        return direction switch
        {
            Directions.North => RotateNW,
            Directions.West => RotateWS,
            Directions.South => RotateSE,
            Directions.East => RotateEN,
            _ => throw new ArgumentException("Direction not valid"),
        };
    }

    public static string ToPlayerMovingAnimationName(Directions direction)
    {
        return direction switch
        {
            Directions.North => PlayerWalkNorth,
            Directions.East => PlayerWalkEast,
            Directions.South => PlayerWalkSouth,
            Directions.West => PlayerWalkWest,
            _ => PlayerWalkEast,
        };
    }

    public static string ToPlayerPushAnimationName(Directions direction)
    {
        return direction switch
        {
            Directions.North => PlayerPushNorth,
            Directions.East => PlayerPushEast,
            Directions.South => PlayerPushSouth,
            Directions.West => PlayerPushWest,
            _ => PlayerPushEast,
        };
    }

    public static string ToFrogJumpAnimationName(Directions direction)
    {
        return direction switch
        {
            Directions.North => FrogJumpNorth,
            Directions.East => FrogJumpEast,
            Directions.South => FrogJumpSouth,
            Directions.West => FrogJumpWest,
            _ => PlayerPushEast,
        };
    }

    public static string ToZombieWalkAnimationName(Directions direction)
    {
        return direction switch
        {
            Directions.North => ZombieWalkNorth,
            Directions.East => ZombieWalkEast,
            Directions.South => ZombieWalkSouth,
            Directions.West => ZombieWalkWest,
            _ => PlayerPushEast,
        };
    }

}