using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameNMSP {

	public class MainGame : Game {
		GraphicsDeviceManager gdm;
		bool nospam = true;
		private Model? cube;
		private Vector3 position = new Vector3(0.0f,0.0f,0.0f);
		private float rotationY = 0.0f;
		private float rotationX = 0.0f;

		private float rotationZ = 0.0f;

		public Matrix gameWorldRotation;

		Vector3 camPos = new Vector3(5, 5, 5);
		Vector3 camRot = new Vector3(0, 1, 0);

		float rotSpeed = 1.0f;
		float rotAng = 0;

		KeyboardState state = new KeyboardState();

		public MainGame() {

			
			gdm = new GraphicsDeviceManager(this);


			//gdm.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
    		//gdm.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			
			
	

			// Content.RootDirectory = "Content";
			// cube = Content.Load<Model>("3D-Models/cube");
		}
		protected override void LoadContent()
		{
			Content.RootDirectory = "Content";
			cube = Content.Load<Model>("3D-Models/cube");
			
			Console.WriteLine("Initializing the cube...");
		}

        private void DrawModel(Model model)
		{
			Matrix[] transforms = new Matrix[model.Bones.Count];
			float aspectRatio = GraphicsDevice.Viewport.AspectRatio;
			model.CopyAbsoluteBoneTransformsTo(transforms);
			Matrix project =
				Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
				aspectRatio, 1.0f, 1000.0f);
			Matrix view = Matrix.CreateLookAt(camPos, camRot, Vector3.Up);
			//Matrix world = /*Matrix.CreateTranslation(new Vector3(0, 0, 0)) +*/ Matrix.CreateRotationX(rotAng);
			
			
			foreach (ModelMesh mesh in model.Meshes)
			{
				foreach (BasicEffect effect in mesh.Effects)
				{
					effect.EnableDefaultLighting();

					effect.View = view;
					effect.Projection = project;
					effect.World = gameWorldRotation/*transforms[mesh.ParentBone.Index]*/*Matrix.CreateTranslation(position);
				}
				mesh.Draw();
			}
		}
        protected override void Initialize()
        {
			gdm.HardwareModeSwitch = false;
			gdm.IsFullScreen = false;
			Window.AllowUserResizing = true;
			gdm.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			gdm.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
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
		
		protected override void Update(GameTime gt) {
			HandleInput();
			rotationY += rotSpeed;
			//UpdateCamera(gt);
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
			if (pressedKeys.Contains(Keys.F11) & nospam)
			{
				if (gdm.IsFullScreen == true)
				{
					gdm.IsFullScreen = false;
					gdm.ApplyChanges();
					nospam = false;
				}
				else if (gdm.IsFullScreen == false)
				{
					gdm.IsFullScreen = true;
					gdm.ApplyChanges();
					nospam = false;
				}
			}
			if (!pressedKeys.Contains(Keys.F11) & !nospam)
			{
				nospam = true;
			}
			gameWorldRotation =
				Matrix.CreateRotationX(MathHelper.ToRadians(rotationX)) *
				Matrix.CreateRotationY(MathHelper.ToRadians(rotationY)) *
				Matrix.CreateRotationZ(MathHelper.ToRadians(rotationZ));
		}
	}

	

}
