using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameNMSP {

	public class MainGame : Game {
		GraphicsDeviceManager gdm;

		Vector3 camPos = new Vector3(-200, 30, 30);
		Vector3 cameraFront = new Vector3(1, 0, 0);

		KeyboardState state = new KeyboardState();

		public MainGame() {
			gdm = new GraphicsDeviceManager(this);
			gdm.GraphicsProfile = GraphicsProfile.HiDef;
		}

		protected override void Update(GameTime gt) {
			HandleInput();
			//UpdateCamera(gt);
			base.Update(gt);
		}

		private void HandleInput() {
			state = Keyboard.GetState();
			if(state.IsKeyDown(Keys.W)) {
				Exit();
			} else if (state.IsKeyDown(Keys.S)) {
				
			}
		}
	}

	

}
