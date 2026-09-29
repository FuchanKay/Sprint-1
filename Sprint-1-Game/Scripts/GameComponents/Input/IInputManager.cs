namespace Scripts.GameComponents;

/// <summary>
/// Checks whether an input is being held, pressed, or released. Update must be called frequently for it to work 
/// </summary>
public interface IInputManager
{
    int MousePositionX { get; }
    int MousePositionY { get; }
    void Update();
    bool IsHeld(string input);
    bool IsPressed(string input);
    bool IsReleased(string input);
    //enum button must be type casted to an int
    void MapInput(string input, int button);
    void ClearMapping();
}
