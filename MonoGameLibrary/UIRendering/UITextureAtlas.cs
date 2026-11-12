using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.UIRendering;

public class UITextureAtlas 
{
    private Dictionary<string, UITextureRegion> UIRegions;
    public Texture2D Texture { get; set; }
    public UITextureAtlas()
    {
        UIRegions = new Dictionary<string, UITextureRegion>();
    }
    public UITextureAtlas(Texture2D texture)
    {
        Texture = texture;
        UIRegions = new Dictionary<string, UITextureRegion>();
    }
    public void AddRegion(string name, int x, int y, int width, int height)
    {
        UITextureRegion uIRegion = new UITextureRegion(Texture, x, y, width, height);
        UIRegions.Add(name, uIRegion);
    }
    public UITextureRegion GetRegion(string name)
    {
        return UIRegions[name];
    }
    public bool RemoveRegion(string name)
    {
        return UIRegions.Remove(name);
    }
    public UISprite CreateSprite(string regionName)
    {
        UITextureRegion uIRegion = GetRegion(regionName);
        return new UISprite(uIRegion);
    }
    public void Clear()
    {
        UIRegions.Clear();
    }
    public void LoadFromFile(ContentManager content, string fileName)
    {
        string filePath = Path.Combine(content.RootDirectory, fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;
                
                string texturePath = root.Element("UITexture").Value;
                Texture = content.Load<Texture2D>(texturePath);
                int width = int.Parse(root.Element("DefultSetting").Attribute("width")?.Value ?? "0");
                int height = int.Parse(root.Element("DefultSetting").Attribute("height")?.Value ?? "0");

                var uIRegions = root.Element("UIRegions")?.Elements("UIRegion");

                if (uIRegions != null)
                {
                    foreach (var uIRegion in uIRegions)
                    {
                        string name = uIRegion.Attribute("name")?.Value;
                        int x = int.Parse(uIRegion.Attribute("x")?.Value ?? "0");
                        int y = int.Parse(uIRegion.Attribute("y")?.Value ?? "0");

                        if (!string.IsNullOrEmpty(name))
                        {
                            AddRegion(name, x, y, width, height);
                        }
                    }
                }
            }
        }
    }

}
