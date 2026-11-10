using Core2D.Graphics.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Graphics;


// for sprites with images
public abstract class TextureSprite : Sprite
{
    public string TextureName { get; }
    public override Rectangle Boundary
    {
        get
        {
            if (Texture == null)
                return Rectangle.Empty;

            return new Rectangle(
                (int)(Transform.Position.X - (Origin.X * Transform.Scale)),
                (int)(Transform.Position.Y - (Origin.Y * Transform.Scale)),
                (int)(Texture.Width * Transform.Scale),
                (int)(Texture.Height * Transform.Scale)
            );
        }
    }



    public TextureSprite(string textureName)
    {
        TextureName = textureName;
    }



    // find out how to give default sprite from Core2D, if possible...
    public override void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
    {
        if (!string.IsNullOrEmpty(TextureName))
        {
            try
            {
                Texture = content.Load<Texture2D>(TextureName);
            }
            catch (ContentLoadException)
            {
                Texture = content.Load<Texture2D>("missing_texture");
            }
        }

        if (Texture != null)
        {
            Origin = SpatialUtils.GetCenterOrigin(Texture);
        }
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        if (Texture == null)
        {
            return;
        }

        spriteBatch.Draw(
            Texture,
            Transform.Position,
            null,
            TintColor,
            MathHelper.ToRadians(Transform.Rotation),
            Origin,
            Transform.Scale,
            SpriteEffects.None,
            0f
        );
    }
}