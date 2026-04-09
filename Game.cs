using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameNMSP {

	public class MainGame : Game {
		private ModelDrawer modelDrawer;
		private KeyHandler keyHandler;
		private WindowResizer windowResizer;

		private const int WINDOW_INIT_WIDTH = 600;
		private const int WINDOW_INIT_HEIGHT= 800;

		// Cube
		private Model? cube;
		private Vector3 position = new Vector3(0.0f,0.0f,0.0f);
		private Vector3 rotation = new Vector3(0.0f,0.0f,0.0f);

		// Window
		private GraphicsDeviceManager gdm;

		public MainGame() {
			gdm = new GraphicsDeviceManager(this);
			modelDrawer = new GameModelDrawer();
			windowResizer = new GameResizer(WINDOW_INIT_WIDTH, WINDOW_INIT_HEIGHT, gdm);
			keyHandler = new GameKeyHandler();
			
		}

		protected override void LoadContent()
		{
			Content.RootDirectory = "Content";
			cube = Content.Load<Model>("3D-Models/cube");
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
			if(cube != null)
			{
				Console.WriteLine("Drawing...");
				GraphicsDevice.Clear(Color.BlueViolet);
				Matrix gameWorldRotation =
						Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) *
						Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) *
						Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
				modelDrawer.DrawModel(GraphicsDevice, cube, position, rotation, gameWorldRotation);
			}
				
			base.Draw(gt);
        }

		protected override void Update(GameTime gt) {
			var state = Keyboard.GetState();
			Keys[] pressedKeys = state.GetPressedKeys();
			keyHandler.HandleInput(GraphicsDevice, pressedKeys, ref position, ref rotation, windowResizer, Window);

			base.Update(gt);
		}

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

		class GameKeyHandler : KeyHandler
		{
			private bool f11Clickable = true;
			private float rotSpeed = 1.0f;

			public void HandleInput(GraphicsDevice GraphicsDevice, Keys[] pressedKeys, ref Vector3 position, 
				ref Vector3 rotation, WindowResizer windowResizer, GameWindow Window) 
			{
				if (pressedKeys.Contains(Keys.W))
				{
					position.X -= 0.1f;
				}
				if (pressedKeys.Contains(Keys.S))
				{
					position.X += 0.1f;
				}
				if (pressedKeys.Contains(Keys.D))
				{
					position.Z -= 0.1f;
				}
				if (pressedKeys.Contains(Keys.A))
				{
					position.Z += 0.1f;
				}
				if(pressedKeys.Contains(Keys.X))
				{
					rotation.X += rotSpeed;
				}
				if (pressedKeys.Contains(Keys.Z))
				{
					rotation.Z += rotSpeed;
				}
				if (pressedKeys.Contains(Keys.C))
				{
					rotation.Y += rotSpeed;
					Console.WriteLine(rotation.Y);
				}

				if (pressedKeys.Contains(Keys.F11) & f11Clickable)
				{
					windowResizer.ResizeWindow(GraphicsDevice, Window);
				}
				f11Clickable = !pressedKeys.Contains(Keys.F11);
			}
		}

		class GameModelDrawer : ModelDrawer
		{
			// Camera
			private Vector3 camPos = new Vector3(5, 5, 0);
			private Vector3 camRot = new Vector3(0, 0, 0);

			public GameModelDrawer() {
			}

			public void DrawModel(GraphicsDevice GraphicsDevice, Model model, Vector3 position, Vector3 rotation, Matrix gameWorldRotation)
			{
				Console.WriteLine("Drawing Model...");
				Matrix[] transforms = new Matrix[model.Bones.Count];
				float aspectRatio = GraphicsDevice.Viewport.AspectRatio;
				model.CopyAbsoluteBoneTransformsTo(transforms);
				Matrix project = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
					aspectRatio, 1.0f, 1000.0f);
				Matrix view = Matrix.CreateLookAt(camPos, camRot, Vector3.Up);			
				
				foreach (ModelMesh mesh in model.Meshes)
				{
					foreach (BasicEffect effect in mesh.Effects)
					{
						effect.EnableDefaultLighting();

						effect.View = view;
						effect.Projection = project;
						effect.World = gameWorldRotation*Matrix.CreateTranslation(position);
					}
					mesh.Draw();
				}
			}
		}
	}	
}