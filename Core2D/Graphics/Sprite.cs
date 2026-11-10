using Core2D.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Graphics;


// ABSTRACT this even move!!!@#4;laskdjfa;lskdjg
// potential interfaces/classes. texture rendering, position, scaling, movement, bondary(differnet types of bondarys)
// add struct that contains info on window dimensions, or a record for updatability

public abstract class Sprite : IComponent
{
    public Texture2D Texture { get; protected set; }
    public Transform2D Transform { get; set; } = Transform2D.Default;
    public Trajectory2D Trajectory { get; set; } = Trajectory2D.Default;
    public Vector2 Origin { get; protected set; }


    public Color TintColor { get; set; } = Color.White;

    public abstract Rectangle Boundary { get; }


    public virtual void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Transform = Transform.WithPosition(Transform.Position + Trajectory.Velocity * deltaTime);
    }
    public abstract void Draw(SpriteBatch spriteBatch);
    public abstract void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager);
}