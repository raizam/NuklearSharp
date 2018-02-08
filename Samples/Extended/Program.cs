namespace Extended
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var game = new ExtendedGame())
			{
				game.Run();
			}
		}
	}
}
