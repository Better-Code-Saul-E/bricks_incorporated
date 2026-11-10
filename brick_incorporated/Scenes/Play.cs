using brick_incorporated.Entities;
using Core2D.Engine;
using Core2D.GameScene;
using Core2D.Graphics;
using Core2D.Graphics.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using brick_incorporated.GamePlay;

namespace brick_incorporated.Scenes;


public class Play : Scene
{
    private readonly ComponentManager<Brick> bricks = new();
    private readonly Paddle paddle;
    private readonly Ball ball;

    private readonly TextElement scoreText;
    private readonly TextElement livesText;
    private readonly TextElement levelText;

    private readonly GameSession gameSession;
    private readonly Level level;



    public Play(SceneContext context, GameSession session) : base(context)
    {
        gameSession = session;
        level = Levels.For(gameSession.Level);

        Rectangle screen = Context.Screen;

        AddComponent(bricks);


        paddle = new Paddle(new Vector2(80f, 10f), Context.Input, screen);
        paddle.Transform = paddle.Transform.WithPosition(new Vector2(screen.Center.X, screen.Bottom - 40f));

        ball = new Ball(level.BallRadius, screen);

        Entities.Add(paddle);
        Entities.Add(ball);


        scoreText = new TextElement("arial", "", new Vector2(20f, 15f));
        livesText = new TextElement("arial", "", new Vector2(screen.Right - 120f, 15f));
        levelText = new TextElement("arial", "", new Vector2(screen.Center.X - 40f, 15f));

        UI.Add(scoreText);
        UI.Add(livesText);
        UI.Add(levelText);

        UpdateHud();
        ResetBall();
        BuildBricks();
    }



    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (bricks.Items.Count == 0)
        {
            gameSession.NextLevel();
            Context.Scenes.ChangeScene(new LevelComplete(Context, gameSession));
            return;
        }

        if (!ball.IsLaunched)
        {
            ball.SetInitialLocation(new Vector2(paddle.Transform.Position.X, paddle.Boundary.Top - ball.Radius));

            if (Context.Input.Keyboard.WasKeyPressed(Keys.Space))
            {
                ball.Launch(level.BallSpeed);
            }

            UpdateHud();
            return;
        }

        if (ball.IsOutOfBounds)
        {
            gameSession.LoseLife();
            UpdateHud();

            if (gameSession.Lives <= 0)
            {
                Context.Scenes.ChangeScene(new GameOver(Context, gameSession));
            }
            else
            {
                ResetBall();
            }

            return;
        }


        ball.BounceOffPaddle(paddle.Boundary);

        Brick closest = null;
        float closestDistance = float.MaxValue;
        Vector2 ballPosition = ball.Transform.Position;

        foreach (Brick brick in bricks.Items)
        {
            if (!CollisionUtils.CircleIntersectsRectangle(ballPosition, ball.Radius, brick.Boundary))
                continue;

            if (brick.TakeHit())
            {
                bricks.Remove(brick);
                gameSession.AddScore(brick.PointValue);
            }

            float distance = Vector2.DistanceSquared(ballPosition, brick.Transform.Position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = brick;
            }
        }

        if (closest != null)
        {
            ball.BounceOff(closest.Boundary);
        }

        UpdateHud();
    }

    private void UpdateHud()
    {
        scoreText.Text = $"Score: {gameSession.Score}";
        levelText.Text = $"Level: {gameSession.Level + 1}";
        livesText.Text = $"Lives: {gameSession.Lives}";
    }

    private void ResetBall()
    {
        ball.Reset(new Vector2(paddle.Transform.Position.X, paddle.Boundary.Top - ball.Radius));
    }
    private void BuildBricks()
    {
        Vector2 size = Levels.BrickSize;
        float gridWidth = level.Columns * size.X + (level.Columns - 1) * Levels.Padding;
        float left = Context.Screen.Center.X - gridWidth / 2f;
        const float top = 60f;

        for (int row = 0; row < level.Rows; row++)
        {
            for (int column = 0; column < level.Columns; column++)
            {
                var brick = new Brick(size, level.Health, level.Health * 100);

                brick.Transform = brick.Transform.WithPosition(new Vector2(
                    left + column * (size.X + Levels.Padding) + size.X / 2f,
                    top + row * (size.Y + Levels.Padding) + size.Y / 2f));

                bricks.Add(brick);
            }
        }
    }
}