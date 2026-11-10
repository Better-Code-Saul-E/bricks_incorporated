using Core2D.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Core2D.GameScene;
using brick_incorporated.Scenes;

namespace brick_incorporated;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SceneManager sceneManger;

    private SceneContext sceneContext;
    private InputManager inputManager;



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        sceneManger = new SceneManager();
        inputManager = new InputManager();
        sceneContext = new SceneContext(GraphicsDevice, Content, inputManager, sceneManger);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        sceneManger.ChangeScene(new Home(sceneContext));
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        inputManager.Update();
        sceneManger.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Transparent);

        _spriteBatch.Begin();
        sceneManger.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
