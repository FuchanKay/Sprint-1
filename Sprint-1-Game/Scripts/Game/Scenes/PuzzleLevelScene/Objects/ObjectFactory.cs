using System.Threading.Tasks.Dataflow;

namespace Scripts.Game;

public class ObjectFactory()
{
    public IObject CreateEmpty()
    {
        return new EmptyObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreatePlayer()
    {
        return new PlayerObject
        {
            Direction = Directions.South,
        };
    }

    private IObject CreateRock(string textureName, string regionName)
    {
        return new RockObject
        {
            Direction = Directions.South,
            TextureName = textureName,
            RegionName = regionName
        };
    }

    public IObject CreateDefaultRock()
    {
        return CreateRock(TextureNames.Rock, RegionNames.Rock);
    }

    //TODO: implement this
    // public IObject CreatePumpkin()
    // {
    //     return CreateRock(TextureNames.Pumpkin, RegionNames.Pumpkin);
    // }

    public IObject CreateWall()
    {
        return new WallObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateBomb()
    {
        return new BombObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateKey()
    {
        return new KeyObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateClosedDoor()
    {
        return new DoorObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateOpenDoor()
    {
        return new DoorObject
        {
            Direction = Directions.South,
            IsOpen = true
        };
    }

    public IObject CreateTimedBomb()
    {
        return new TimedBombObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateSkeleton()
    {
        return new Skeleton
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateWarlock()
    {
        return new Warlock
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateBlueLizard()
    {
        return new BlueLizard
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateRedLizard()
    {
        return new RedLizard
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateBat()
    {
        return new Bat
        {
          Direction = Directions.South,   
        };
    }

    public IObject CreateFrog()
    {
        return new Frog
        {
          Direction = Directions.South,   
        };
    }

    public IObject CreateZombie()
    {
        return new Zombie
        {
          Direction = Directions.South,   
        };
    }
}