namespace RaizamTest
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var game = new FileBrowserGame())
			{
				game.Run();
			}
		}
	}
}
