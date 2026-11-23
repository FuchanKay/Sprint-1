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
        GridScale = 0.1875f,
        PuzzleOffsetX = 160,
        PuzzleOffsetY = 160,
    };
    private GridPointer CurrentGrid = new();
    private SpriteGrid SpriteGrid;
    private LevelEditorPalette LevelEditorPalette;
    
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
        InitUI();

        UndoHistory.Clear();
        UndoHistory.Add(CurrentGrid.Clone());

        Brush = new EraserBrush(mouseInput, SpriteGrid, Context, UndoHistory, RedoHistory);
        Brush.Init(CurrentGrid);

        HUD = new(spriteFontManager);
        HUD.Init();
    }

    public void Update(int dtMs)
    {
        SpriteGrid.Update(dtMs);
        Brush.Update(dtMs);

        var objCoord = Coordinate.Zero;
        var tileCoord = Coordinate.Zero;
        var clickedObjectSelector = mouseInput.IsPressed(InputNames.Select) && LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, LevelEditorPalette.ObjectSelectorContext, out objCoord) && LevelEditorPalette.ObjectSelectorGrid.GetObject(objCoord).Id != ObjectIds.Empty;
        var clickedTileSelector = mouseInput.IsPressed(InputNames.Select) && LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, LevelEditorPalette.TileSelectorContext, out tileCoord) && LevelEditorPalette.TileSelectorGrid.GetTile(tileCoord).Id != TileIds.Empty;

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
        else if (buttonInput.IsPressed(InputNames.ObjectPaintBrushMode) || clickedObjectSelector)
        {
            Brush = new ObjectPaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, LevelEditorPalette.ObjectSelectorContext, LevelEditorPalette.ObjectSelectorGrid, ObjectSpriteFactory, UndoHistory, RedoHistory);
            Brush.Init(CurrentGrid);
            if(clickedObjectSelector) Brush.SetSelection(objCoord);
        }
        else if (buttonInput.IsPressed(InputNames.TileBrushMode) || clickedTileSelector)
        {
            Brush = new TilePaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, LevelEditorPalette.TileSelectorContext, LevelEditorPalette.TileSelectorGrid, TileSpriteFactory, UndoHistory, RedoHistory);
            Brush.Init(CurrentGrid);
            if(clickedTileSelector) Brush.SetSelection(tileCoord);
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
        Brush.Draw(sb);
        SpriteGrid.Draw(sb);
        LevelEditorPalette.ObjectSelectorSpriteGrid.Draw(sb);
        LevelEditorPalette.TileSelectorSpriteGrid.Draw(sb);
        HUD.Draw(sb);
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

    private void InitUI()
    {
        LevelEditorPalette = new(ObjectSpriteFactory, TileSpriteFactory, EffectSpriteFactory, audioManager);
        LevelEditorPalette.Init();
        Gum.GumService.Default.Root.Children.Clear();
        LevelEditorPalette.CreateLevelEditorPanel();
    }
}