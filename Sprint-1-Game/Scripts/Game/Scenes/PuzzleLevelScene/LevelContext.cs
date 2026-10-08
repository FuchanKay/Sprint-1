namespace Scripts.Game;

public class LevelContext
{
    public bool ShouldUpdate { get; set; } = false;
    public int LevelWidth { get; set; }
    public int LevelHeight { get; set; }
    public int GridWidthPx { get; set; }
    public int GridHeightPx { get; set; }
    public int PuzzleOffsetX { get; set; }
    public int PuzzleOffsetY { get; set; }
    public float GridScale { get; set; }
    public bool IsIdle { get; set; }
}