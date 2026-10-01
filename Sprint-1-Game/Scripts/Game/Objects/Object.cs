using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Scripts.GameComponents;

public abstract class Object(Vector2 pos, IAudioManager am, ITextureAtlas textureAtlas) : IObject
{
    protected string TextureName { get; }
    protected IAudioManager AudioManager { get; } = am;
    public Vector2 Position { get; set; } = pos;
    protected abstract bool Pushable { get; }
    protected abstract bool Destructible { get; }
    protected bool IsDestroyed { get; set; } = false;
    protected const float MoveSpeed = 5.0f;
    protected float XSpeed = 0.0f;
    protected float YSpeed = 0.0f;
    // Sprite Properties (Potentially moved to a Sprite class in the future)

    ISprite Sprite { get; set; }

    // protected abstract Texture2D Texture { get; }
    // protected virtual Rectangle SourceRectangle { get; }
    // protected virtual float Scale { get; } = 1.0f;
    // protected virtual float Rotation { get; } = 0f;
    // protected virtual Color Color { get; } = Color.White;
    // protected virtual Vector2 Origin { get; } = Vector2.Zero;
    // protected virtual float LayerDepth { get; } = 0f;
    // protected virtual SpriteEffects Effects { get; } = SpriteEffects.None;
    public abstract void SnapBehavior();

    public void Init()
    {
        Sprite = new AnimatedSprite(textureAtlas);
        Sprite.SetState("SproutWalkRight");
        Sprite.Position = new Vector2(800, 500);
        Sprite.Texture = textureAtlas.GetTexture(TextureName);
    }

    public void Update(int dtMs)
    {
        if (Pushable)
        {
            Position = new Vector2(Position.X + XSpeed * dtMs, Position.Y + YSpeed * dtMs);
            XSpeed = 0.0f; YSpeed = 0.0f;
        }
    }
    public void Draw(SpriteBatch sb)
    {
        if (IsDestroyed) return;
        Sprite.Draw(sb);
    }
    public void Destroy()
    {
        if (IsDestroyed || !Destructible) return;
        // TODO: Play destroy animation and sound effect
        IsDestroyed = true;
    }
    public void MoveUp()
    {
        YSpeed = MoveSpeed * -1;
    }

    public void MoveDown()
    {
        YSpeed = MoveSpeed;
    }

    public void MoveLeft()
    {
        XSpeed = MoveSpeed * -1;
    }

    public void MoveRight()
    {
        XSpeed = MoveSpeed;
    }

}