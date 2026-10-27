namespace Scripts.Game;

public class LevelContext
{
    public string LevelName { get; set; }
    public int LevelWidth { get; set; }
    public int LevelHeight { get; set; }
    public int GridSpriteWidthPx { get; set; }
    public int GridSpriteHeightPx { get; set; }
    public int PuzzleOffsetX { get; set; }
    public int PuzzleOffsetY { get; set; }
    public float GridScale { get; set; }
    public bool PlayerIsDead { get; set; }
    public bool LevelIsCompleted { get; set; }
}