using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public interface IBrush
{
    BrushTypes Type { get; }
    void Init();

    void Update(int dtMs);

    void Draw(SpriteBatch sb);
}