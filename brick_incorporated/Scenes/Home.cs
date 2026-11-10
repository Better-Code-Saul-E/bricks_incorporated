using Core2D.GameScene;
using Core2D.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using brick_incorporated.GamePlay;

namespace brick_incorporated.Scenes;


public class Home : Scene
{
    private Rectangle screen;
    private GameSession gameSession;

    public Home(SceneContext context) : base(context)
    {
        gameSession = new GameSession();
        screen = context.Screen;


        UI.Add(new TextElement("arialB", "BRICKS", new Vector2(screen.Center.X, 130f))
        {
            IsCentered = true,
            Scale = 7f,
            Color = Color.White,
            BackgroundColor = Color.Crimson
        });

        UI.Add(new TextElement("arial", "I  N  C  O  R  P  O  R  A  T  E  D", new Vector2(screen.Center.X, 210f))
        {
            IsCentered = true,
            Scale = 1.5f,
            Color = Color.White
        });

        UI.Add(new TextElement("arial", "Press SPACE to play", new Vector2(screen.Center.X, 320f))
        {
            IsCentered = true
        });
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Context.Input.Keyboard.WasKeyPressed(Keys.Space))
        {
            Context.Scenes.ChangeScene(new Play(Context, gameSession));
        }
    }
}