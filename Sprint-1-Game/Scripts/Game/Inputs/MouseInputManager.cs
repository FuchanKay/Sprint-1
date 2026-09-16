using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Scripts.GameComponents;

namespace Scripts.Game;

public class MouseInputManager : IInputManager
{
    private readonly Dictionary<string, MouseButtonStatus> InputButtonStateMap;
    public int X => Mouse.GetState().Position.X;
    public int Y => Mouse.GetState().Position.Y;
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

    public bool IsHeld(string input)
    {
        if (InputButtonStateMap.TryGetValue(input, out var buttonState))
        {
            return buttonState.Current;
        }
        return false;
    }

    public bool IsPressed(string input)
    {
        if (InputButtonStateMap.TryGetValue(input, out var buttonState))
        {
            return !buttonState.Previous && buttonState.Current;
        }
        return false;   
    }

    public bool IsReleased(string input)
    {
        if (InputButtonStateMap.TryGetValue(input, out var buttonState))
        {
            return buttonState.Previous && !buttonState.Current;
        }
        return false;
    }

    public void MapInput(string input, int button)
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