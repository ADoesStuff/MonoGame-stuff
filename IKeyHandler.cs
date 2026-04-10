using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Input;
public interface IKeyHandler
{
    public void HandleKeyInput(Keys[] pressedKeys);
}