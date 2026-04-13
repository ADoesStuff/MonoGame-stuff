public abstract class AbstractElement
{
    public Texture2D? texture;
    public SpriteBatch sprtBatch;
    public Rectangle rectangle;
    public virtual void load(ContentManager content, GraphicsDevice graphicsDevice)
    {
        sprtBatch = new SpriteBatch(graphicsDevice);
        texture = loadTexture(content);
        rectangle = new Rectangle();
    }
    public abstract Texture2D loadTexture(ContentManager content);
    public Texture2D getTexture()
    {
        if(texture == null)
            throw new Exception("No texture is loaded.");
        return texture;
    }
    public virtual void Draw()
    {
        sprtBatch.Begin();
        sprtBatch.Draw(texture, rectangle, Color.Transparent);
        sprtBatch.End();
    }
    public virtual void Update()
    {
        
    }
}