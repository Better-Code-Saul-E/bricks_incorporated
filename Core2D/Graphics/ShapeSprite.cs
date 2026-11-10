using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Graphics;


// for sprites with shapes
// add dispose method to remove, or mabey cache it?
public class ShapeSprite : Sprite
{
    public ShapeType Shape { get; }
    public Vector2 Size { get; }
    public float Radius { get; }
    public override Rectangle Boundary
    {
        get
        {
            float width = Size.X * Transform.Scale;
            float height = Size.Y * Transform.Scale;

            return new Rectangle(
                (int)(Transform.Position.X - width * 0.5),
                (int)(Transform.Position.Y - height * 0.5),
                (int)width,
                (int)height
            );
        }
    }


    public ShapeSprite(Vector2 rectangleSize)
    {
        Shape = ShapeType.Rectangle;
        Size = rectangleSize;
    }
    public ShapeSprite(float circleRadius)
    {
        Shape = ShapeType.Circle;
        Radius = circleRadius;
        Size = new Vector2(circleRadius * 2);
    }



    public override void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
    {
        Texture = Shape switch
        {
            ShapeType.Rectangle =>
                ShapeRenderer.GetRectangle(graphicsDevice),

            ShapeType.Circle =>
                ShapeRenderer.GetCircle(graphicsDevice),

            _ => throw new ArgumentOutOfRangeException()
        };

        Origin = new Vector2(Texture.Width, Texture.Height) / 2f;
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Texture == null)
            return;

        Vector2 scale = Shape == ShapeType.Circle
            ? new Vector2(Size.X / Texture.Width)
            : new Vector2(Size.X / Texture.Width, Size.Y / Texture.Height);

        spriteBatch.Draw(
            Texture,
            Transform.Position,
            null,
            TintColor,
            MathHelper.ToRadians(Transform.Rotation),
            Origin,
            scale * Transform.Scale,
            SpriteEffects.None,
            0f);
    }
}