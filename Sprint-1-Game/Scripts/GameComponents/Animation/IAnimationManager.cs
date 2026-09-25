using Microsoft.Xna.Framework;

namespace Scripts.GameComponents;
public interface IAnimationManager
{
    void AddAnimation(string name, int numFrames, int rowNumber);
    void LoadAnimations(int width, int height);
    Rectangle UpdateSourceRectangle(string currentSet, int dtMs);
    bool IsFinished();
}