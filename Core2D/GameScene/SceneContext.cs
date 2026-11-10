using Core2D.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.GameScene;

// acts as shared tools for the scenes
public class SceneContext
{
    public GraphicsDevice GraphicsDevice { get; }
    public ContentManager ContentManager { get; }
    public InputManager Input { get; }
    public SceneManager Scenes { get; }
    public Rectangle Screen => GraphicsDevice.Viewport.Bounds;


    public SceneContext(GraphicsDevice graphicsDevice, ContentManager content, 
        InputManager input, SceneManager scenes)
    {
        GraphicsDevice = graphicsDevice;
        ContentManager = content;
        Input = input;
        Scenes = scenes;
    }
}