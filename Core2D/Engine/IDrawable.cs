using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Engine;
//possibly move to a 'common' folder

public interface IDrawable
{
    void Draw(SpriteBatch spriteBatch);
}