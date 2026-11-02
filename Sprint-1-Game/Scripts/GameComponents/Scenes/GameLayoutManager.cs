using System;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using MonoGameLibrary;

public class GameLayoutManager() : Game
{
    int CurrentSceneWidth=0;
    int CurrentSceneHeight=0;
    public void Update(GameWindow window)
    {
        CurrentSceneWidth = window.ClientBounds.Width;
        CurrentSceneHeight = window.ClientBounds.Height;
    }
    public Point AnchorPoint()
    {
        Point point = new Point((CurrentSceneWidth - CurrentSceneHeight*16/9)*1/2, -(CurrentSceneHeight - CurrentSceneWidth*16/9)*1/2);
        return point;
    }
    public float ScaleFactor()
    {
        float scaleFactor = 0f;

        return scaleFactor;
    }
}