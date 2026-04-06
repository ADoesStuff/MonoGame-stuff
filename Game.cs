using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameNMSP {

	public class MainGame : Game, ModelDrawer {
		// Window
		private GraphicsDeviceManager gdm;
		public Matrix gameWorldRotation;
		private KeyboardState state = new KeyboardState();
		private bool f11Clickable = true;
		
		// Cube
		private Model? cube;
		private Vector3 position = new Vector3(0.0f,0.0f,0.0f);
		private float rotationY = 0.0f;
		private float rotationX = 0.0f;
		private float rotationZ = 0.0f;
		private float rotSpeed = 1.0f;
		private float rotAng = 0;

		// Camera
		private Vector3 camPos = new Vector3(5, 5, 5);
		private Vector3 camRot = new Vector3(0, 1, 0);

		public MainGame() {
			gdm = new GraphicsDeviceManager(this);
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
		
		protected override void Update(GameTime gt) {
			HandleInput();
			rotationY += rotSpeed;
			gameWorldRotation =
				Matrix.CreateRotationX(MathHelper.ToRadians(rotationX)) *
				Matrix.CreateRotationY(MathHelper.ToRadians(rotationY)) *
				Matrix.CreateRotationZ(MathHelper.ToRadians(rotationZ));

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
			
			if (pressedKeys.Contains(Keys.F11) && f11Clickable)
			{
				switchFullScreen();
				f11Clickable = false;
			}
			f11Clickable = !pressedKeys.Contains(Keys.F11);
		}

		private void switchFullScreen() {
			gdm.IsFullScreen = !gdm.IsFullScreen;
			gdm.ApplyChanges();
		}
	}

}
