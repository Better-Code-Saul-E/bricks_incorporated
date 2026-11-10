using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Engine;
//possibly move to a 'common' folder


public interface ILoadable
{
    void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager);
}