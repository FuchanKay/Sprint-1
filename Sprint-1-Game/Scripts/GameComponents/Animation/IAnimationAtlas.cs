using Scripts.Game;

namespace Scripts.GameComponents;

public interface IAnimationAtlas
{
    void AddAnimation(string name, int row, int numFrames, int width, int height);
    void AddAnimation(string name, int row, int numFrames, int width, int height, int delay);
    Animation GetAnimation(string animationName);
    void ClearMapping();
}