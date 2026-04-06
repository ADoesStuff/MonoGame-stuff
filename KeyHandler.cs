using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public interface KeyHandler {
    public void HandleInput(Keys[] pressedKeys);
}