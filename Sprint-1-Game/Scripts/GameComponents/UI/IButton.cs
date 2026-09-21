using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

public interface IButton
{
    void Update(int dtMs);

    void Draw(SpriteBatch sb);

    void OnClick();
}