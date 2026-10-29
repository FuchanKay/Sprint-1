namespace Scripts.Game;

public class TileFactory()
{
    public static ITile CreateEmptyTile()
    {
        return new EmptyTile
        {

        };
    }
    private static ITile CreateFloorTile(string regionName)
    {
        return new FloorTile
        {
            RegionName = regionName
        };
    }

    public static ITile CreateDirtTile()
    {
        return CreateFloorTile(RegionNames.DirtTile);
    }

    public static ITile CreateGrassTile()
    {
        return CreateFloorTile(RegionNames.GrassTile);
    }

    public static ITile CreateBrickTile()
    {
        return CreateFloorTile(RegionNames.BrickTile);
    }

    public static ITile CreateMossyBrickTile()
    {
        return CreateFloorTile(RegionNames.MossyBrickTile);
    }

    public static ITile CreatePitBoulderTile()
    {
        return CreateFloorTile(RegionNames.BoulderPitTile);
    }

    public static ITile CreateWaterBoulderTile()
    {
        return CreateFloorTile(RegionNames.BoulderWaterTile);
    }

    public static ITile CreatePitPumpkinTile()
    {
        return CreateFloorTile(RegionNames.PumpkinPitTile);
    }

    public static ITile CreateWaterPumpkinTile()
    {
        return CreateFloorTile(RegionNames.PumpkinWaterTile);
    }

    public static ITile CreateVineStartTile()
    {
        return CreateFloorTile(RegionNames.VineStartTile);
    }

    public static ITile CreateVineTile()
    {
        return CreateFloorTile(RegionNames.VineTile);
    }
    public static ITile CreatePitTile()
    {
        return new PitTile
        {

        };
    }

    public static ITile CreateWaterTile()
    {
        return new WaterTile
        {
        };
    }

    public static ITile CreateLavaTile()
    {
        return new LavaTile
        {
        };
    }

    public static ITile CreateBugTile()
    {
        return new BugTile
        {

        };
    }

    public static ITile CreateCCBugTile()
    {
        return new CCBugTile
        {

        };
    }

    public static ITile CreateVineSeedTile()
    {
        return new VineSeedTile
        {
            
        };
    }
}