using Microsoft.Xna.Framework.Graphics;
using Scripts.Game;

namespace Scripts.GameComponents;

public interface IButton
{
    void Init(int x, int y, int w, int h, MouseInputManager input);
    void Update(int dtMs);
    void Draw(SpriteBatch sb);
}