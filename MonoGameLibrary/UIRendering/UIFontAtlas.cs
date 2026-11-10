using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
namespace MonoGameLibrary.UIRendering;


public class UIFontAtlas(ContentManager content)
{
    private readonly Dictionary<string, SpriteFont> FontMap;
    public void MapFont(string fontName, string fileName)
    {

        if(!FontMap.TryAdd(fontName, content.Load<SpriteFont>(fileName)))
        {
            FontMap[fontName] = content.Load<SpriteFont>(fileName);
        }
    }
    public SpriteFont GetFont(string fontName)
    {
        if (!FontMap.TryGetValue(fontName, out SpriteFont spriteFont))
        {
            throw new ArgumentException($"{fontName} was not found");
        }
        return spriteFont;
    }
}