using Core2D.Graphics;
using Core2D.Graphics.Utilities;
using Microsoft.Xna.Framework;

namespace brick_incorporated.Entities;

public class Brick : ShapeSprite
{
    private const float HitCoolDown = 0.1f;

    private BrickState state = BrickState.Normal;
    private float hitCoolDownTimer;

    public int Health { get; private set; }
    public int PointValue { get; }
    public bool IsDestroyed => state == BrickState.Broken;



    public Brick(Vector2 size, int health = 1, int pointValue = 100) : base(size)
    {
        Health = health;
        PointValue = pointValue;
        setState(BrickState.Normal);
    }



    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if (state == BrickState.Hit)
        {
            hitCoolDownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            if (hitCoolDownTimer <= 0f)
            {
                setState(BrickState.Normal);
            }
        }
    }

    public bool TakeHit(int damage = 1)
    {
        if (state != BrickState.Normal)
        {
            return false;
        }


        Health -= damage;
        setState(Health <= 0 ? BrickState.Broken : BrickState.Hit);
        return IsDestroyed;
    }

    private void setState(BrickState nextState)
    {
        state = nextState;

        switch (state)
        {
            case BrickState.Normal:
                TintColor = getBrickColor();
                break;

            case BrickState.Hit:
                TintColor = Color.Red;
                hitCoolDownTimer = HitCoolDown;
                break;
            case BrickState.Broken:
                TintColor = Color.Transparent;
                break;
        }
    }

    private Color getBrickColor() => Health switch
    {
        >= 9 => Color.SaddleBrown,
        8 => Color.White,
        7 => Color.Turquoise,
        6 => Color.MediumPurple,
        5 => Color.HotPink,
        4 => Color.Orange,
        3 => Color.Gold,
        2 => Color.LimeGreen,
        _ => Color.DodgerBlue
    };
}