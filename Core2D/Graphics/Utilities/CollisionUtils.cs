using Microsoft.Xna.Framework;

namespace Core2D.Graphics.Utilities;


// add triangle collision?
public static class CollisionUtils
{
    public static bool CircleIntersectsCircle(Vector2 centerA, float radiusA, Vector2 centerB, float radiusB)
    {
        float distance = Vector2.DistanceSquared(centerA, centerB);
        float radiusSum = radiusA + radiusB;

        return distance <= radiusSum * radiusSum;
    }

    public static bool CircleIntersectsRectangle(Vector2 center, float radius, Rectangle rectangle)
    {
        float closestX = MathHelper.Clamp(center.X, rectangle.Left, rectangle.Right);
        float closestY = MathHelper.Clamp(center.Y, rectangle.Top, rectangle.Bottom);

        Vector2 closestPoint = new(closestX, closestY);

        float distance = Vector2.DistanceSquared(center, closestPoint);

        return distance <= radius * radius;
    }

    public static bool RectangleIntersectsRectangle(Rectangle rectangleA, Rectangle rectangleB)
    {
        return rectangleA.Intersects(rectangleB);
    }

}