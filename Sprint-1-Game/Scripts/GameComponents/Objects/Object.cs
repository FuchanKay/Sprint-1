using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Scripts.GameComponents;
public abstract class Object : IObject
{
    public Vector2 position { get; set; }
    protected bool pushable { get; set; }
    protected bool destructible { get; set; }
    public bool isDestroyed { get; set; }
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
        if(isDestroyed) return;
        // TODO: Play destroy animation and sound effect
        isDestroyed = true;
    }
    public void MoveUp()
    {
        position = new Vector2(position.X, position.Y - 20);
    }

    public void MoveDown()
    {
        position = new Vector2(position.X, position.Y + 20);
    }

    public void MoveLeft()
    {
        position = new Vector2(position.X - 20, position.Y);
    }

    public void MoveRight()
    {
        position = new Vector2(position.X + 20, position.Y);
    }
    
}