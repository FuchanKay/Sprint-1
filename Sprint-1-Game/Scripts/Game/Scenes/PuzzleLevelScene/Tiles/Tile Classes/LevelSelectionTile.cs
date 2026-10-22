namespace Scripts.Game;

public class LevelSelectionTile : ITile
{
    public ITile Clone()
    {
        return new LevelSelectionTile
        {

        };
    }
    public TileIds Id => TileIds.LevelSelection;
    public bool IsWalkable => true;
}