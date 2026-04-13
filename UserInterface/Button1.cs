

class ExitButton : AbstractElement, IMouseHandler
{
    public override void load(ContentManager content, GraphicsDevice graphicsDevice)
    {
        base.load(content, graphicsDevice);
		rectangle = new Rectangle(100, 100, 100, 100);
    }
	
	public override Texture2D loadTexture(ContentManager content)
    {
        return content.Load<Texture2D>("2D-Images/EXIT");
    }
	public void HandleMouseInput(MouseState mouse){}
    public void HandleMouseInput(MouseState mouseState, IExiter exiter)
    {
		Rectangle mouseRect = new Rectangle(mouseState.X, mouseState.Y, 1,1);
		if (mouseRect.Intersects(rectangle))
		{
			Mouse.SetCursor(MouseCursor.Hand);

			if(mouseState.LeftButton == ButtonState.Pressed)
			{
				exiter.Exit();
			}
		}
		else
		{
			Mouse.SetCursor(MouseCursor.Arrow);
		}
    }
    public override void Draw()
    {
        sprtBatch.Begin();
		sprtBatch.Draw(texture,rectangle,Color.White);
		sprtBatch.End();
    }
    public void HandleMouseInput(MouseState mouseState, Rectangle mouseRect, IExiter exiter)
    {
		if (mouseRect.Intersects(rectangle))
		{
			Mouse.SetCursor(MouseCursor.Hand);
			Console.WriteLine("hovering over the button");

			if(mouseState.LeftButton == ButtonState.Pressed)
			{
				Console.WriteLine("button pressed");
				exiter.Exit();
			}
		}
		else
		{
			Mouse.SetCursor(MouseCursor.Arrow);
		}
    }
}