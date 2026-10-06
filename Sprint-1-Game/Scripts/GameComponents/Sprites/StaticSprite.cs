using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;

public class StaticSprite(ITextureAtlas textureAtlas) : Sprite(textureAtlas)
{
    public override void Update(int dtMs) {}
    public override bool IsFinished { get; protected set; } = true;
    public override void SetState(string name)
    {
        CurrentRegion = TexAtlas.GetRegion(name);
    }

    public override void SetTargetPosition(Vector2 position)
    {
        TargetPosition = position;
        CurrentPosition = TargetPosition;
    }
}