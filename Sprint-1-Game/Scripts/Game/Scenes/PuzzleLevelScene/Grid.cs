namespace Scripts.Game;

public class Grid
{
    public IObject Object { get; set; }
    public ITile Tile { get; set; }

    public Grid Clone()
    {
        return new Grid
        {
            Object = Object.Clone(),
            Tile = Tile.Clone()
        };
    }

    public bool Equals(Grid grid)
    {
        return Object.Equals(grid.Object) && Tile.Equals(grid.Tile);
    }
}