using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface ModelDrawer {
    public void DrawModel(GraphicsDevice GraphicsDevice, Model model, Vector3 position, Vector3 rotation, Matrix gameWorldRotation);
}