using Microsoft.Xna.Framework;

namespace Core2D.Engine;
//possibly move to a 'common' folder

// possibly include rendering, instead of only updating  state
public interface IUpdatable
{
    void Update(GameTime gameTime);
}