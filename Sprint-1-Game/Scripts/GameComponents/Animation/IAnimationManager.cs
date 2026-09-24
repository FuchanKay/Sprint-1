using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;
public interface IAnimationManager
{
    void AddAnimationSet(string name, int numFrames, int rowNumber);
    void LoadAnimationSets(Texture2D texture, int width, int height);
    Rectangle UpdateSourceRectangle(string currentSet, GameTime gameTime);
    bool IsFinished(string animationName);
}