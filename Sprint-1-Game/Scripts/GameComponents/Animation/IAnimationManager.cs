using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;
public interface IAnimationManager
{
    void AddNameAndFrames(string name, int numFrames);
    void LoadAnimationSets(Texture2D texture, int width, int height);
    Rectangle UpdateSourceRectangle(string currentSet, GameTime gameTime);
    bool IsFinished(string currentAnimationName);
}