using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

public interface KeyHandler {
    public void HandleInput(GraphicsDevice GraphicsDevice, Keys[] pressedKeys, ref Vector3 position, ref Vector3 rotation, WindowResizer windowResizer, GameWindow Window);
}