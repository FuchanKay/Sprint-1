using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

public abstract class Sprite(ITextureAtlas textureAtlas) : ISprite
{
    protected ITextureAtlas TexAtlas => textureAtlas;
    public Texture2D Texture { get; set; }
    public Vector2 Position { get; set; }
    public Rectangle CurrentRegion { get; set; }
    public Color Color { get; set; } = Color.White;
    public float Rotation { get; set; } = 0f;
    public Vector2 Origin => new Vector2(CurrentRegion.Width / 2, CurrentRegion.Height / 2);
    public float Scale { get; set; } = 1.0f;
    public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    public float LayerDepth { get; set; } = 0f;
    public abstract bool IsFinished { get; protected set; }

    public abstract void Update(int dtMs);
    public abstract void SetState(string name);

    public void Draw(SpriteBatch sb)
    {
        sb.Draw(Texture, Position, CurrentRegion, Color, Rotation, Origin, Scale, Effects, LayerDepth);
    }

    public StaticSprite ConvertToStatic(string name)
    {
        StaticSprite staticSprite = new(textureAtlas);
        staticSprite.SetState(name);
        staticSprite.Position = Position;
        staticSprite.Texture = Texture;
        return staticSprite;
    }

    public AnimatedSprite ConvertToAnimated(string name)
    {
        AnimatedSprite animatedSprite = new(textureAtlas);
        animatedSprite.SetState(name);
        animatedSprite.Position = Position;
        animatedSprite.Texture = Texture;
        return animatedSprite;
    }
}