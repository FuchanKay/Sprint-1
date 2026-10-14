namespace Scripts.Game;

public class TileFactory()
{
    public ITile CreateEmptyTile()
    {
        return new EmptyTile
        {

        };
    }
    private ITile CreateFloorTile(string textureName)
    {
        return new FloorTile
        {
            TextureName = textureName
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