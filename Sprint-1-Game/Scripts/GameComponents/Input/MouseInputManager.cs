using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Scripts.GameComponents;

public class MouseInputManager : IInputManager
{
    private readonly Dictionary<Inputs, MouseButtonStatus> InputButtonStateMap;
    public MouseInputManager()
    {
        InputButtonStateMap = [];
    }

    public void Update()
    {
        foreach (var inputButtonState in InputButtonStateMap)
        {
            var buttonStatus = inputButtonState.Value;
            var mouse = Mouse.GetState();
            
            buttonStatus.Previous = buttonStatus.Current;
            switch (buttonStatus.Button)
            {
                case MouseButtons.Left:
                    buttonStatus.Current = mouse.LeftButton == ButtonState.Pressed;
                    break;
                case MouseButtons.Right:
                    buttonStatus.Current = mouse.RightButton == ButtonState.Pressed;
                    break;
                case MouseButtons.Middle:
                    buttonStatus.Current = mouse.MiddleButton == ButtonState.Pressed;
                    break;
                default: 
                    break;
            }
        }
    }
    public int X()
    {
        return Mouse.GetState().Position.X;
    }

    public int Y()
    {
        return Mouse.GetState().Position.Y;
    }

    public bool IsHeld(Inputs input)
    {
        if (InputButtonStateMap.TryGetValue(input, out var buttonState))
        {
            return buttonState.Current;
        }
        return false;
    }

    public bool IsPressed(Inputs input)
    {
        if (InputButtonStateMap.TryGetValue(input, out var buttonState))
        {
            return !buttonState.Previous && buttonState.Current;
        }
        return false;   
    }

    public bool IsReleased(Inputs input)
    {
        if (InputButtonStateMap.TryGetValue(input, out var buttonState))
        {
            return buttonState.Previous && !buttonState.Current;
        }
        return false;
    }

    public void MapInput(Inputs input, int button)
    {
        var buttonEnum = (MouseButtons) button;
        var previouslyMapped = InputButtonStateMap.TryAdd(input, new MouseButtonStatus(buttonEnum));
        if (!previouslyMapped)
        {
            InputButtonStateMap[input] = new MouseButtonStatus(buttonEnum);
        }
    }

    public void ClearMapping()
    {
        InputButtonStateMap.Clear();
    }
}