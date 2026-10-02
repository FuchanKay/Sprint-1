using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Scripts.Game;

public class PuzzleLevel
{
    private List<ILevelEvent> EventQueue = [];
    private readonly int MaxEventsQueued = 3; 
    
    private readonly int LevelWidth = 10;
    private readonly int LevelHeight = 10;

    public PuzzleLevel()
    {
        
    }

    public void Init()
    {
        
    }

    public void Draw()
    {

    }   

    public void EnqueueEvent(ILevelEvent levelEvent)
    {
        if (EventQueue.Count < MaxEventsQueued)
        {
            EventQueue.Add(levelEvent);
        }
    }

    public void ExecuteEvent()
    {
        var first = EventQueue[0];
        EventQueue.RemoveAt(0);
        // first.execute();
    }
}