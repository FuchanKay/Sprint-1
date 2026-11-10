namespace Scripts.Game;

public class LevelContext
{
    public string LevelName { get; set; }
    public int LevelWidth { get; set; }
    public int LevelHeight { get; set; }
    public int RawSpriteWidthPx { get; set; }
    public int RawSpriteHeightPx { get; set; }
    public int GridWidth => (int)(RawSpriteWidthPx * GridScale);
    public int GridHeight => (int)(RawSpriteHeightPx * GridScale);
    public int PuzzleOffsetX { get; set; }
    public int PuzzleOffsetY { get; set; }
    public float GridScale { get; set; }
    public bool PlayerIsDead { get; set; }
    public bool LevelIsCompleted { get; set; }
}