using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class LevelEditorSceneController(IInputManager buttonInput, IInputManager mouseInput, ITextureAtlas textureAtlas) : ISceneController
{
    private LevelContext Context = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.175f,
        PuzzleOffsetX = 50,
        PuzzleOffsetY = 50,
    };
    private GridPointer CurrentGrid = new();
    private readonly ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private readonly TileSpriteFactory TileSpriteFactory = new(textureAtlas);
    private readonly EffectSpriteFactory EffectSpriteFactory = new(textureAtlas);
    private readonly List<GridPointer> UndoHistory = [];
    private readonly List<GridPointer> RedoHistory = [];
    private readonly int UndoHistoryMaxLength = 2000;
    private SpriteGrid SpriteGrid;
    private IBrush Brush;
    private readonly int UndoRedoBufferMs = 400;
    private int UndoBufferTimerMs = 0;
    private int RedoBufferTimeMs = 0;

    public void Init()
    {
        CurrentGrid.Init();
        UndoHistory.Clear();
        var levelFileName = FileNames.LevelFileNames[1];
        string[] levelFile = LevelFileParser.ParseLevelFile(levelFileName);
        LevelFileParser levelFileParser = new();
        Context.LevelName = levelFileParser.GetLevelName(levelFile);
        Context.LevelWidth = levelFileParser.GetLevelWidth(levelFile);
        Context.LevelHeight = levelFileParser.GetLevelHeight(levelFile);
        CurrentGrid = levelFileParser.CreateGrids(levelFile);

        SpriteGrid = new(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory);
        SpriteGrid.Init(CurrentGrid, Context);

        UndoHistory.Add(CurrentGrid.Clone());

        Brush = new EraserBrush(mouseInput, SpriteGrid, Context, UndoHistory, RedoHistory);
        Brush.Init(CurrentGrid);
    }

    public void Update(int dtMs)
    {
        SpriteGrid.Update(dtMs);
        Brush.Update(dtMs);

        if (UndoHistory.Count > UndoHistoryMaxLength) UndoHistory.RemoveAt(0);

        if (buttonInput.IsPressed(InputNames.ToggleObjectView))
        {
            SpriteGrid.ToggleObjectLayerHide();
        }
        else if (buttonInput.IsPressed(InputNames.EraserBrushMode) || mouseInput.IsPressed(InputNames.EraserBrushMode))
        {
            Brush = new EraserBrush(mouseInput, SpriteGrid, Context, UndoHistory, RedoHistory);
            Brush.Init(CurrentGrid);
        }
        else if (buttonInput.IsPressed(InputNames.ObjectPaintBrushMode))
        {
            Brush = new ObjectPaintBrush(buttonInput, mouseInput, SpriteGrid, Context, ObjectSpriteFactory, UndoHistory, RedoHistory);
            Brush.Init(CurrentGrid);
        }   
        else if (buttonInput.IsPressed(InputNames.TileBrushMode))
        {
            Brush = new TilePaintBrush(buttonInput, mouseInput, SpriteGrid, Context, TileSpriteFactory, UndoHistory, RedoHistory);
            Brush.Init(CurrentGrid);
        }
        else if (buttonInput.IsPressed(InputNames.Undo) && UndoHistory.Count > 0)
        {
            Undo();
            UndoBufferTimerMs = UndoRedoBufferMs;
        }
        else if (buttonInput.IsHeld(InputNames.Undo) && UndoHistory.Count > 0)
        {
            UndoBufferTimerMs -= dtMs;
            if (UndoBufferTimerMs <= 0) Undo();
        }
        else if (buttonInput.IsPressed(InputNames.Redo) && RedoHistory.Count > 0)
        {
            Redo();
            RedoBufferTimeMs = UndoRedoBufferMs;
        }
        else if (buttonInput.IsHeld(InputNames.Redo) && RedoHistory.Count > 0)
        {
            RedoBufferTimeMs -= dtMs;
            if (RedoBufferTimeMs <= 0) Redo();
        }
        else if (buttonInput.IsPressed(InputNames.CycleBrushLeft))
        {
            Brush.CycleLeft();
        }
        else if (buttonInput.IsPressed(InputNames.CycleBrushRight))
        {
            Brush.CycleRight();
        }
        if (buttonInput.IsReleased(InputNames.Undo))
        {
            UndoBufferTimerMs = UndoRedoBufferMs;
        }
        if (buttonInput.IsReleased(InputNames.Redo))
        {
            RedoBufferTimeMs = UndoRedoBufferMs;
        }
    }

    public void Draw(SpriteBatch sb)
    {
        SpriteGrid.Draw(sb);
        Brush.Draw(sb);
    }

    private void Undo()
    {
        var top = UndoHistory.Count - 1;
        RedoHistory.Add(CurrentGrid);
        CurrentGrid = UndoHistory[top];
        UndoHistory.RemoveAt(top);
        SpriteGrid.Init(CurrentGrid, Context);
        Brush.Init(CurrentGrid);
    }
    private void Redo()
    {
        var top = RedoHistory.Count - 1;
        UndoHistory.Add(CurrentGrid);
        CurrentGrid = RedoHistory[top];
        RedoHistory.RemoveAt(top);
        SpriteGrid.Init(CurrentGrid, Context);
        Brush.Init(CurrentGrid);   
    }
}