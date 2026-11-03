namespace Scripts.Game;

public class ObjectFactory()
{
    public static IObject CreateEmpty()
    {
        return new EmptyObject
        {
            Direction = Directions.South,
        };
    }

    public static IObject CreatePlayer()
    {
        return new PlayerObject
        {
            Direction = Directions.South,
        };
    }

    private static IObject CreateRock(string regionName)
    {
        return new RockObject
        {
            Direction = Directions.South,
            RegionName = regionName
        };
    }

    public static IObject CreateDefaultRock()
    {
        return CreateRock(RegionNames.Rock);
    }

    public static IObject CreatePumpkin()
    {
        return CreateRock(RegionNames.Pumpkin);
    }

    private static IObject CreateSeed(string regionName, Directions direction)
    {
        return new SeedObject
        {
            Direction = direction,
            RegionName = regionName
        };
    }

    public static IObject CreatePumpkinSeed()
    {
        return CreateSeed(RegionNames.PumpkinSeed, Directions.South);
    }

    public static IObject CreateVineSeed(Directions direction)
    {
        return CreateSeed(RegionNames.VineSeed, direction);
    }

    private static IObject CreateSkinnedWall(Directions direction, string regionName)
    {
        return new WallObject
        {
            Direction = direction,
            RegionName = regionName
        };
    }

    public static IObject CreateWall(Directions direction)
    {
        return CreateSkinnedWall(direction, RegionNames.Wall);
    }

    public static IObject CreateCornerWall(Directions direction)
    {
        return CreateSkinnedWall(direction, RegionNames.CornerWall);
    }

    public static IObject CreateStandaloneWall(Directions direction)
    {
        return CreateSkinnedWall(direction, RegionNames.StandaloneWall);
    }

    public static IObject CreateTopOfWall(Directions direction)
    {
        return CreateSkinnedWall(direction, RegionNames.TopOfWall);
    }

    public static IObject CreateBomb()
    {
        return new BombObject
        {
            Direction = Directions.South,
        };
    }

    public static IObject CreateKey()
    {
        return new KeyObject
        {
            Direction = Directions.South,
        };
    }

    public static IObject CreateClosedDoor()
    {
        return new DoorObject
        {
            Direction = Directions.South,
        };
    }

    public static IObject CreateOpenDoor()
    {
        return new DoorObject
        {
            Direction = Directions.South,
            IsOpen = true
        };
    }

    public static IObject CreateTimedBomb()
    {
        return new TimedBombObject
        {
            Direction = Directions.South,
        };
    }

    public static IObject CreateSkeleton()
    {
        return new Skeleton
        {
            Direction = Directions.South,
        };
    }

    public static IObject CreateWarlock()
    {
        return new Warlock
        {
            Direction = Directions.South,
        };
    }

    public static IObject CreateBlueLizard(Directions direction)
    {
        return new BlueLizard
        {
            Direction = direction,
        };
    }

    public static IObject CreateRedLizard(Directions direction)
    {
        return new RedLizard
        {
            Direction = direction,
        };
    }

    public static IObject CreateBat()
    {
        return new Bat
        {
          Direction = Directions.South,   
        };
    }

    public static IObject CreateFrog()
    {
        return new Frog
        {
          Direction = Directions.South,   
        };
    }

    public static IObject CreateZombie()
    {
        return new Zombie
        {
          Direction = Directions.South,   
        };
    }
}