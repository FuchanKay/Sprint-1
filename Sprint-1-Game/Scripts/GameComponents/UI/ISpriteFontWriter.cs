using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;
public interface ISpriteFontWriter
{
    public void Init(string font, Vector2 pos, string text, Color color)
    {
        
    } 
    public void Update(SpriteFont font, Vector2 pos, string text, Color color)
    {
        
    }

    public void Draw(SpriteBatch sb)
    {
        
    }
}