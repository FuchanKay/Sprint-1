using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SceneManager(IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas texAtlas) : ISceneManager
{
    private readonly Dictionary<string, ISceneController> NameSceneMap = [];
    private ISceneController CurrentScene;
    public bool ShouldExit;

    public void Init()
    {
        ShouldExit = false;

        AddAnimations();

        AddScenesToMap();

        MapDefaultInputs();

        CurrentScene = NameSceneMap[MainMenuSceneController.Name];
        CurrentScene.Init();
    }

    public void Restart()
    {
        NameSceneMap.Clear();

        buttonInput.ClearMapping();
        mouseInput.ClearMapping();

        texAtlas.ClearMapping();

        Init();
    }

    public void Update(int dtMs)
    {
        UpdateInputs();

        //Later on in development exiting game should not be mapped to a key but decent temporary solution for now. 
        if (buttonInput.IsPressed("Restart Game"))
        {
            Restart();
        }

        if (buttonInput.IsPressed("Exit Game"))
        {
            ExitGame();
        }
        CurrentScene.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        CurrentScene.Draw(sb);
    }

    public void SwapScene(string sceneName)
    {
        if (NameSceneMap.TryGetValue(sceneName, out ISceneController scene))
        {
            CurrentScene = scene;
            scene.Init();
        }
    }

    public void ExitGame()
    {
        ShouldExit = true;
    }

    private void UpdateInputs()
    {
        buttonInput.Update();
        mouseInput.Update();
    }

    private void MapDefaultInputs()
    {
        //TODO: mapping input example. Should be removed
        buttonInput.MapInput("Move North", (int)Keys.W);
        buttonInput.MapInput("Move East", (int)Keys.D);
        buttonInput.MapInput("Move South", (int)Keys.S);
        buttonInput.MapInput("Move West", (int)Keys.A);
        buttonInput.MapInput("Destroy", (int)Keys.Space);
        buttonInput.MapInput("Snap", (int)Keys.E);
        buttonInput.MapInput("Cycle Block Left", (int)Keys.T);
        buttonInput.MapInput("Cycle Block Right", (int)Keys.Y);

        buttonInput.MapInput("Exit Game", (int)Keys.Q);
        buttonInput.MapInput("Restart Game", (int)Keys.R);

        mouseInput.MapInput("Select", (int)MouseButtons.Left);
    }

    private void AddScenesToMap()
    {
        var mainMenuScene = new MainMenuSceneController(this, mouseInput, audioManager);
        NameSceneMap.TryAdd(MainMenuSceneController.Name, mainMenuScene);

        var gameplayScene = new GameplaySceneController(this, buttonInput, mouseInput, audioManager, texAtlas);
        NameSceneMap.TryAdd(GameplaySceneController.Name, gameplayScene);
    }

    private void AddAnimations()
    {
        texAtlas.AddAnimation("SproutWalkDown", 3, 6, 64, 64);
        texAtlas.AddAnimation("SproutWalkRight", 0, 6, 64, 64);
        // AddAnimation(name, row, numFrames, width, height, delay)
        texAtlas.AddAnimation("SproutWalkLeft", 1, 6, 64, 64, 100);
        texAtlas.AddAnimation("Explode", 1, 4, 105, 96, 150);
        texAtlas.AddAnimation("Destroy", 1, 4, 105, 96, 150);
        texAtlas.AddAnimation("BombIdle", 0, 1, 86, 96, 0);
        texAtlas.AddAnimation("TimedBombIdle", 0, 1, 57, 96, 0);
        texAtlas.AddAnimation("StoneBlockIdle", 0, 1, 96, 96, 0);
        texAtlas.AddAnimation("RockIdle", 0, 1, 96, 96, 0);
        texAtlas.AddAnimation("DoorIdle", 0, 1, 53, 96, 0);
        texAtlas.AddAnimation("StoneWallIdle", 0, 1, 96, 96, 0);
        texAtlas.AddAnimation("PylonFullPower", 0, 1, 99, 192, 0);
        texAtlas.AddAnimation("PylonHalfPower", 1, 1, 99, 192, 0);
        texAtlas.AddAnimation("PylonNoPower", 2, 1, 99, 192, 0);
        texAtlas.AddAnimation("VineIdle", 0, 1, 96, 96, 0);
        // AddRegion(name, column, row, width, height)
        texAtlas.AddRegion("SproutIdle", 0, 5, 64, 64);
        
    }
}