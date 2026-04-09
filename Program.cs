class Program {
	static void Main() {
		try {
			MainGame game = new MainGame();
			game.Run();
		} catch (Exception e) {
			System.Console.WriteLine(e);
		}
	}
}
