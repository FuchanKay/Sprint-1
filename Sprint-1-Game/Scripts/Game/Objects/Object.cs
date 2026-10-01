using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Scripts.GameComponents;

public abstract class Object : IObject
{
    protected IAudioManager AudioManager { get; set; }
    protected abstract bool Pushable { get; }
    protected abstract bool Destructible { get; }
    protected bool IsDestroyed { get; set; } = false;
    protected const float MoveSpeed = 1.0f;
    protected float XSpeed = 0.0f;
    protected float YSpeed = 0.0f;
    protected AnimatedSprite Sprite;
    private bool destroyPlayed = false;
    protected abstract Texture2D Texture { get; }
    protected abstract string InitialState { get; }
    public Object(Vector2 pos, IAudioManager am, ITextureAtlas textureAtlas)
    {
        Sprite = new AnimatedSprite(textureAtlas)
        {
            Position = pos,
            Texture = Texture
        };
        Sprite.SetState(InitialState);
        AudioManager = am;
    }
    public abstract void SnapBehavior();
    public void Update(int dtMs)
    {
        if (Pushable && !IsDestroyed)
        {
            Sprite.Position = new Vector2(Sprite.Position.X + XSpeed * dtMs, Sprite.Position.Y + YSpeed * dtMs);
        }
        XSpeed = YSpeed = 0;
        Sprite.Update(dtMs);
    }
    public void Draw(SpriteBatch sb)
    {
        if(IsDestroyed && Sprite.IsFinished) destroyPlayed = true;
        if (destroyPlayed) return;
        Sprite.Texture ??= Texture;
        Sprite.Draw(sb);
    }
    public void Destroy()
    {
        if(IsDestroyed || !Destructible) return;
        Sprite = Sprite.ConvertToAnimated("Destroy");
        AudioManager.PlaySound("destroy");
        IsDestroyed = true;
    }
    public void MoveUp() => YSpeed = MoveSpeed * -1;
    public void MoveDown() => YSpeed = MoveSpeed;
    public void MoveLeft() => XSpeed = MoveSpeed * -1;
    public void MoveRight() => XSpeed = MoveSpeed;
}