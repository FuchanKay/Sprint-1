using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Scripts.GameComponents;

public class TextureAtlas(ContentManager content) : ITextureAtlas
{
    private const int Delay = 100;
    private readonly Dictionary<string, Texture2D> Textures = [];
    private readonly Dictionary<string, Rectangle> Regions = [];
    private readonly Dictionary<string, Animation> Animations = [];
    private readonly Dictionary<string, Texture2D> Textures = [];

    public void AddTexture(string name, string fileName)
    {
        var texture = content.Load<Texture2D>(fileName);
        if (!Textures.TryAdd(name, texture))
        {
            Textures[name] = texture;
        }
    }

    public Texture2D GetTexture(string name)
    {
        if (!Textures.TryGetValue(name, out var texture))
        {
            throw new ArgumentException($"{name} was not found");
        }
        return texture;
    }

    public void AddTexture(string name, string fileName)
    {
        var texture = content.Load<Texture2D>(fileName);
        if (!Textures.TryAdd(name, texture))
        {
            Textures[name] = texture;
        }
    }

    public Texture2D GetTexture(string name)
    {
        if (!Textures.TryGetValue(name, out var texture))
        {
            throw new ArgumentException($"{name} could not be found");
        }
        return texture;
    }

    public void AddRegion(string name, int row, int column, int width, int height)
    {
        Regions.TryAdd(name, new Rectangle(row * width, column * height, width, height));
    }

    public Rectangle GetRegion(string name)
    {
        if (!Regions.TryGetValue(name, out Rectangle region))
        {
            throw new ArgumentException($"{name} was not found");
        }
        return region;
    }

    public void AddAnimation(string name, int row, int numFrames, int width, int height)
    {
        Animation animation = new Animation(Delay);
        LoadAnimation(name, row, numFrames, width, height, animation);
    }

    public void AddAnimation(string name, int row, int numFrames, int width, int height, int delay)
    {
        Animation animation = new Animation(delay);
        LoadAnimation(name, row, numFrames, width, height, animation);
    }

    public Animation GetAnimation(string name)
    {
        if (!Animations.TryGetValue(name, out Animation animation))
        {
            throw new ArgumentException($"{name} was not found");
        }
        return animation;
    }

    private void LoadAnimation(string name, int row, int numFrames, int width, int height, Animation animation)
    {
        for (int col = 0; col < numFrames; col++)
        {
            animation.Frames.Add(new Rectangle(width * col, height * row, width, height));
        }

        Animations.TryAdd(name, animation);
    }

    public void ClearMapping()
    {
        Animations.Clear();
    }
}