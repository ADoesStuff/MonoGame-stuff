using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Entity;

namespace Graphics;
public interface IModelDrawer {
    public void DrawModel(GraphicsDevice GraphicsDevice, AbstractEntity entity);
}