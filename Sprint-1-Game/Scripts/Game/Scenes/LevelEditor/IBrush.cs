using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public interface IBrush
{
    BrushTypes Type { get; }
    bool ChangesMade { get; set; }
    void Init();

    void Update(int dtMs);

    void Draw(SpriteBatch sb);

    void CycleRight();
    void CycleLeft();
}