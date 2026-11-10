using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Graphics;



public static class ShapeRenderer
{
    const int resolution = 64;

    // temp cache for brick breaker, will be removed!!!!
    private static Texture2D rectangle, circle;


    public static Texture2D GetRectangle(GraphicsDevice graphicsDevice)
    {
        if(rectangle == null)
        {
            rectangle = CreateRectangle(graphicsDevice);
        }

        return rectangle;
    }
    public static Texture2D CreateRectangle(GraphicsDevice graphicsDevice)
    {
        Texture2D texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData(new[] { Color.White });

        return texture;
    }

    public static Texture2D GetCircle(GraphicsDevice graphicsDevice)
    {
        if(circle == null)
        {
            circle = CreateCircle(graphicsDevice);
        }

        return circle;
    }
    public static Texture2D CreateCircle(GraphicsDevice graphicsDevice)
    {
        Texture2D texture = new Texture2D(graphicsDevice, resolution, resolution);

        Color[] circleData = new Color[resolution * resolution];

        float center = resolution / 2f;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float distance = Vector2.Distance(
                    new Vector2(x + 0.5f, y + 0.5f),
                    new Vector2(center, center));

                circleData[y * resolution + x] =
                    distance <= center ? Color.White : Color.Transparent;
            }
        }

        texture.SetData(circleData);

        return texture;
    }
}