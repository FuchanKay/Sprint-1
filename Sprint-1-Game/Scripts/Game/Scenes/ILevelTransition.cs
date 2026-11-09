using Microsoft.Xna.Framework.Graphics;

public interface ILevelTransition 
{
    void StartLevelTransition(int newLevelIndex);
    void Update(int dtMs);
    void Draw(SpriteBatch sb);
    public bool IsActive { get; }
    public int TransitionDurationMs { get; set; }
}