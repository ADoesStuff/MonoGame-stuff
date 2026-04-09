using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class MainGame : Game
{
	private ModelDrawer modelDrawer;
	private KeyHandler keyHandler;
	private WindowResizer windowResizer;

	private const int WINDOW_INIT_WIDTH = 600;
	private const int WINDOW_INIT_HEIGHT= 800;

	private Cube? cube;
	private GraphicsDeviceManager gdm;

	public MainGame() {
		gdm = new GraphicsDeviceManager(this);
		modelDrawer = new GameModelDrawer();
		windowResizer = new GameResizer(WINDOW_INIT_WIDTH, WINDOW_INIT_HEIGHT, gdm);
		keyHandler = new GameKeyHandler();
		cube = new Cube();
	}

	protected override void LoadContent()
	{
		Content.RootDirectory = "Content";
		cube.load(Content);
	}

	protected override void Initialize()
	{
		gdm.HardwareModeSwitch = false;
		gdm.IsFullScreen = false;
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
		}		
		base.Draw(gt);
	}

	protected override void Update(GameTime gt) {
		var state = Keyboard.GetState();
		Keys[] pressedKeys = state.GetPressedKeys();
		keyHandler.HandleInput(GraphicsDevice, pressedKeys, windowResizer, Window);
		cube.HandleInput(pressedKeys);
		base.Update(gt);
	}		
}