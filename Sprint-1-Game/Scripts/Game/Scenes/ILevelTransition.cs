using Microsoft.Xna.Framework.Graphics;

public interface ILevelTransition 
{
    public void StartLevelTransition(int newLevelIndex);
    public void Update(int dtMs);
    public void Draw(SpriteBatch sb);
    public bool IsActive { get; }
    public int TransitionDurationMs { get; set; }
}