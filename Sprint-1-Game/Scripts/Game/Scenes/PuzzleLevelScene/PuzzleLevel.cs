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
        IsIdle = true,
        PlayerIsDead = false
    };
    public GridPointer CurrentGridPointer;
    public List<GridPointer> UndoHistory = [];
    private readonly int UndoHistoryMaxLength = 200;
    public readonly SpriteGrid SpriteGrid = new(objectSpriteFactory, tileSpriteFactory);
    private readonly int MaxEventsQueued = 2;
    public bool IsIdle = true;
    private ILevelEvent CurrentEvent = new MovePlayerEvent(Directions.East, false, objectFactory);
    private string[] LevelFile;

    public void Init(string levelFilePath)
    {
        LevelFile = levelFileParser.ParseLevelFile(levelFilePath);
        Context.LevelName = levelFileParser.GetLevelName(LevelFile);
        Context.LevelWidth = levelFileParser.GetLevelWidth(LevelFile);
        Context.LevelHeight = levelFileParser.GetLevelHeight(LevelFile);
        CurrentGridPointer = levelFileParser.CreateGrids(LevelFile);
        SpriteGrid.Init(CurrentGridPointer, Context);
    }

    public void Update(int dtMs)
    {
        SpriteGrid.Update(dtMs);
        Context.IsIdle = SpriteGrid.IsIdle;
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

    public bool TryExecuteEvent()
    {
        if (CurrentEvent == null) return false;
        if (!Context.IsIdle) return false;
        if(Context.PlayerIsDead) return false;

        var CurrentEventIsFinished = CurrentEvent.SubEventStack.Count == 0;
        if (CurrentEventIsFinished && EventQueue.Count > 0)
        {
            PushCurrentGridPointerClone();

            CurrentEvent = EventQueue[0];
            EventQueue.RemoveAt(0);

            CurrentEvent.Execute(SpriteGrid, CurrentGridPointer, Context);
            return true;
        }

        if (!CurrentEventIsFinished)
        {
            var subEvent = CurrentEvent.SubEventStack.Pop();
            subEvent.Execute(SpriteGrid, CurrentGridPointer, Context);
        }
        return false;
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