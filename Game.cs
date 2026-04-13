using Input;
using Graphics;
using Entity;

public class MainGame : Game, IKeyHandler, IExiter
{
	private IModelDrawer modelDrawer;
	private IWindowResizer windowResizer;

	private const int WINDOW_INIT_WIDTH = 600;
	private const int WINDOW_INIT_HEIGHT= 800;
	private bool f11Clickable = true;
	private SpriteBatch spriteBatch;

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
		Keys[] pressedKeys = state.GetPressedKeys();
		this.HandleKeyInput(pressedKeys);
		cube.HandleKeyInput(pressedKeys);
		exit.HandleMouseInput(mouse, mouseRect, this);
		base.Update(gt);
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