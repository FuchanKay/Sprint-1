using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.Game;

public class PuzzleLevel(ObjectFactory objectFactory, TileFactory tileFactory, ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory, LevelFileParser levelFileParser)
{
    private readonly List<ILevelEvent> EventQueue = [];
    //TODO: Some constants regarding the width and height of each grid, the size of hte level, etc. probably should be moved somewhere else idk where
    public  LevelContext Context = new()
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
    public List<GridPointer> GridPointerStack;
    public readonly SpriteGrid SpriteGrid = new(objectSpriteFactory, tileSpriteFactory);
    private readonly int MaxEventsQueued = 2;
    private readonly int MaximumGridPointerStackSize = 100;
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

    public void TryExecuteEvent()
    {
        if (CurrentEvent == null) return;
        if (!Context.IsIdle) return;
        if(Context.PlayerIsDead) return;

        var CurrentEventIsFinished = CurrentEvent.SubEventStack.Count() == 0;
        if (CurrentEventIsFinished && EventQueue.Count > 0)
        {
            PushCopyOfGridPointer();

            CurrentEvent = EventQueue[0];
            EventQueue.RemoveAt(0);

            CurrentEvent.Execute(SpriteGrid, CurrentGridPointer, Context);
            return;
        }

        if (!CurrentEventIsFinished)
        {
            var subEvent = CurrentEvent.SubEventStack.Pop();
            subEvent.Execute(SpriteGrid, CurrentGridPointer, Context);
        }
        return;
    }

    public void Undo()
    {
        
    }

    private void PushCopyOfGridPointer()
    {
        var clone = CurrentGridPointer.Clone();
        if (GridPointerStack.Count >= MaximumGridPointerStackSize)
        {
            GridPointerStack.RemoveAt(GridPointerStack.Count - 1);
        }

        

    }
}