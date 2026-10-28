namespace Scripts.Game;

public class TileFactory()
{
    public ITile CreateEmptyTile()
    {
        return new EmptyTile
        {

        };
    }
    private ITile CreateFloorTile(string regionName)
    {
        return new FloorTile
        {
            RegionName = regionName
        };
    }

    public ITile CreateDirtTile()
    {
        return CreateFloorTile(RegionNames.DirtTile);
    }

    public ITile CreateGrassTile()
    {
        return CreateFloorTile(RegionNames.GrassTile);
    }

    public ITile CreateBrickTile()
    {
        return CreateFloorTile(RegionNames.BrickTile);
    }

    public ITile CreateMossyBrickTile()
    {
        return CreateFloorTile(RegionNames.MossyBrickTile);
    }

    public ITile CreatePitBoulderTile()
    {
        return CreateFloorTile(RegionNames.BoulderPitTile);
    }

    public ITile CreateWaterBoulderTile()
    {
        return CreateFloorTile(RegionNames.BoulderWaterTile);
    }

    public ITile CreatePitPumpkinTile()
    {
        return CreateFloorTile(RegionNames.PumpkinPitTile);
    }

    public ITile CreateWaterPumpkinTile()
    {
        return CreateFloorTile(RegionNames.PumpkinWaterTile);
    }

    public ITile CreatePitTile()
    {
        return new PitTile
        {

        };
    }

    public ITile CreateWaterTile()
    {
        return new WaterTile
        {
        };
    }

    public ITile CreateLavaTile()
    {
        return new LavaTile
        {
        };
    }

    public ITile CreateBugTile()
    {
        return new BugTile
        {

        };
    }

    public ITile CreateCCBugTile()
    {
        return new CCBugTile
        {

        };
    }
}