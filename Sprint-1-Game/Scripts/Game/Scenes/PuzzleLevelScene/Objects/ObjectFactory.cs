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

    private IObject CreateRock(string regionName)
    {
        return new RockObject
        {
            Direction = Directions.South,
            RegionName = regionName
        };
    }

    public IObject CreateDefaultRock()
    {
        return CreateRock(RegionNames.Rock);
    }

    public IObject CreatePumpkin()
    {
        return CreateRock(RegionNames.Pumpkin);
    }

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

    public IObject CreateTimedBomb()
    {
        return new TimedBombObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateSkeleton()
    {
        return new SkeletonObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateWarlock()
    {
        return new WarlockObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateBlueLizard()
    {
        return new BlueLizardObject
        {
            Direction = Directions.South,
        };
    }

    public IObject CreateRedLizard()
    {
        return new RedLizardObject
        {
            Direction = Directions.South,
        };
    }
}