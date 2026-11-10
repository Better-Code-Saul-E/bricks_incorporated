using brick_incorporated.Scenes;
using Core2D.GameScene;
using Core2D.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using brick_incorporated.GamePlay;

public class LevelComplete : Scene
{
    private readonly GameSession session;

    public LevelComplete(SceneContext context, GameSession session) : base(context)
    {
        this.session = session;
        float x = Context.Screen.Center.X;

        UI.Add(new TextElement("arial", $"LEVEL {session.Level} CLEARED", new Vector2(x, 140f))
        {
            IsCentered = true,
            Scale = 2.5f,
            Color = Color.White,
            BackgroundColor = Color.LimeGreen
        });

        UI.Add(new TextElement("arial", $"Score: {session.Score}", new Vector2(x, 230f))
        {
            IsCentered = true,
            Scale = 1.5f
        });

        UI.Add(new TextElement("arial", $"Lives: {session.Lives}", new Vector2(x, 270f))
        {
            IsCentered = true
        });

        UI.Add(new TextElement("arial", "Press SPACE for the next level", new Vector2(x, 340f))
        {
            IsCentered = true
        });
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (Context.Input.Keyboard.WasKeyPressed(Keys.Space))
        {
            Context.Scenes.ChangeScene(new Play(Context, session));
        }
    }
}