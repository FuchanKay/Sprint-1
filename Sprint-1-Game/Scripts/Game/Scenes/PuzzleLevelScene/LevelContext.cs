namespace Scripts.Game;

public class LevelContext
{
    public bool ShouldUpdate { get; set; } = false;
    public int LevelWidth { get; set; }
    public int LevelHeight { get; set; }
    public bool IsIdle { get; set; }
}