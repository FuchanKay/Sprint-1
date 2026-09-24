using System.Diagnostics.Contracts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Scripts.GameComponents;
public abstract class Object : IObject
{
    public Vector2 position { get; set; }
    protected abstract bool pushable { get; }
    protected abstract bool destructible { get; }
    public bool isDestroyed { get; set; }
    protected abstract Texture2D Texture { get; }
    private float xSpeed = 0.0f;
    private float ySpeed = 0.0f;
    public abstract void SnapBehavior();
    public void Init(Vector2 position)
    {
        this.position = position;
        this.isDestroyed = false;
    }
    public void Update(int dtMs)
    {
        position = new Vector2(position.X + xSpeed * dtMs, position.Y + ySpeed * dtMs);
        xSpeed = 0.0f; ySpeed = 0.0f;   
    }
    public void Draw(SpriteBatch sb)
    {
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
        if(isDestroyed || !destructible) return;
        // TODO: Play destroy animation and sound effect
        isDestroyed = true;
    }
    public void MoveUp()
    {
        ySpeed = -5.0f;
    }

    public void MoveDown()
    {
        ySpeed = 5.0f;
    }

    public void MoveLeft()
    {
        xSpeed = -5.0f;
    }

    public void MoveRight()
    {
        xSpeed = 5.0f;
    }
    
}