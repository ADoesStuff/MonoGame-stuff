using Entity;
using Input;


public class Crystal : AbstractEntity, IKeyHandler
{
    protected override Model loadModel(ContentManager Content)
    {
        return Content.Load<Model>("3D-Models/crystal3");
    }

    public void HandleMouseInput(MouseState mouse){}

    public void HandleKeyInput(Keys[] pressedKeys)
    {
        if (pressedKeys.Contains(Keys.W))
        {
            position.X -= 0.1f;
        }
        if (pressedKeys.Contains(Keys.S))
        {
            position.X += 0.1f;
        }
        if (pressedKeys.Contains(Keys.D))
        {
            position.Z -= 0.1f;
        }
        if (pressedKeys.Contains(Keys.A))
        {
            position.Z += 0.1f;
        }
        if(pressedKeys.Contains(Keys.X))
        {
            rotation.X += 1.0f;
        }
        if (pressedKeys.Contains(Keys.Z))
        {
            rotation.Z += 1.0f;
        }
        if (pressedKeys.Contains(Keys.C))
        {
            rotation.Y += 1.0f;
        }
    }
}