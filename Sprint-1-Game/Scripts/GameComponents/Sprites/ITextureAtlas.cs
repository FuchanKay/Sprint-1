using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;

public interface ITextureAtlas
{
    void AddRegion(string name, int column, int row, int width, int height);
    Rectangle GetRegion(string name);
    void AddAnimation(string name, int row, int numFrames, int width, int height);
    void AddAnimation(string name, int row, int numFrames, int width, int height, int delay);
    Animation GetAnimation(string animationName);
}