using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameNMSP {

	public class MainGame : Game, ModelDrawer, KeyHandler, WindowResizer {
		private ModelDrawer modelDrawer;
		private KeyHandler keyHandler;
		private WindowResizer windowResizer;
		
		private int startResizeHeight = 600;
		private int startResizeWidth = 800;
		private int winPosX;
		private int winPosY;

		// Window
		private GraphicsDeviceManager gdm;
		public Matrix gameWorldRotation;
		private bool f11Clickable = true;
		
		// Cube
		private Model? cube;
		private Vector3 position = new Vector3(0.0f,0.0f,0.0f);
		private float rotationY = 0.0f;
		private float rotationX = 0.0f;
		private float rotationZ = 0.0f;

		// Camera
		private Vector3 camPos = new Vector3(5, 5, 0);
		private Vector3 camRot = new Vector3(0, 0, 0);
		private float rotSpeed = 1.0f;
		private float rotAng = 0;

		public MainGame() {
			gdm = new GraphicsDeviceManager(this);
			modelDrawer = this;
			windowResizer = this;
			keyHandler = this;
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
			gdm.PreferredBackBufferHeight = startResizeHeight;
			gdm.PreferredBackBufferWidth = startResizeWidth;
			gdm.ApplyChanges();
			Console.WriteLine("Height:"+GraphicsDevice.Adapter.CurrentDisplayMode.Height+" Width:"+GraphicsDevice.Adapter.CurrentDisplayMode.Width+" something idek:"+gdm.PreferredBackBufferHeight);
			base.Initialize();
        }
		
		protected override void Draw(GameTime gt)
        {
			if(cube != null)
			{
				GraphicsDevice.Clear(Color.BlueViolet);
				modelDrawer.DrawModel(cube);
			}
				
			base.Draw(gt);
        }

		protected override void Update(GameTime gt) {
			var state = Keyboard.GetState();
			Keys[] pressedKeys = state.GetPressedKeys();
			keyHandler.HandleInput(pressedKeys);

			rotationY += rotSpeed;
			gameWorldRotation =
				Matrix.CreateRotationX(MathHelper.ToRadians(rotationX)) *
				Matrix.CreateRotationY(MathHelper.ToRadians(rotationY)) *
				Matrix.CreateRotationZ(MathHelper.ToRadians(rotationZ));

			base.Update(gt);
		}

		public void DrawModel(Model model)
		{
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

		public void HandleInput(Keys[] pressedKeys)
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
				rotationX += rotSpeed;
			}
			if (pressedKeys.Contains(Keys.Z))
			{
				rotationZ += rotSpeed;
			}
			if (pressedKeys.Contains(Keys.C))
			{
				rotationY += rotSpeed;
			}

			if (pressedKeys.Contains(Keys.F11) & f11Clickable)
			{
				windowResizer.ResizeWindow(gdm);
			}
			f11Clickable = !pressedKeys.Contains(Keys.F11);
		}
		
		public void ResizeWindow(GraphicsDeviceManager gdm)
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
}