using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core2D.Graphics.Utilities;

// add options for
// top right, bottom left, bottom right
public static class SpatialUtils
{
    public static Vector2 GetCenterOrigin(Texture2D texture) =>
        texture == null ? Vector2.Zero : new Vector2(texture.Width, texture.Height) * 0.5f;
    
    public static Vector2 GetCenterOrigin(SpriteFont font, string text) => 
        font == null || string.IsNullOrEmpty(text) ? Vector2.Zero : font.MeasureString(text) * 0.5f;
}