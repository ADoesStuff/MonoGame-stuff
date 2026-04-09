using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

public interface KeyHandler
{
    public void HandleKeyInput(Keys[] pressedKeys);
}