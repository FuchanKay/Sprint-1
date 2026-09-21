using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SceneManager : ISceneManager
{
    private readonly Dictionary<string, ISceneController> NameSceneMap = [];
    private ISceneController CurrentScene;
    private readonly KeyboardInputManager KeyInput;
    private readonly MouseInputManager MouseInput;
    public bool ShouldExit;

    public SceneManager()
    {
        KeyInput = new();
        MouseInput = new();
    }

    public void Init()
    {
        ShouldExit = false;

        MapDefaultInputs();

        AddScenesToMap();

        CurrentScene = NameSceneMap[MainMenuSceneController.Name];
        CurrentScene.Init(this);
    }

    public void Update(int dtMs)
    {
        UpdateInputs();

        //Later on in development exiting game should not be mapped to a key but decent temporary solution for now. 
        if (KeyInput.IsPressed("Exit Game"))
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
        }
    }
    
    public void ExitGame()
    {
        ShouldExit = true;
    }

    private void UpdateInputs()
    {
        KeyInput.Update();
        MouseInput.Update();
    }

    private void MapDefaultInputs()
    {
        //TODO: mapping input example. Should be removed
        KeyInput.MapInput("Move North", (int) Keys.W);
        KeyInput.MapInput("Move East", (int) Keys.D);
        KeyInput.MapInput("Move South", (int) Keys.S);
        KeyInput.MapInput("Move West", (int) Keys.A);

        KeyInput.MapInput("Exit Game", (int) Keys.Escape);
        
        MouseInput.MapInput("Select", (int) MouseButtons.Left);
    }

    private void AddScenesToMap()
    {
        var mainMenuScene = new MainMenuSceneController(KeyInput, MouseInput);
        NameSceneMap.TryAdd(MainMenuSceneController.Name, mainMenuScene);

        var gameplayScene = new GameplaySceneController(KeyInput, MouseInput);
        NameSceneMap.TryAdd(GameplaySceneController.Name, gameplayScene);
    }
}