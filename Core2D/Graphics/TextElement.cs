using Core2D.Engine;
using Core2D.Graphics.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Graphics;


// mabey, add transform instead of scale, and position? 
// possibly abstract transform?
// make alignment struct for its origin? top, left, center, etc
public class TextElement : IComponent
{
    public SpriteFont Font { get; private set; }
    public string FontName { get; set; }
    public string Text { get; set; }
    public Vector2 Position { get; set; }
    public Color Color { get; set; } = Color.White;
    public float Scale { get; set; } = 1.0f;
    public bool IsCentered { get; set; } = false;

    public Color? BackgroundColor { get; set; }
    public Vector2 Padding { get; set; } = new(12f, 6f);
    private Texture2D pixel;



    public TextElement(string fontName, string text = "", Vector2 position = default)
    {
        FontName = fontName;
        Text = text;
        Position = position;
    }



    public void Draw(SpriteBatch spriteBatch)
    {
        if (Font == null || string.IsNullOrEmpty(Text)) return;

        Vector2 textSize = Font.MeasureString(Text) * Scale;

        if (BackgroundColor.HasValue && pixel != null)
        {
            Vector2 topLeft = IsCentered ? Position - textSize / 2f : Position;

            var box = new Rectangle(
                (int)(topLeft.X - Padding.X),
                (int)(topLeft.Y - Padding.Y),
                (int)(textSize.X + Padding.X * 2f),
                (int)(textSize.Y + Padding.Y * 2f));

            spriteBatch.Draw(pixel, box, BackgroundColor.Value);
        }

        Vector2 origin = IsCentered ? SpatialUtils.GetCenterOrigin(Font, Text) : Vector2.Zero;

        spriteBatch.DrawString(
            Font,
            Text,
            Position,
            Color,
            0f,
            origin,
            Scale,
            SpriteEffects.None,
            0f);
    }

    public void Update(GameTime gameTime)
    {
        return;
    }

    public void LoadContent(GraphicsDevice graphicsDevice, ContentManager contentManager)
    {
        pixel = ShapeRenderer.GetRectangle(graphicsDevice);

        if (!string.IsNullOrEmpty(FontName))
        {
            try
            {
                Font = contentManager.Load<SpriteFont>(FontName);
            }
            catch
            {
                Font = contentManager.Load<SpriteFont>("arial");
            }
        }
    }
}