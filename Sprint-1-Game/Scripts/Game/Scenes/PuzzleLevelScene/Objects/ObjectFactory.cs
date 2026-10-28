using System.Threading.Tasks.Dataflow;

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

    private static IObject CreateRock(string textureName, string regionName)
    {
        return new RockObject
        {
            Direction = Directions.South,
            TextureName = textureName,
            RegionName = regionName
        };
    }

    public static IObject CreateDefaultRock()
    {
        return CreateRock(TextureNames.Rock, RegionNames.Rock);
    }

    //TODO: implement this
    // public IObject CreatePumpkin()
    // {
    //     return CreateRock(TextureNames.Pumpkin, RegionNames.Pumpkin);
    // }

    public static IObject CreateWall()
    {
        return new WallObject
        {
            Direction = Directions.South,
        };
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

    public static IObject CreateBlueLizard()
    {
        return new BlueLizard
        {
            Direction = Directions.South,
        };
    }

    public static IObject CreateRedLizard()
    {
        return new RedLizard
        {
            Direction = Directions.South,
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