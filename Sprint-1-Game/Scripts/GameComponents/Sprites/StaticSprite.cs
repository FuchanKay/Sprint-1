namespace Scripts.GameComponents;

public class StaticSprite(ITextureAtlas textureAtlas) : Sprite(textureAtlas)
{
    public override void Update(int dtMs) {}
    public override void SetState(string name)
    {
        CurrentRegion = TexAtlas.GetRegion(name);
    }
}