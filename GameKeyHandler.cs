using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameKeyHandler : KeyHandler
{
    private bool f11Clickable = true;

    public void HandleInput(GraphicsDevice GraphicsDevice, Keys[] pressedKeys, WindowResizer windowResizer, GameWindow Window) 
    {
        if (pressedKeys.Contains(Keys.F11) & f11Clickable)
        {
            windowResizer.ResizeWindow(GraphicsDevice, Window);
        }
        f11Clickable = !pressedKeys.Contains(Keys.F11);
    }
}