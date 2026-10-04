using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SceneManager(IInputManager buttonInput, IInputManager mouseInput, IAudioManager audioManager, ITextureAtlas textureAtlas) : ISceneManager
{
    private readonly Dictionary<string, ISceneController> NameSceneMap = [];
    private ISceneController CurrentScene;
    public bool ShouldExit;

    public void Init()
    {
        ShouldExit = false;

        AddAnimations();

        AddScenesToMap();

        CurrentScene = NameSceneMap[SceneNames.MainMenu];
        CurrentScene.Init();
    }

    public void Restart()
    {
        NameSceneMap.Clear();

        buttonInput.ClearMapping();
        mouseInput.ClearMapping();

        textureAtlas.ClearMapping();

        Init();
    }

    public void Update(int dtMs)
    {
        UpdateInputs();

        //Later on in development exiting game should not be mapped to a key but decent temporary solution for now. 
        if (buttonInput.IsPressed(InputNames.ResetGame))
        {
            Restart();
        }

        if (buttonInput.IsPressed(InputNames.ExitGame))
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

    private void AddScenesToMap()
    {
        var mainMenuScene = new MainMenuSceneController(this, mouseInput, audioManager, textureAtlas);
        NameSceneMap.TryAdd(SceneNames.MainMenu, mainMenuScene);

        // var gameplayScene = new GameplaySceneController(this, buttonInput, mouseInput, audioManager, texAtlas);
        // NameSceneMap.TryAdd(GameplaySceneController.Name, gameplayScene);

        var puzzleLevelScene = new PuzzleLevelSceneController(buttonInput, textureAtlas);
        NameSceneMap.TryAdd(SceneNames.PuzzleLevel, puzzleLevelScene);
    }
}