using Microsoft.Xna.Framework.Input;

namespace Scripts.Game;

public class KeyStatus(Keys key)
{
    public Keys Key { get; } = key;
    public bool Previous { get; set; } = false;
    public bool Current { get; set; } = false;
}