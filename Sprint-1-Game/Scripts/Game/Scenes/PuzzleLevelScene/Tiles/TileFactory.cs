namespace Scripts.Game;

public class TileFactory()
{
    public ITile CreateEmptyTile()
    {
        return new EmptyTile
        {

        };
    }

    public ITile CreateBrickTile()
    {
        return new BrickTile
        {
        };
    }

    public ITile CreateGrassTile()
    {
        return new GrassTile
        {
        };
    }

    public ITile CreateWaterTile()
    {
        return new WaterTile
        {
        };
    }
}