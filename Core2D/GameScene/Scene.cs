using System.Collections.Generic;
using Core2D.Engine;
using Core2D.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.GameScene;


// require screen dimention as a property, along with game window boundary
public abstract class Scene
{
    protected SceneContext Context { get; }
    protected ComponentManager<Sprite> Entities { get; } = new();
    protected ComponentManager<TextElement> UI { get; } = new();
    // updatables is intended for spawners
    private readonly List<IUpdatable> updatables = new();

    // used for custom component managers
    private readonly List<IComponent> components = new();



    public Scene(SceneContext context)
    {
        Context = context;
        components.Add(Entities);
        components.Add(UI);
    }



    protected void AddComponent(IComponent component) => components.Add(component);
    protected void AddUpdatable(IUpdatable updatable) => updatables.Add(updatable);
    public virtual void LoadContent()
    {
        foreach (IComponent comp in components)
        {
            comp.LoadContent(Context.GraphicsDevice, Context.ContentManager);
        }
    }

    public virtual void Update(GameTime gameTime)
    {
        foreach (IUpdatable updt in updatables)
        {
            updt.Update(gameTime);
        }

        foreach (IComponent comp in components)
        {
            comp.Update(gameTime);
        }
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (IComponent comp in components)
            comp.Draw(spriteBatch);
    }

    public virtual void Unload()
    {
        Entities.Clear();
        UI.Clear();
        updatables.Clear();
    }
}