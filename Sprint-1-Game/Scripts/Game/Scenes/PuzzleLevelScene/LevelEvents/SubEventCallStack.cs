using System.Collections.Generic;

namespace Scripts.Game;

public class SubEventCallStack
{
    private List<ILevelEvent> SubEventStack = [];

    public void Push(ILevelEvent subEvent)
    {
        SubEventStack.Add(subEvent);
    }

    public ILevelEvent Pop()
    {
        var end = SubEventStack.Count - 1;
        var topEvent = SubEventStack[end];
        SubEventStack.RemoveAt(end);
        return topEvent;
    }
}
