using Scripts.GameComponents;

namespace Scripts.Game;

public class GameInput(string name): IInput
{
    private readonly string instanceName = name;
    public string InputName => instanceName;
}