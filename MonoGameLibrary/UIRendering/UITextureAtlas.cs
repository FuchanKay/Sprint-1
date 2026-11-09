using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameLibrary.Graphics;

public class UITextureAtlas 
{
    private Dictionary<string, UITextureRegion> _regions;
    public Texture2D Texture { get; set; }
    public UITextureAtlas()
    {
        _regions = new Dictionary<string, UITextureRegion>();
    }
    public UITextureAtlas(Texture2D texture)
    {
        Texture = texture;
        _regions = new Dictionary<string, UITextureRegion>();
    }
    public void AddRegion(string name, int x, int y, int width, int height)
    {
        UITextureRegion region = new UITextureRegion(Texture, x, y, width, height);
        _regions.Add(name, region);
    }
    public UITextureRegion GetRegion(string name)
    {
        return _regions[name];
    }
    public bool RemoveRegion(string name)
    {
        return _regions.Remove(name);
    }
    public UISprite CreateSprite(string regionName)
    {
        UITextureRegion region = GetRegion(regionName);
        return new UISprite(region);
    }
    public void Clear()
    {
        _regions.Clear();
    }
    public static UITextureAtlas LoadFromFile(ContentManager content, string fileName)
    {
        UITextureAtlas atlas = new UITextureAtlas();

        string filePath = Path.Combine(content.RootDirectory, fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                string texturePath = root.Element("Texture").Value;
                atlas.Texture = content.Load<Texture2D>(texturePath);

                var regions = root.Element("Regions")?.Elements("Region");

                if (regions != null)
                {
                    foreach (var region in regions)
                    {
                        string name = region.Attribute("name")?.Value;
                        int x = int.Parse(region.Attribute("x")?.Value ?? "0");
                        int y = int.Parse(region.Attribute("y")?.Value ?? "0");
                        int width = int.Parse(region.Attribute("width")?.Value ?? "0");
                        int height = int.Parse(region.Attribute("height")?.Value ?? "0");

                        if (!string.IsNullOrEmpty(name))
                        {
                            atlas.AddRegion(name, x, y, width, height);
                        }
                    }
                }

                return atlas;
            }
        }
    }

}
