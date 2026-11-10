using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Core2D.Input;


public class MouseHandler
{
    private MouseState previousState;
    private MouseState currentState;

    public Vector2 Position => new Vector2(currentState.X, currentState.Y);



    public MouseHandler()
    {
        currentState = Mouse.GetState();
        previousState = currentState;
    }



    public bool IsLeftButtonDown() => currentState.LeftButton == ButtonState.Pressed;
    public bool IsLeftButtonUp() => currentState.LeftButton == ButtonState.Released;
    
    public bool IsRightButtonDown() => currentState.RightButton == ButtonState.Pressed;
    public bool IsRightButtonUp() => currentState.RightButton == ButtonState.Released;

    public bool WasLeftButtonClicked() => (currentState.LeftButton == ButtonState.Pressed) && (previousState.LeftButton == ButtonState.Released);
    public bool WasRightButtonClicked() => (currentState.RightButton == ButtonState.Pressed) && (previousState.RightButton == ButtonState.Released);

    public bool WasLeftButtonReleased() => currentState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed;
    public bool WasRightButtonReleased() => currentState.RightButton == ButtonState.Released && previousState.RightButton == ButtonState.Pressed;

    public bool IsHovering(Rectangle boundary) => boundary.Contains(Position);

    public void Update()
    {
        previousState = currentState;
        currentState = Mouse.GetState();
    }

}