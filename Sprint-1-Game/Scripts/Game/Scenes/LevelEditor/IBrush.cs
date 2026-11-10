using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public interface IBrush
{
    BrushTypes Type { get; }
    void Init(GridPointer grid);

    void Update(int dtMs);

    void Draw(SpriteBatch sb);

    void CycleRight();
    void CycleLeft();
}