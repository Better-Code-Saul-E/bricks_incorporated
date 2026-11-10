namespace Core2D.Input;

public class InputManager
{
    public KeyboardHandler Keyboard { get; } = new();
    public MouseHandler Mouse {get;} = new();

    public void Update()
    {
        Keyboard.Update();
        Mouse.Update();
    }
}