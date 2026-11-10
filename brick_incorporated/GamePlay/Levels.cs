using System;
using brick_incorporated.Entities;
using Microsoft.Xna.Framework;

namespace brick_incorporated.GamePlay;

public static class Levels
{

    public static readonly Vector2 BrickSize = new(50f, 20f);
    public const float Padding = 6f;

    private const int MaxRows = 7;
    private const int MaxColumns = 13;
    private const float MaxBallSpeed = 480f;
    private const float MinBallRadius = 4f;
    public const int MaxBrickHealth = 9;



    public static Level For(int index) => new()
    {
        Rows = Math.Min(2 + index, MaxRows),
        Columns = Math.Min(4 + index, MaxColumns),
        Health = Math.Min(1 + index, MaxBrickHealth),
        BallSpeed = Math.Min(300f + index * 10f, MaxBallSpeed),
        BallRadius = Math.Max(8f - index, MinBallRadius),
    };
}