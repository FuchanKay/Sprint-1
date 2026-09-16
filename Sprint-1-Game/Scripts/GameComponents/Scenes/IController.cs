using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

public interface IController
{
    void Init();

    void Update(int dtMs);

    void Draw(SpriteBatch sb);
}