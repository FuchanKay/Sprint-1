namespace Scripts.GameComponents;

public class StaticSprite(ITextureAtlas textureAtlas) : Sprite
{
    public void SetRegion(string name)
    {
        CurrentRegion = textureAtlas.GetRegion(name);
    }
}