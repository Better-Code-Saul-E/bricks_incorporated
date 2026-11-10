using Microsoft.Xna.Framework;

namespace Core2D.Graphics;

// add a normalize method
// mabey replace 
// consider renaming static method
public readonly struct Transform2D
{
    public Vector2 Position { get; }
    public float Rotation { get; }
    public float Scale { get; }



    public Transform2D(Vector2 position, float rotation = 0f, float scale = 1f)
    {
        Position = position;
        Rotation = rotation;
        Scale = scale;
    }



    public Transform2D WithRotation(float degrees) => new(Position, degrees, Scale);
    public Transform2D WithPosition(Vector2 position) => new(position, Rotation, Scale);
    public Transform2D WithScale(float scale) => new(Position, Rotation, scale);
    public static Transform2D Default => new(Vector2.Zero, 0f, 1f);
}