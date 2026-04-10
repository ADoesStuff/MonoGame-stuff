using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics;

public interface IWindowResizer {
    public void ResizeWindow(GraphicsDevice GraphicsDevice, GameWindow Window);
}