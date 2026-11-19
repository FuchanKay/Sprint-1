using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public interface IBrush
{
    void Init(GridPointer grid);
    void Init(GridPointer grid, Coordinate coord);
    void Update(int dtMs);
    void Draw(SpriteBatch sb);
    void CycleRight();
    void CycleLeft();
}