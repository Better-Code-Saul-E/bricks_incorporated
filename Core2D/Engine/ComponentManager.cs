using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Engine;


// generic manager intended for simple batch use, add/remove, load, update, draw
public class ComponentManager<T> : IComponent where T : IComponent
{
    private GraphicsDevice graphicsDevice;
    private ContentManager contentManager;
    private bool contentLoaded;

    private readonly List<T> items = new();
    private readonly List<T> pendingAddition = new();
    private readonly List<T> pendingRemoval = new();

    public IReadOnlyList<T> Items => items;




    public void Add(T item)
    {
        if (item == null || items.Contains(item) || pendingAddition.Contains(item))
        {
            return;
        }

        pendingAddition.Add(item);
    }

    public void Remove(T item)
    {
        if (item == null)
        {
            return;
        }
        // no reason to add if its getting removed
        if (pendingAddition.Remove(item))
        {
            return;
        }

        if (!pendingRemoval.Contains(item))
        {
            pendingRemoval.Add(item);
        }
    }

    public void LoadContent(GraphicsDevice graphics, ContentManager content)
    {
        graphicsDevice = graphics;
        contentManager = content;
        contentLoaded = true;
        ProcessPending();
    }

    public void Update(GameTime gameTime)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].Update(gameTime);
        }

        ProcessPending();
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].Draw(spriteBatch);
        }
    }

    public void Clear()
    {
        items.Clear();
        pendingAddition.Clear();
        pendingRemoval.Clear();
    }

    private void ProcessPending()
    {
        foreach (T item in pendingRemoval)
        {
            items.Remove(item);
        }

        pendingRemoval.Clear();

        foreach (T item in pendingAddition)
        {
            if (contentLoaded) item.LoadContent(graphicsDevice, contentManager);
            items.Add(item);
        }
        
        pendingAddition.Clear();
    }
}