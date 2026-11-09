using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public interface IBrush
{
    void Init();

    void Update(int dtMs);

    void Draw(SpriteBatch sb);
}