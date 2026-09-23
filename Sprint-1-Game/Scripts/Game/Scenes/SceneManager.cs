using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SceneManager(IInputManager buttonInput, IInputManager mouseInput) : ISceneManager
{
    private readonly Dictionary<string, ISceneController> NameSceneMap = [];
    private ISceneController CurrentScene;
    private readonly IInputManager ButtonInput = buttonInput;
    private readonly IInputManager MouseInput = mouseInput;
    public bool ShouldExit;


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
        if (ButtonInput.IsPressed("Exit Game"))
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
        ButtonInput.Update();
        MouseInput.Update();
    }

    private void MapDefaultInputs()
    {
        //TODO: mapping input example. Should be removed
        ButtonInput.MapInput("Move North", (int) Keys.W);
        ButtonInput.MapInput("Move East", (int) Keys.D);
        ButtonInput.MapInput("Move South", (int) Keys.S);
        ButtonInput.MapInput("Move West", (int) Keys.A);

        ButtonInput.MapInput("Exit Game", (int) Keys.Escape);
        
        MouseInput.MapInput("Select", (int) MouseButtons.Left);
    }

    private void AddScenesToMap()
    {
        var mainMenuScene = new MainMenuSceneController(ButtonInput, MouseInput);
        NameSceneMap.TryAdd(MainMenuSceneController.Name, mainMenuScene);

        var gameplayScene = new GameplaySceneController(ButtonInput, MouseInput);
        NameSceneMap.TryAdd(GameplaySceneController.Name, gameplayScene);
    }
}