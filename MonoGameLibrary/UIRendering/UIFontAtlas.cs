using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
namespace MonoGameLibrary.UIRendering;


public class UIFontAtlas
{
    private readonly Dictionary<string, SpriteFont> Fonts;
    public UIFontAtlas()
    {
        Fonts = new Dictionary<string, SpriteFont>();
    }
    public void MapFont(ContentManager content, string fontName, string fileName)
    {

        if(!Fonts.TryAdd(fontName, content.Load<SpriteFont>(fileName)))
        {
            Fonts[fontName] = content.Load<SpriteFont>(fileName);
        }
    }
    public SpriteFont GetFont(string fontName)
    {
        return Fonts[fontName];
    }
}