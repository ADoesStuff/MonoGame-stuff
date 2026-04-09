using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameResizer : WindowResizer
{
    private int startResizeHeight;
    private int startResizeWidth;
    private int winPosX;
    private int winPosY;

    private GraphicsDeviceManager gdm;

    public GameResizer(int w, int h, GraphicsDeviceManager graDevMan)
    {
        gdm = graDevMan;
        startResizeWidth = w;
        startResizeHeight= h;
    }

    public void ResizeWindow(GraphicsDevice GraphicsDevice, GameWindow Window)
    {
        if (gdm.IsFullScreen == true)
        {	
            gdm.PreferredBackBufferHeight = startResizeHeight;
            gdm.PreferredBackBufferWidth = startResizeWidth;
            gdm.HardwareModeSwitch = true;
            Window.Position = new Point(winPosX, winPosY);
        }
        else if (gdm.IsFullScreen == false)
        {
            startResizeHeight = Window.ClientBounds.Height;
            startResizeWidth = Window.ClientBounds.Width;
            gdm.HardwareModeSwitch = false;
            winPosX = Window.Position.X;
            winPosY = Window.Position.Y;
            gdm.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
            gdm.PreferredBackBufferHeight = GraphicsDevice.Adapter.CurrentDisplayMode.Height;
        }
        gdm.IsFullScreen = !gdm.IsFullScreen;
        gdm.ApplyChanges();
    }
}