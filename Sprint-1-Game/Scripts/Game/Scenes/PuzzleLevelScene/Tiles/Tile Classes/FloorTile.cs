namespace Scripts.Game;

public class FloorTile : ITile
{
    public TileIds Id => TileIds.Floor;
    public string TextureName { get; set; }
}