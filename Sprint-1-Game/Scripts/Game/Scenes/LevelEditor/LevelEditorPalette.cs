using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;
using System;
using Gum.Forms.Controls;
using Gum.Wireframe;

namespace Scripts.Game;

public class LevelEditorPalette(ObjectSpriteFactory objectSpriteFactory, TileSpriteFactory tileSpriteFactory, EffectSpriteFactory effectSpriteFactory, ShaderManager shaderManager, IAudioManager audioManager)
{
    public LevelContext ObjectSelectorContext = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.1875f,
        PuzzleOffsetX = 232,
        PuzzleOffsetY = 40,
    };
    public GridPointer ObjectSelectorGrid = new();
    public SpriteGrid ObjectSelectorSpriteGrid;

    public LevelContext TileSelectorContext = new()
    {
        RawSpriteWidthPx = 256,
        RawSpriteHeightPx = 256,
        GridScale = 0.1875f,
        PuzzleOffsetX = 40,
        PuzzleOffsetY = 232,
    };
    public GridPointer TileSelectorGrid = new();
    public SpriteGrid TileSelectorSpriteGrid;

    private Panel LevelEditorPanel;
    private Gum.Forms.Controls.Button DownArrowButton;
    private Gum.Forms.Controls.Button UpArrowButton;
    private Gum.Forms.Controls.Button RightArrowButton;
    private Gum.Forms.Controls.Button LeftArrowButton;
    private int tileSelectorIndex;
    private int objectSelectorIndex;

    public void Init()
    {
        ObjectSelectorGrid.Init();
        UpdateObjectSelectorGrid();
        TileSelectorGrid.Init();
        UpdateTileSelectorGrid();
    }

    public void CreateLevelEditorPanel()
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
    private static void SetupButton(Gum.Forms.Controls.Button button, Anchor anchor, int x, int y, int width, int height, string text, bool isEnabled)
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

        UpdateTileSelectorGrid();
    }

    private void HandleUpClicked(object sender, EventArgs e)
    {
        audioManager.PlaySound(SoundNames.Snap);

        tileSelectorIndex--;
        UpdateEnable(tileSelectorIndex, UpArrowButton, DownArrowButton, FileNames.TileSelectorFileNames);

        UpdateTileSelectorGrid();
    }

    private void HandleRightClicked(object sender, EventArgs e)
    {
        audioManager.PlaySound(SoundNames.Snap);

        objectSelectorIndex++;
        UpdateEnable(objectSelectorIndex, LeftArrowButton, RightArrowButton, FileNames.ObjectSelectorFileNames);

        UpdateObjectSelectorGrid();
    }

    private void HandleLeftClicked(object sender, EventArgs e)
    {
        audioManager.PlaySound(SoundNames.Snap);

        objectSelectorIndex--;
        UpdateEnable(objectSelectorIndex, LeftArrowButton, RightArrowButton, FileNames.ObjectSelectorFileNames);

        UpdateObjectSelectorGrid();
    }

    private void UpdateEnable(int index, Gum.Forms.Controls.Button prevButton, Gum.Forms.Controls.Button nextButton, string[] fileArray)
    {
        prevButton.IsEnabled = index > 0;
        nextButton.IsEnabled = index < fileArray.Length - 1;
    }

        private void UpdateObjectSelectorGrid()
    {
        var objGridFileName = FileNames.ObjectSelectorFileNames[objectSelectorIndex];
        string[] objGridFile = LevelFileParser.ParseLevelFile(objGridFileName);
        LevelFileParser levelFileParser = new();
        ObjectSelectorContext.LevelName = levelFileParser.GetLevelName(objGridFile);
        ObjectSelectorContext.LevelWidth = levelFileParser.GetLevelWidth(objGridFile);
        ObjectSelectorContext.LevelHeight = levelFileParser.GetLevelHeight(objGridFile);
        ObjectSelectorGrid = levelFileParser.CreateGrids(objGridFile);

        ObjectSelectorSpriteGrid = new(objectSpriteFactory, tileSpriteFactory, effectSpriteFactory, shaderManager);
        ObjectSelectorSpriteGrid.Init(ObjectSelectorGrid, ObjectSelectorContext);
    }

    private void UpdateTileSelectorGrid()
    {
        var tileGridFileName = FileNames.TileSelectorFileNames[tileSelectorIndex];
        string[] tileGridFile = LevelFileParser.ParseLevelFile(tileGridFileName);
        LevelFileParser levelFileParser = new();
        TileSelectorContext.LevelName = levelFileParser.GetLevelName(tileGridFile);
        TileSelectorContext.LevelWidth = levelFileParser.GetLevelWidth(tileGridFile);
        TileSelectorContext.LevelHeight = levelFileParser.GetLevelHeight(tileGridFile);
        TileSelectorGrid = levelFileParser.CreateGrids(tileGridFile);

        TileSelectorSpriteGrid = new(objectSpriteFactory, tileSpriteFactory, effectSpriteFactory, shaderManager);
        TileSelectorSpriteGrid.Init(TileSelectorGrid, TileSelectorContext);
    }

}