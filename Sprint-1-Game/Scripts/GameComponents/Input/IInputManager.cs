namespace Scripts.GameComponents;
public interface IInputManager
{
    void Update();
    bool IsHeld(string input);
    bool IsPressed(string input);
    bool IsReleased(string input);
    //enum button must be type casted to an int
    void MapInput(string input, int button);
    void ClearMapping();
}
