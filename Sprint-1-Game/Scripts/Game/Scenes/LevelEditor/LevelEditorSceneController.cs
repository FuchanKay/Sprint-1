using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class LevelEditorSceneController(SceneManager sceneManager, IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas, SpriteFontManager spriteFontManager) : ISceneController
{
    private LevelContext Context = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.175f,
        PuzzleOffsetX = 160,
        PuzzleOffsetY = 160,
    };
    private GridPointer CurrentGrid = new();
    private SpriteGrid SpriteGrid;
    private LevelContext ObjectSelectorContext = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.175f,
        PuzzleOffsetX = 252,
        PuzzleOffsetY = 40,
    };
    private GridPointer ObjectSelectorGrid = new();
    private SpriteGrid ObjectSelectorSpriteGrid;

    private LevelContext TileSelectorContext = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.175f,
        PuzzleOffsetX = 40,
        PuzzleOffsetY = 252,
    };
    private GridPointer TileSelectorGrid = new();
    private SpriteGrid TileSelectorSpriteGrid;
    private Button DownArrowButton;
    private Grid Flag;
    private int tileSelectorIndex;

    private readonly ObjectSpriteFactory ObjectSpriteFactory = new(textureAtlas);
    private readonly TileSpriteFactory TileSpriteFactory = new(textureAtlas);
    private readonly EffectSpriteFactory EffectSpriteFactory = new(textureAtlas);
    private LevelEditorHUDPanel HUD = new(spriteFontManager);
    private readonly List<GridPointer> UndoHistory = [];
    private readonly List<GridPointer> RedoHistory = [];
    private readonly int UndoHistoryMaxLength = 2000;
    private IBrush Brush;
    private readonly int UndoRedoBufferMs = 400;
    private int UndoBufferTimerMs = 0;
    private int RedoBufferTimeMs = 0;

    public void Init()
    {
        InitLevelEditorGrid();
        InitObjectSelectorGrid();
        InitTileSelectorGrid();

        Flag = new Grid();
        Flag.Object = new EmptyObject();
        DownArrowButton = new DownArrowButton(sceneManager, audioManager, textureAtlas, Flag);
        DownArrowButton.Init(40, 600, mouseInput, true);

        UndoHistory.Clear();
        UndoHistory.Add(CurrentGrid.Clone());

        Brush = new EraserBrush(mouseInput, SpriteGrid, Context, UndoHistory, RedoHistory);
        Brush.Init(CurrentGrid);

        HUD = new(spriteFontManager);
        HUD.Init();
    }

    public void Update(int dtMs)
    {
        DownArrowButton.Update(dtMs);
        SpriteGrid.Update(dtMs);
        Brush.Update(dtMs);

        if (UndoHistory.Count > UndoHistoryMaxLength) UndoHistory.RemoveAt(0);

        if(Flag.Object.Id != ObjectIds.Empty)
        {
            SwapTileSelector();
            Flag.Object = new EmptyObject();
        }

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
            Brush = new ObjectPaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, ObjectSelectorContext, ObjectSelectorGrid, ObjectSpriteFactory, UndoHistory, RedoHistory);
            Brush.Init(CurrentGrid);
        }
        else if (buttonInput.IsPressed(InputNames.TileBrushMode))
        {
            Brush = new TilePaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, TileSelectorContext, TileSelectorGrid, TileSpriteFactory, UndoHistory, RedoHistory);
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
        else if (buttonInput.IsPressed(InputNames.PlayLevel))
        {
            sceneManager.PlayLevel(CurrentGrid, Context);
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
        ObjectSelectorSpriteGrid.Draw(sb);
        TileSelectorSpriteGrid.Draw(sb);
        Brush.Draw(sb);
        HUD.Draw(sb);
        DownArrowButton.Draw(sb);
    }

    private void SwapTileSelector()
    {
        tileSelectorIndex = (tileSelectorIndex + 1) % FileNames.TileSelectorFileNames.Length;

        var tileGridFileName = FileNames.TileSelectorFileNames[tileSelectorIndex];
        string[] tileGridFile = LevelFileParser.ParseLevelFile(tileGridFileName);
        LevelFileParser levelFileParser = new();
        TileSelectorContext.LevelName = levelFileParser.GetLevelName(tileGridFile);
        TileSelectorGrid = levelFileParser.CreateGrids(tileGridFile);
        TileSelectorSpriteGrid.Init(TileSelectorGrid, TileSelectorContext);

        Brush = new TilePaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, TileSelectorContext, TileSelectorGrid, TileSpriteFactory, UndoHistory, RedoHistory);
        Brush.Init(CurrentGrid);
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

    private void InitLevelEditorGrid()
    {
        CurrentGrid.Init();
        var levelFileName = FileNames.LevelFileNames[1];
        string[] levelFile = LevelFileParser.ParseLevelFile(levelFileName);
        LevelFileParser levelFileParser = new();
        Context.LevelName = levelFileParser.GetLevelName(levelFile);
        Context.LevelWidth = levelFileParser.GetLevelWidth(levelFile);
        Context.LevelHeight = levelFileParser.GetLevelHeight(levelFile);
        CurrentGrid = levelFileParser.CreateGrids(levelFile);

        SpriteGrid = new(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory);
        SpriteGrid.Init(CurrentGrid, Context);
        
    }

    private void InitObjectSelectorGrid()
    {
        ObjectSelectorGrid.Init();
        var objGridFileName = FileNames.ObjectSelectorFileNames[0];
        string[] objGridFile = LevelFileParser.ParseLevelFile(objGridFileName);
        LevelFileParser levelFileParser = new();
        ObjectSelectorContext.LevelName = levelFileParser.GetLevelName(objGridFile);
        ObjectSelectorContext.LevelWidth = levelFileParser.GetLevelWidth(objGridFile);
        ObjectSelectorContext.LevelHeight = levelFileParser.GetLevelHeight(objGridFile);
        ObjectSelectorGrid = levelFileParser.CreateGrids(objGridFile);

        ObjectSelectorSpriteGrid = new(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory);
        ObjectSelectorSpriteGrid.Init(ObjectSelectorGrid, ObjectSelectorContext);
    }

    private void InitTileSelectorGrid()
    {
        TileSelectorGrid.Init();
        var tileGridFileName = FileNames.TileSelectorFileNames[0];
        string[] tileGridFile = LevelFileParser.ParseLevelFile(tileGridFileName);
        LevelFileParser levelFileParser = new();
        TileSelectorContext.LevelName = levelFileParser.GetLevelName(tileGridFile);
        TileSelectorContext.LevelWidth = levelFileParser.GetLevelWidth(tileGridFile);
        TileSelectorContext.LevelHeight = levelFileParser.GetLevelHeight(tileGridFile);
        TileSelectorGrid = levelFileParser.CreateGrids(tileGridFile);

        TileSelectorSpriteGrid = new(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory);
        TileSelectorSpriteGrid.Init(TileSelectorGrid, TileSelectorContext);
    }
}