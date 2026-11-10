using Microsoft.Xna.Framework.Input;

namespace Core2D.Input;

// add binding like unity EX: left is - A, leftstick, left arrow
public class KeyboardHandler
{
    private KeyboardState previousState;
    private KeyboardState currentState;



    public KeyboardHandler()
    {
        currentState = Keyboard.GetState();
        previousState = currentState;
    }



    public void Update()
    {
        previousState = currentState;
        currentState = Keyboard.GetState();
    }



    public bool IsKeyDown(Keys key) => currentState.IsKeyDown(key);
    public bool IsKeyUp(Keys key) => currentState.IsKeyUp(key);
    public bool WasKeyPressed(Keys key) => currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
    public bool HasReleasedKey(Keys key) => currentState.IsKeyUp(key) && previousState.IsKeyDown(key);
}