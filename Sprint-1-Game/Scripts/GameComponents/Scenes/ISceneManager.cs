using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

public interface ISceneManager
{
    void SwapScene(string sceneName);

    void Init();

    void Update(int dtMs);

    void Draw(SpriteBatch sb);
}