using System;
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
        PlayerIsDead = false,
        LevelIsCompleted = false
    };
    public GridPointer CurrentGridPointer;
    public InventoryManager CurrentInventoryManager;
    public LevelStateHistory LevelStateHistory;
    public List<LevelStateHistory> UndoHistory = [];
    private readonly int UndoHistoryMaxLength = 200;
    public readonly SpriteGrid SpriteGrid = new(objectSpriteFactory, tileSpriteFactory);
    private readonly int MaxEventsQueued = 2;
    private ILevelEvent CurrentEvent = new MovePlayerEvent(Directions.East, false, objectFactory, tileFactory);
    private string[] LevelFile;

    public void Init(int levelIndex)
    {
        UndoHistory.Clear();
        CurrentEvent.SubEventStack.Clear();

        var levelFileName = FileNames.LevelFileNames[levelIndex];
        LevelFile = levelFileParser.ParseLevelFile(levelFileName);
        Context.LevelName = levelFileParser.GetLevelName(LevelFile);
        Context.LevelWidth = levelFileParser.GetLevelWidth(LevelFile);
        Context.LevelHeight = levelFileParser.GetLevelHeight(LevelFile);
        CalculateOffsets();
        CurrentGridPointer = levelFileParser.CreateGrids(LevelFile);
        SpriteGrid.Init(CurrentGridPointer, Context);
        CurrentInventoryManager = new InventoryManager(objectFactory);
        LevelStateHistory = new LevelStateHistory(CurrentGridPointer, CurrentInventoryManager);
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
            PushCurrentLevelStateHistory();

            CurrentEvent = EventQueue[0];
            EventQueue.RemoveAt(0);

            CurrentEvent.Execute(SpriteGrid, LevelStateHistory.GridPointer, LevelStateHistory.InventoryManager, Context);
            CurrentEventIsFinished = CurrentEvent.SubEventStack.Count == 0;
        }

        var maxSubEventsToExecute = 500;
        while (!CurrentEventIsFinished && SpriteGrid.IsIdle() && maxSubEventsToExecute > 0)
        {
            var subEvent = CurrentEvent.SubEventStack.Pop();
            subEvent.Execute(SpriteGrid, LevelStateHistory.GridPointer, LevelStateHistory.InventoryManager, Context);
            CurrentEventIsFinished = CurrentEvent.SubEventStack.Count == 0;
            maxSubEventsToExecute--;
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
        LevelStateHistory = UndoHistory[top];
        UndoHistory.RemoveAt(top);
        SpriteGrid.Init(LevelStateHistory.GridPointer, Context);
        CurrentEvent.SubEventStack.Clear();
        EventQueue.Clear();

        Context.PlayerIsDead = false;
    }

    public void RestartLevel()
    {
        PushCurrentLevelStateHistory();

        LevelStateHistory.GridPointer = levelFileParser.CreateGrids(LevelFile);
        LevelStateHistory.InventoryManager = new InventoryManager(objectFactory);
        SpriteGrid.Init(LevelStateHistory.GridPointer, Context);

        CurrentEvent.SubEventStack.Clear();
        EventQueue.Clear();
        Context.PlayerIsDead = false;
    }

    private void PushCurrentLevelStateHistory()
    {
        var clone = LevelStateHistory.Clone();
        UndoHistory.Add(clone);
        if (UndoHistory.Count > UndoHistoryMaxLength) UndoHistory.RemoveAt(0);
    }

    private void CalculateOffsets()
    {
        const int midX = 1280 / 2;
        const int midY = 720 / 2;
        Context.PuzzleOffsetX = (int) (midX - Context.LevelWidth / 2 * Context.GridSpriteWidthPx * Context.GridScale);
        Context.PuzzleOffsetY = (int) (midY - Context.LevelHeight / 2 * Context.GridSpriteHeightPx * Context.GridScale);
    }
}