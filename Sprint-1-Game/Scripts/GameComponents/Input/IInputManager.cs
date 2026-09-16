namespace Scripts.GameComponents;
public interface IInputManager
{
    void Update();
    bool IsHeld(IInput input);
    bool IsPressed(IInput input);
    bool IsReleased(IInput input);
    //enum button must be type casted to an int
    void MapInput(IInput input, int button);
    void ClearMapping();
}
