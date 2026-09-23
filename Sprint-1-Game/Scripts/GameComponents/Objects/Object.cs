using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Scripts.GameComponents;
public abstract class Object : IObject
{
    protected Vector2 position { get; set; }
    protected bool pushable { get; set; }
    protected bool destructible { get; set; }
    protected abstract Texture2D Texture { get; }
    public abstract void SnapBehavior();
    public void Update(int dtMs)
    {
        // Update logic for the object (Checking for collisions, etc.)
    }
    public void Draw(SpriteBatch sb)
    {
        // Draw logic for the object (Rendering the object on the screen)
        Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
        float scale = 0.25f;
        float rotation = 0f;
        Color color = Color.White;
        Vector2 origin = Vector2.Zero;
        float layerDepth = 0f;
        SpriteEffects effects = SpriteEffects.None;
        sb.Draw(
            Texture, 
            position, 
            sourceRectangle, 
            color, 
            rotation, 
            origin, 
            scale, 
            effects, 
            layerDepth
        );
    }
    public void Destroy()
    {
        // Logic to destroy the object (Removing it from the game world)
    }
    public void Move()
    {
        // Logic to move the object (Changing its position)
    }
    
}