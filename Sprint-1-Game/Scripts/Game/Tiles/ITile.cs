using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
namespace Scripts.GameComponents;
public interface ITile
{
    void Update(int dtMs);
    void Draw(SpriteBatch sb);
    void SnapBehavior();
}