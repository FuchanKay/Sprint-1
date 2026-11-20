using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class PuzzleLevel(ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory, EffectSpriteFactory effectSpriteFactory, LevelFileParser levelFileParser, IAudioManager audioManager, GameLayoutManager gameLayoutManager)
{
    public readonly List<ILevelEvent> EventQueue = [];
    public LevelContext Context;
    public LevelState PreviousLevelState;
    public LevelState CurrentLevelState;
    public IAudioManager AudioManager = audioManager;
    public List<LevelState> UndoHistory = [];
    private readonly int UndoHistoryMaxLength = 200;
    public readonly SpriteGrid SpriteGrid = new(objectSpriteFactory, tileSpriteFactory, effectSpriteFactory);
    private readonly int MaxEventsQueued = 2;
    public ILevelEvent CurrentEvent = new MovePlayerEvent(Directions.East, false);
    private string[] LevelFile;
    private GridPointer LevelStart;

    public void Init(int levelIndex)
    {
        UndoHistory.Clear();
        EventQueue.Clear();
        CurrentEvent.SubEventStack.Clear();

        Context = new()
        {
            RawSpriteWidthPx = 256,
            RawSpriteHeightPx = 256,
            GridScale = 0.25f,
            PuzzleOffsetX = 400,
            PuzzleOffsetY = 50,
            PlayerIsDead = false,
            LevelIsCompleted = false,
            LevelEditorMode = false
        };
        var levelFileName = FileNames.LevelFileNames[levelIndex];
        LevelFile = LevelFileParser.ParseLevelFile(levelFileName);
        Context.LevelName = levelFileParser.GetLevelName(LevelFile);
        Context.LevelWidth = levelFileParser.GetLevelWidth(LevelFile);
        Context.LevelHeight = levelFileParser.GetLevelHeight(LevelFile);

        gameLayoutManager.LevelLayout(Context);
        CurrentLevelState = new LevelState(levelFileParser.CreateGrids(LevelFile), new InventoryManager(), Context);
        SpriteGrid.Init(CurrentLevelState.GridPointer, Context);
    }

    public void Init(GridPointer grid, LevelContext context)
    {
        LevelStart = grid.Clone();
        UndoHistory.Clear();
        CurrentEvent.SubEventStack.Clear();
        Context = context;
        Context.LevelEditorMode = true;
        CurrentLevelState = new LevelState(grid, new InventoryManager(), context);
        SpriteGrid.Init(grid, Context);
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
        if (CurrentEventIsFinished) CurrentLevelState.GridPointer.ObjectsJustMoved.Clear();
        if (CurrentEventIsFinished && EventQueue.Count > 0)
        {
            PushCurrentLevelStateHistory();

            CurrentEvent = EventQueue[0];
            EventQueue.RemoveAt(0);

            CurrentEvent.Execute(SpriteGrid, CurrentLevelState.GridPointer, CurrentLevelState.InventoryManager, Context, AudioManager, UndoHistory);
            CurrentEventIsFinished = CurrentEvent.SubEventStack.Count == 0;
        }

        var maxSubEventsToExecute = 500;
        while (!CurrentEventIsFinished && SpriteGrid.IsIdle() && maxSubEventsToExecute > 0)
        {
            var subEvent = CurrentEvent.SubEventStack.Pop();
            subEvent.Execute(SpriteGrid, CurrentLevelState.GridPointer, CurrentLevelState.InventoryManager, Context, AudioManager);
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
        CurrentLevelState = UndoHistory[top];
        UndoHistory.RemoveAt(top);
        SpriteGrid.Init(CurrentLevelState.GridPointer, Context);
        CurrentEvent.SubEventStack.Clear();
        EventQueue.Clear();

        Context.PlayerIsDead = false;
    }

    public void RestartLevel()
    {
        PushCurrentLevelStateHistory();

        if (!Context.LevelEditorMode)
        {
            CurrentLevelState.GridPointer = levelFileParser.CreateGrids(LevelFile);
        }
        else
        {
            CurrentLevelState.GridPointer = LevelStart.Clone();
        }
        CurrentLevelState.InventoryManager = new InventoryManager();
        SpriteGrid.Init(CurrentLevelState.GridPointer, Context);

        CurrentEvent.SubEventStack.Clear();
        EventQueue.Clear();
        Context.PlayerIsDead = false;
    }

    public void ToggleObjectHide()
    {
        SpriteGrid.ToggleObjectLayerHide();
    }

    private void PushCurrentLevelStateHistory()
    {
        var clone = CurrentLevelState.Clone();
        if (UndoHistory.Count == 0)
        {
            UndoHistory.Add(clone);
        }
        else if (UndoHistory.Count > 0)
        {
            var last = UndoHistory[UndoHistory.Count - 1];
            if (!CurrentLevelState.Equals(last))
            {
                UndoHistory.Add(CurrentLevelState.Clone());
            }
        }
        if (UndoHistory.Count > UndoHistoryMaxLength) UndoHistory.RemoveAt(0);
    }
}