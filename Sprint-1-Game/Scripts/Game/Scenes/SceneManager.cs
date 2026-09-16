using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework.Graphics;
using Scripts.GameComponents;

namespace Scripts.Game;

public class SceneManager : ISceneManager
{
    private readonly Dictionary<string, IController> nameControllerMap = [];

    private IController current;

    public SceneManager()
    {
        
    }
    public void Init()
    {
        var mainMenu = new MainMenuController();
        nameControllerMap.TryAdd("Main Menu", mainMenu);

        current = nameControllerMap["Main Menu"];
    }

    public void Update(int dtMs)
    {
        current.Update(dtMs);
    }

    public void Draw(SpriteBatch sb)
    {
        current.Draw(sb);
    }

    public void SwapScene(string sceneName)
    {
        current = nameControllerMap[sceneName];
    }
}