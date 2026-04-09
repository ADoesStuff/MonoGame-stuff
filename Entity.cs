using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

public abstract class Entity {
    private Model? model;
	protected Vector3 position = new Vector3(0.0f,0.0f,0.0f);
	protected Vector3 rotation = new Vector3(0.0f,0.0f,0.0f);

    public void load(ContentManager Content)
    {
        model = loadModel(Content);
    }

    protected abstract Model loadModel(ContentManager Content);

    public Model getModel()
    {
        if(model == null)
            throw new Exception("No model is loaded.");
        return model;
    }

    public Vector3 getPosition()
    {
        return position;
    }

    public Vector3 getRotation()
    {
        return rotation;
    }
}