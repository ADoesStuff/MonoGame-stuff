using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

public class Cube : Entity
{
    protected override Model loadModel(ContentManager Content)
    {
        return Content.Load<Model>("3D-Models/cube");
    }

    public void HandleInput(Keys[] pressedKeys)
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