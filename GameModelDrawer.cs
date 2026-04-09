using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

class GameModelDrawer : ModelDrawer
{
    // Camera
    private Vector3 camPos = new Vector3(5, 5, 0);
    private Vector3 camRot = new Vector3(0, 0, 0);

    public GameModelDrawer() {
    }

    public void DrawModel(GraphicsDevice GraphicsDevice, Entity entity)
    {
        Vector3 rotation = entity.getRotation();
        Matrix gameWorldRotation =
                Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) *
                Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) *
                Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
        Model model = entity.getModel();
        Matrix[] transforms = new Matrix[model.Bones.Count];
        float aspectRatio = GraphicsDevice.Viewport.AspectRatio;
        model.CopyAbsoluteBoneTransformsTo(transforms);
        Matrix project = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 1000.0f);
        Matrix view = Matrix.CreateLookAt(camPos, camRot, Vector3.Up);			
        foreach (ModelMesh mesh in model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.EnableDefaultLighting();
                effect.View = view;
                effect.Projection = project;
                effect.World = gameWorldRotation*Matrix.CreateTranslation(entity.getPosition());
            }
            mesh.Draw();
        }
    }
}