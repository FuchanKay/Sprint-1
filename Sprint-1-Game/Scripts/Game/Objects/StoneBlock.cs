using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
namespace Scripts.GameComponents;

public class StoneBlock : IObject
{
    public Vector2 position {get; set;}
    public bool pushable {get; set;} = false;
    public bool destructible {get; set;} = true;
    public static Texture2D textures {get; set;}

    public StoneBlock(Vector2 position)
    {
        this.position = position;
    }

    public void Update(int dtMs)
    {
        // Update logic for StoneBlock
    }

    public void Draw(SpriteBatch sb)
    {
        float scale = 0.25f;
        Rectangle sourceRectangle = new Rectangle(0, 0, textures.Width, textures.Height);
        sb.Draw(StoneBlock.textures, this.position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
    }

    public void SnapBehavior()
    {
    }

    public void Destroy()
    {
        // Logic to destroy the StoneBlock
    }

    public void Move()
    {
    }
}