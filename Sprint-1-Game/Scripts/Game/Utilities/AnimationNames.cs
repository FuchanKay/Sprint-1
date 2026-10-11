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
    public readonly static string PlayerRotateSE = "Player Rotate SE";
    public readonly static string PlayerRotateEN = "Player Rotate EN";
    public readonly static string PlayerRotateNW = "Player Rotate NW";
    public readonly static string PlayerRotateWS = "Player Rotate WS";
    public readonly static string PlayerRotateSW = "Player Rotate SW";
    public readonly static string PlayerRotateWN = "Player Rotate WN";
    public readonly static string PlayerRotateNE = "Player Rotate NE";
    public readonly static string PlayerRotateES = "Player Rotate ES";
    public readonly static string PlayerSnap = "Player Snap";
    public readonly static string Explosion = "Explosion";

    public static string ToPlayerMovingAnimationName(Directions direction)
    {
        return direction switch
        {
            Directions.North => AnimationNames.PlayerWalkNorth,
            Directions.East => AnimationNames.PlayerWalkEast,
            Directions.South => AnimationNames.PlayerWalkSouth,
            Directions.West => AnimationNames.PlayerWalkWest,
            _ => AnimationNames.PlayerWalkEast,
        };
    }

}