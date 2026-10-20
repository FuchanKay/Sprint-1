using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

/// <summary>
/// Controls the initialization, update and draw of a scene
/// </summary>
public interface ISceneController
{
    void Init();

    void Update(int dtMs);

    void Draw(SpriteBatch sb);
}