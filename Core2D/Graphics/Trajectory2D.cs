using Microsoft.Xna.Framework;

namespace Core2D.Graphics;


// consider renaming static method
public readonly struct Trajectory2D
{
    public Vector2 Direction { get; }
    public float Speed { get; }
    public Vector2 Velocity => Direction * Speed;



    public Trajectory2D(Vector2 direction, float speed)
    {
        Direction = direction == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(direction);
        Speed = speed;
    }



    public Trajectory2D WithDirection(Vector2 direction)
    {
        direction.Normalize();
        return new(direction, Speed);
    }
    public Trajectory2D WithSpeed(float speed) => new(Direction, speed);
    public static Trajectory2D Default => new(Vector2.Zero, 0f);
}