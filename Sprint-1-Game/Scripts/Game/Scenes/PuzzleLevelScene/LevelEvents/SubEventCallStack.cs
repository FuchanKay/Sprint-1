using System.Collections.Generic;

namespace Scripts.Game;

public class SubEventCallStack
{
    private List<ILevelSubEvent> SubEventStack = [];

    public void Push(ILevelSubEvent subEvent)
    {
        SubEventStack.Add(subEvent);
    }

    public ILevelSubEvent Pop()
    {
        var end = SubEventStack.Count - 1;
        var topEvent = SubEventStack[end];
        SubEventStack.RemoveAt(end);
        return topEvent;
    }

    public int Count()
    {
        return SubEventStack.Count;
    }
}
