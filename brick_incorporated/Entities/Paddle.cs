using Core2D.Graphics;
using Core2D.Graphics.Utilities;
using Core2D.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace brick_incorporated.Entities;

// make the Speed dynamic?
public class Paddle : ShapeSprite
{
    private const float Speed = 400f;
    private readonly InputManager input;
    private readonly Rectangle bounds;



    public Paddle(Vector2 size, InputManager inputManager, Rectangle bounds) : base(size)
    {
        input = inputManager;
        this.bounds = bounds;
    }



    public override void Update(GameTime gameTime)
    {
        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        float direction = 0f;

        if (input.Keyboard.IsKeyDown(Keys.Left) || input.Keyboard.IsKeyDown(Keys.A))
        {
            direction -= 1f;
        }
        if (input.Keyboard.IsKeyDown(Keys.Right) || input.Keyboard.IsKeyDown(Keys.D))
        {
            direction += 1f;
        }

        float halfWidth = Size.X / 2f;
        float x = Transform.Position.X + direction * Speed * deltaTime;
        x = MathHelper.Clamp(x, bounds.Left + halfWidth, bounds.Right - halfWidth);

        Transform = Transform.WithPosition(new Vector2(x, Transform.Position.Y));
    }
}