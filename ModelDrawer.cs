using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public interface ModelDrawer {
    public void DrawModel(GraphicsDevice GraphicsDevice, Entity entity);
}