using Microsoft.Xna.Framework;
using Scripts.Game;

namespace GUMProject.Screens
{
    public interface ILevelScreen
    {
        void FetchLevel(PuzzleLevel puzzleLevel);
    }
}