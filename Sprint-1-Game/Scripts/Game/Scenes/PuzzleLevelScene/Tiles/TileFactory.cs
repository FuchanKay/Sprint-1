namespace Scripts.Game;

public class TileFactory()
{
    public ITile CreateEmptyTile()
    {
        return new EmptyTile
        {

        };
    }

    public ITile CreateDirtTile()
    {
        return new DirtTile
        {
            
        };
    }

    public ITile CreateGrassTile()
    {
        return new GrassTile
        {
            
        };
    }

    public ITile CreateBrickTile()
    {
        return new BrickTile
        {
        };
    }

    public ITile CreateMossyBrickTile()
    {
        return new MossyBrickTile
        {
            
        };
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