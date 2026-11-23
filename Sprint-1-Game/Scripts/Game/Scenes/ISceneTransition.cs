using Microsoft.Xna.Framework.Graphics;

public interface ISceneTransition 
{
    public void StartSceneTransition(string sceneName);
    public void Update(int dtMs);
    public void Draw(SpriteBatch sb);
    public bool IsActive { get; }
    public int TransitionDurationMs { get; set; }
}