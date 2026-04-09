using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

public interface KeyHandler {
    public void HandleInput(GraphicsDevice GraphicsDevice, Keys[] pressedKeys, WindowResizer windowResizer, GameWindow Window);
}