namespace Scripts.Game;

public class TileFactory()
{
    public ITile CreateEmptyTile()
    {
        return new EmptyTile
        {

        };
    }

    public ITile CreateUnsolvedLevelSelectionTile()
    {
        return new LevelSelectionTile{
            
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