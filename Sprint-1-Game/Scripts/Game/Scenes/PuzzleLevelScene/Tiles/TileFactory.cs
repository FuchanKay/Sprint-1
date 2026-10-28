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
}