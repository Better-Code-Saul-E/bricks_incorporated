using brick_incorporated.GamePlay;
using Core2D.GameScene;
using Core2D.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace brick_incorporated.Scenes;


public class GameOver : Scene
{
    public GameOver(SceneContext context, GameSession session) : base(context)
    {
        float x = Context.Screen.Center.X;

        UI.Add(new TextElement("arial", "GAME OVER", new Vector2(x, 140f))
        {
            IsCentered = true,
            Scale = 3f,
            Color = Color.White,
            BackgroundColor = Color.Sienna
        });

        UI.Add(new TextElement("arial", $"High Score: {session.HighScore}", new Vector2(x, 240f))
        {
            IsCentered = true,
            Scale = 1.5f
        });

        UI.Add(new TextElement("arial", "Press SPACE to go home", new Vector2(x, 320f))
        {
            IsCentered = true
        });
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Context.Input.Keyboard.WasKeyPressed(Keys.Space))
        {
            Context.Scenes.ChangeScene(new Home(Context));
        }
    }
}