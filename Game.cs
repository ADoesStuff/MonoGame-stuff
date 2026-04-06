using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameNMSP {

	public class MainGame : Game, ModelDrawer {
<<<<<<< HEAD
		GraphicsDeviceManager gdm;
		bool f11Clickable = true;
		int startResizeHeight = 600;
		int startResizeWidth = 800;
		int winPosX;
		int winPosY;
=======
		// Window
		private GraphicsDeviceManager gdm;
		public Matrix gameWorldRotation;
		private KeyboardState state = new KeyboardState();
		private bool f11Clickable = true;
		
		// Cube
>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
		private Model? cube;
		private Vector3 position = new Vector3(0.0f,0.0f,0.0f);
		private float rotationY = 0.0f;
		private float rotationX = 0.0f;
		private float rotationZ = 0.0f;
<<<<<<< HEAD
		public Matrix gameWorldRotation;
		Vector3 camPos = new Vector3(5, 5, 5);
		Vector3 camRot = new Vector3(0, 1, 0);
		float rotSpeed = 1.0f;
		float rotAng = 0;
		KeyboardState state = new KeyboardState();
=======
		private float rotSpeed = 1.0f;
		private float rotAng = 0;

		// Camera
		private Vector3 camPos = new Vector3(5, 5, 5);
		private Vector3 camRot = new Vector3(0, 1, 0);

>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
		public MainGame() {
			gdm = new GraphicsDeviceManager(this);
<<<<<<< HEAD
			



			//gdm.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
    		//gdm.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			
			
	

			// Content.RootDirectory = "Content";
			// cube = Content.Load<Model>("3D-Models/cube");
=======
>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
		}
		
		protected override void Initialize()
        {
			gdm.HardwareModeSwitch = false;
			gdm.IsFullScreen = false;
			Window.AllowUserResizing = true;
			gdm.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			gdm.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			gdm.ApplyChanges();
			base.Initialize();
        }

		protected override void LoadContent()
		{
			Content.RootDirectory = "Content";
			cube = Content.Load<Model>("3D-Models/cube");
		}
		
		protected override void Draw(GameTime gt)
        {
			if(cube != null)
			{
				GraphicsDevice.Clear(Color.BlueViolet);
				DrawModel(cube);
			}
			base.Draw(gt);
        }

<<<<<<< HEAD
        public void DrawModel(Model model)
=======
		public void DrawModel(Model model)
>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
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
<<<<<<< HEAD
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
				DrawModel(cube);
			}
				
			base.Draw(gt);
        }
=======
>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
		
		protected override void Update(GameTime gt) {
			HandleInput();
			rotationY += rotSpeed;
			gameWorldRotation =
				Matrix.CreateRotationX(MathHelper.ToRadians(rotationX)) *
				Matrix.CreateRotationY(MathHelper.ToRadians(rotationY)) *
				Matrix.CreateRotationZ(MathHelper.ToRadians(rotationZ));

<<<<<<< HEAD
			//UpdateCamera(gt);
=======
>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
			base.Update(gt);
		}

		private void HandleInput() {			
			state = Keyboard.GetState();
			Keys[] pressedKeys = state.GetPressedKeys();
			if (pressedKeys.Contains(Keys.W))
			{
				position.X += 0.1f;
			}
			if (pressedKeys.Contains(Keys.S))
			{
				position.X -= 0.1f;
			}
			if (pressedKeys.Contains(Keys.D))
			{
				position.Z += 0.1f;
			}
			if (pressedKeys.Contains(Keys.A))
			{
				position.Z -= 0.1f;
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
<<<<<<< HEAD
			if (pressedKeys.Contains(Keys.F11) & f11Clickable)
			{
				FullscreenSwitch();
=======
			
			if (pressedKeys.Contains(Keys.F11) && f11Clickable)
			{
				switchFullScreen();
>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
				f11Clickable = false;
			}
			f11Clickable = !pressedKeys.Contains(Keys.F11);
		}
<<<<<<< HEAD
		private void FullscreenSwitch()
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
=======

		private void switchFullScreen() {
>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
			gdm.IsFullScreen = !gdm.IsFullScreen;
			gdm.ApplyChanges();
		}
	}
<<<<<<< HEAD
=======

>>>>>>> b558549432cf688b0b50551575fe934460a4fe28
}
