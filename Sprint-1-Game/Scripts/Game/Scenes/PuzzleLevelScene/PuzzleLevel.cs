using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class PuzzleLevel(ObjectFactory objectFactory, TileFactory tileFactory, ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory, LevelFileParser levelFileParser)
{
    private readonly List<ILevelEvent> EventQueue = [];
    public LevelContext Context = new()
    {
        GridSpriteWidthPx = 256,
        GridSpriteHeightPx = 256,
        GridScale = 0.25f,
        PuzzleOffsetX = 400,
        PuzzleOffsetY = 50,
        PlayerIsDead = false
    };
    public GridPointer CurrentGridPointer;
    public List<GridPointer> UndoHistory = [];
    private readonly int UndoHistoryMaxLength = 200;
    public readonly SpriteGrid SpriteGrid = new(objectSpriteFactory, tileSpriteFactory);
    private readonly int MaxEventsQueued = 2;
    private ILevelEvent CurrentEvent = new MovePlayerEvent(Directions.East, false, objectFactory);
    private string[] LevelFile;

    public void Init(string levelFilePath)
    {
        UndoHistory.Clear();
        CurrentEvent.SubEventStack.Clear();

        LevelFile = levelFileParser.ParseLevelFile(levelFilePath);
        Context.LevelName = levelFileParser.GetLevelName(LevelFile);
        Context.LevelWidth = levelFileParser.GetLevelWidth(LevelFile);
        Context.LevelHeight = levelFileParser.GetLevelHeight(LevelFile);
        CurrentGridPointer = levelFileParser.CreateGrids(LevelFile);
        SpriteGrid.Init(CurrentGridPointer, Context);
    }

    public void Update(int dtMs)
    {
        TryExecuteEvent();
        SpriteGrid.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        SpriteGrid.Draw(sb);
    }

    public void EnqueueEvent(ILevelEvent levelEvent)
    {
        if (EventQueue.Count < MaxEventsQueued)
        {
            EventQueue.Add(levelEvent);
        }
    }

    private void TryExecuteEvent()
    {
        if (!SpriteGrid.IsIdle() || Context.PlayerIsDead) return;

        var CurrentEventIsFinished = CurrentEvent.SubEventStack.Count == 0;
        if (CurrentEventIsFinished && EventQueue.Count > 0)
        {
            PushCurrentGridPointerClone();

            CurrentEvent = EventQueue[0];
            EventQueue.RemoveAt(0);

            CurrentEvent.Execute(SpriteGrid, CurrentGridPointer, Context);
            CurrentEventIsFinished = CurrentEvent.SubEventStack.Count == 0;
        }

        while (!CurrentEventIsFinished && SpriteGrid.IsIdle())
        {
            var subEvent = CurrentEvent.SubEventStack.Pop();
            subEvent.Execute(SpriteGrid, CurrentGridPointer, Context);
            CurrentEventIsFinished = CurrentEvent.SubEventStack.Count == 0;
        }
    }

    public void Undo()
    {
        var canUndo = UndoHistory.Count > 0;
        if (!canUndo)
        {
            return;
        }
        var top = UndoHistory.Count - 1;
        CurrentGridPointer = UndoHistory[top];
        UndoHistory.RemoveAt(top);
        SpriteGrid.Init(CurrentGridPointer, Context);

        CurrentEvent.SubEventStack.Clear();
        EventQueue.Clear();

        Context.PlayerIsDead = false;
    }

    public void RestartLevel()
    {
        PushCurrentGridPointerClone();

        CurrentGridPointer = levelFileParser.CreateGrids(LevelFile);
        SpriteGrid.Init(CurrentGridPointer, Context);

        CurrentEvent.SubEventStack.Clear();
        EventQueue.Clear();
        Context.PlayerIsDead = false;
    }

    private void PushCurrentGridPointerClone()
    {
        var clone = CurrentGridPointer.Clone();
        UndoHistory.Add(clone);
        if (UndoHistory.Count > UndoHistoryMaxLength) UndoHistory.RemoveAt(0);
    }
}