using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
using System;
using Gum.Forms.Controls;
using Gum.Wireframe;

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
    private LevelContext ObjectSelectorContext = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.1875f,
        PuzzleOffsetX = 232,
        PuzzleOffsetY = 40,
    };
    private GridPointer ObjectSelectorGrid = new();
    private SpriteGrid ObjectSelectorSpriteGrid;

    private LevelContext TileSelectorContext = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.1875f,
        PuzzleOffsetX = 40,
        PuzzleOffsetY = 232,
    };
    private GridPointer TileSelectorGrid = new();
    private SpriteGrid TileSelectorSpriteGrid;

    private Panel LevelEditorPanel;
    private Gum.Forms.Controls.Button DownArrowButton;
    private Gum.Forms.Controls.Button UpArrowButton;
    private Gum.Forms.Controls.Button RightArrowButton;
    private Gum.Forms.Controls.Button LeftArrowButton;
    private int tileSelectorIndex;
    private int objectSelectorIndex;

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
        ObjectSelectorGrid.Init();
        AddObjectSelectorGridContent();
        TileSelectorGrid.Init();
        AddTileSelectorGridContent();
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
        var clickedObjectSelector = mouseInput.IsPressed(InputNames.Select) && LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, ObjectSelectorContext, out objCoord);
        var clickedTileSelector = mouseInput.IsPressed(InputNames.Select) && LevelEditorMouseCoordinates.TryGetGridCoordinate(mouseInput, TileSelectorContext, out tileCoord);

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
            Brush = new ObjectPaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, ObjectSelectorContext, ObjectSelectorGrid, ObjectSpriteFactory, UndoHistory, RedoHistory);
            if(clickedObjectSelector) Brush.Init(CurrentGrid, objCoord);
            else Brush.Init(CurrentGrid);
        }
        else if (buttonInput.IsPressed(InputNames.TileBrushMode) || clickedTileSelector)
        {
            Brush = new TilePaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, TileSelectorContext, TileSelectorGrid, TileSpriteFactory, UndoHistory, RedoHistory);
            if(clickedTileSelector) Brush.Init(CurrentGrid, tileCoord);
            else Brush.Init(CurrentGrid);
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
        ObjectSelectorSpriteGrid.Draw(sb);
        TileSelectorSpriteGrid.Draw(sb);
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

    private void AddObjectSelectorGridContent()
    {
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

    private void AddTileSelectorGridContent()
    {
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

    private void InitUI()
    {
        Gum.GumService.Default.Root.Children.Clear();
        CreateLevelEditorPanel();
    }

    private void CreateLevelEditorPanel()
    {
        LevelEditorPanel = new Panel();
        LevelEditorPanel.Dock(Dock.Fill);
        LevelEditorPanel.AddToRoot();

        DownArrowButton = new Gum.Forms.Controls.Button();
        SetupButton(DownArrowButton, Anchor.BottomLeft, 37, -100, 100, 10, "Next", true);
        DownArrowButton.Click += HandleDownClicked;
        LevelEditorPanel.AddChild(DownArrowButton);

        UpArrowButton = new Gum.Forms.Controls.Button();
        SetupButton(UpArrowButton, Anchor.TopLeft, 37, 180, 100, 10, "Prev", false);
        UpArrowButton.Click += HandleUpClicked;
        LevelEditorPanel.AddChild(UpArrowButton);

        RightArrowButton = new Gum.Forms.Controls.Button();
        SetupButton(RightArrowButton, Anchor.Top, 205, 40, 40, 10, "N\ne\nx\nt", false);
        RightArrowButton.Click += HandleRightClicked;
        LevelEditorPanel.AddChild(RightArrowButton);

        LeftArrowButton = new Gum.Forms.Controls.Button();
        SetupButton(LeftArrowButton, Anchor.TopLeft, 175, 40, 40, 10, "P\nr\ne\nv", false);
        LeftArrowButton.Click += HandleLeftClicked;
        LevelEditorPanel.AddChild(LeftArrowButton);
    }

    private void SetupButton(Gum.Forms.Controls.Button button, Anchor anchor, int x, int y, int width, int height, string text, bool isEnabled)
    {
        button.Anchor(anchor);
        button.X = x;
        button.Y = y;
        button.Width = width;
        button.Height = height;
        button.Text = text;
        button.IsEnabled = isEnabled;
    }

    private void HandleDownClicked(object sender, EventArgs e)
    {
        audioManager.PlaySound(SoundNames.Snap);

        tileSelectorIndex++;
        UpdateEnable(tileSelectorIndex, UpArrowButton, DownArrowButton, FileNames.TileSelectorFileNames);

        SwapTileSelector();
    }

    private void HandleUpClicked(object sender, EventArgs e)
    {
        audioManager.PlaySound(SoundNames.Snap);

        tileSelectorIndex--;
        UpdateEnable(tileSelectorIndex, UpArrowButton, DownArrowButton, FileNames.TileSelectorFileNames);

        SwapTileSelector();
    }

    private void HandleRightClicked(object sender, EventArgs e)
    {
        audioManager.PlaySound(SoundNames.Snap);

        objectSelectorIndex++;
        UpdateEnable(objectSelectorIndex, LeftArrowButton, RightArrowButton, FileNames.ObjectSelectorFileNames);

        SwapObjectSelector();
    }

    private void HandleLeftClicked(object sender, EventArgs e)
    {
        audioManager.PlaySound(SoundNames.Snap);

        objectSelectorIndex--;
        UpdateEnable(objectSelectorIndex, LeftArrowButton, RightArrowButton, FileNames.ObjectSelectorFileNames);

        SwapObjectSelector();
    }

    private void UpdateEnable(int index, Gum.Forms.Controls.Button prevButton, Gum.Forms.Controls.Button nextButton, string[] fileArray)
    {
        prevButton.IsEnabled = index > 0;
        nextButton.IsEnabled = index < fileArray.Length - 1;
    }

    private void SwapTileSelector()
    {
        AddObjectSelectorGridContent();

        Brush = new TilePaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, TileSelectorContext, TileSelectorGrid, TileSpriteFactory, UndoHistory, RedoHistory);
        Brush.Init(CurrentGrid);
    }

    private void SwapObjectSelector()
    {
        AddObjectSelectorGridContent();

        Brush = new ObjectPaintBrush(buttonInput, mouseInput, audioManager, SpriteGrid, Context, ObjectSelectorContext, ObjectSelectorGrid, ObjectSpriteFactory, UndoHistory, RedoHistory);
        Brush.Init(CurrentGrid);
    }
}