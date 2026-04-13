using Input;
using Graphics;
using Entity;

public class MainGame : Game, IKeyHandler, IMouseHandler, IExiter
{
	private IModelDrawer modelDrawer;
	private IWindowResizer windowResizer;
	private IExiter gameExiter;


	private const int WINDOW_INIT_WIDTH = 600;
	private const int WINDOW_INIT_HEIGHT= 800;
	private bool f11Clickable = true;
	private SpriteBatch spriteBatch;

	private Rectangle rect;

	private Cube cube;
	private Crystal crystal;
	private ExitButton exit;
	private GraphicsDeviceManager gdm;

	public MainGame() {
		gdm = new GraphicsDeviceManager(this);
		modelDrawer = new GameModelDrawer();
		windowResizer = new GameResizer(WINDOW_INIT_WIDTH, WINDOW_INIT_HEIGHT, gdm);
		cube = new Cube();
		crystal = new Crystal();
		exit = new ExitButton();
		
	}

	protected override void LoadContent()
	{
		Content.RootDirectory = "Content";
		cube.load(Content);
		crystal.load(Content);
		exit.load(Content, GraphicsDevice);

		// rect = new Rectangle(Window.ClientBounds.Center.X,Window.ClientBounds.Center.Y ,200, 300);
		rect = new Rectangle();
		rect.Y = 10;
	}

	protected override void Initialize()
	{
		gdm.HardwareModeSwitch = false;
		gdm.IsFullScreen = false;
		IsMouseVisible = true;
		Window.AllowUserResizing = true;
		gdm.PreferredBackBufferHeight= WINDOW_INIT_HEIGHT;
		gdm.PreferredBackBufferWidth = WINDOW_INIT_WIDTH;
		gdm.ApplyChanges();
		base.Initialize();
	}
	
	protected override void Draw(GameTime gt)
	{
		if(cube != null && cube.getModel() != null)
		{
			GraphicsDevice.Clear(Color.BlueViolet);
			modelDrawer.DrawModel(GraphicsDevice, cube);
			//modelDrawer.DrawModel(GraphicsDevice, crystal);
		}
		exit.Draw();
		base.Draw(gt);
	}

	protected override void Update(GameTime gt) {
		var state = Keyboard.GetState();
		var mouse = Mouse.GetState();
		Rectangle mouseRect = new Rectangle(mouse.X, mouse.Y, 1,1);
		rect.X = Window.ClientBounds.Width/2 - rect.Width/2;
		int size = (Window.ClientBounds.Width + Window.ClientBounds.Height)/2/16;
		rect.Width = (int)(size * 1.5);
		rect.Height = size;
		Keys[] pressedKeys = state.GetPressedKeys();
		this.HandleKeyInput(pressedKeys);
		cube.HandleKeyInput(pressedKeys);
		exit.HandleMouseInput(mouse, mouseRect, gameExiter);
		this.HandleMouseInput(Mouse.GetState());
		base.Update(gt);
	}
	public void HandleMouseInput(MouseState mouse)
	{
		Rectangle mouseRect = new Rectangle(mouse.X, mouse.Y, 1,1);
		if (mouseRect.Intersects(rect))
		{
			Mouse.SetCursor(MouseCursor.Hand);

			if(mouse.LeftButton == ButtonState.Pressed)
			{
				Exit();
			}
		}
		else
		{
			Mouse.SetCursor(MouseCursor.Arrow);
		}
	}
    public void HandleKeyInput(Keys[] pressedKeys) 
    {
        if (pressedKeys.Contains(Keys.F11) & f11Clickable)
        {
            windowResizer.ResizeWindow(GraphicsDevice, Window);
        }
        f11Clickable = !pressedKeys.Contains(Keys.F11);
    }	
}