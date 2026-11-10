using System;
using Core2D.Graphics;
using Core2D.Graphics.Utilities;
using Microsoft.Xna.Framework;

namespace brick_incorporated.Entities;

public class Ball : ShapeSprite
{
    private readonly Rectangle bounds;

    public bool IsLaunched { get; private set; }
    public bool IsOutOfBounds => Transform.Position.Y - Radius > bounds.Bottom;



    public Ball(float radius, Rectangle bounds) : base(radius)
    {
        this.bounds = bounds;
    }



    public void SetInitialLocation(Vector2 position)
    {
        this.Transform = Transform.WithPosition(position);
    }

    public void Launch(float speed)
    {
        if (IsLaunched)
        {
            return;
        }

        IsLaunched = true;
        Trajectory = new Trajectory2D(new Vector2(1f, -1f), speed);
    }

    public void Reset(Vector2 position)
    {
        IsLaunched = false;
        Trajectory = Trajectory2D.Default;
        Transform = Transform.WithPosition(position);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        BounceOffWalls();
    }

    private void BounceOffWalls()
    {
        Vector2 position = Transform.Position;
        Vector2 dirction = Trajectory.Direction;

        if (position.X - Radius < bounds.Left)
        {
            position.X = bounds.Left + Radius;
            dirction.X = Math.Abs(dirction.X);
        }
        if (position.X + Radius > bounds.Right)
        {
            position.X = bounds.Right - Radius;
            dirction.X = -Math.Abs(dirction.X);
        }
        if (position.Y - Radius < bounds.Top)
        {
            position.Y = bounds.Top + Radius;
            dirction.Y = Math.Abs(dirction.Y);
        }
        // if (position.Y + Radius > bounds.Bottom)
        // {
        //     position.Y = bounds.Bottom - Radius;
        //     dirction.Y = -Math.Abs(dirction.Y);
        // }

        Transform = Transform.WithPosition(position);
        Trajectory = Trajectory.WithDirection(dirction);
    }

    public void BounceOffPaddle(Rectangle paddle)
    {
        if (Trajectory.Direction.Y <= 0f)
        {
            return;
        }

        if (!CollisionUtils.CircleIntersectsRectangle(Transform.Position, Radius, paddle))
        {
            return;
        }

        float hit = (Transform.Position.X - paddle.Center.X) / (paddle.Width / 2f);
        hit = MathHelper.Clamp(hit, -1f, 1f);

        Vector2 direction = new Vector2(hit, -1f);
        Trajectory = Trajectory.WithDirection(direction);

        Transform = Transform.WithPosition(new Vector2(Transform.Position.X, paddle.Top - Radius));
    }

    public bool BounceOff(Rectangle rect)
    {

        Vector2 position = Transform.Position;

        if (!CollisionUtils.CircleIntersectsRectangle(position, Radius, rect))
        {
            return false;
        }

        Vector2 direction = Trajectory.Direction;

        if (direction.X > 0f && position.X < rect.Left)
        {
            direction.X = -direction.X;
        }
        else if (direction.X < 0f && position.X > rect.Right)
        {
            direction.X = -direction.X;
        }
        else if (direction.Y > 0f && position.Y < rect.Top)
        {
            direction.Y = -direction.Y;
        }
        else if (direction.Y < 0f && position.Y > rect.Bottom)
        {
            direction.Y = -direction.Y;
        }

        Trajectory = Trajectory.WithDirection(direction);

        return true;
    }
}