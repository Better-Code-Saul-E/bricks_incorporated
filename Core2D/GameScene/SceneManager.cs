using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.GameScene;


public class SceneManager
{
    private Scene current;
    private Scene next;

    public void ChangeScene(Scene scene) => next = scene;

    public void Update(GameTime gameTime)
    {
        if (next != null)
        {
            current?.Unload();
            current = next;
            next = null;
            current.LoadContent();
        }

        current?.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch) => current?.Draw(spriteBatch);
}