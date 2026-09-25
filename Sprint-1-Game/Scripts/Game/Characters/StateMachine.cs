namespace Scripts.Game;

public class StateMachine
{
    private enum State{Left, Right, Up, Down};
    private State currState = State.Right;
    public void ChangeDirection()
    {
        switch (currState)
        {
            case State.Left:
                currState = State.Right;
                break;
            case State.Right:
                currState = State.Left;
                break;
            case State.Up:
                currState = State.Down;
                break;
            case State.Down:
                currState = State.Up;
                break;
        }
    }
}