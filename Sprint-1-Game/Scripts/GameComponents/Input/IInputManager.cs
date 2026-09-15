namespace Scripts.GameComponents;
public interface IInputManager
{
    void Update();
    bool IsHeld(Inputs input);
    bool IsPressed(Inputs input);
    bool IsReleased(Inputs input);
    //enum button must be type casted to an int
    void MapInput(Inputs input, int button);
    void ClearMapping();
}
