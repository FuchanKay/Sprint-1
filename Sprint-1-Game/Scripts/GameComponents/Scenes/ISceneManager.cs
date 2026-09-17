using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

/// <summary>
/// Updates and draws the chosen scene. When swapping scenes, some context should be passed. 
/// </summary>
public interface ISceneManager
{
    void SwapScene(string sceneName);

    void Init();

    void Update(int dtMs);

    void Draw(SpriteBatch sb);

    void ExitGame();
}