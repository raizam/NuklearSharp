﻿namespace RaizamTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new GameTest())
            {
                game.Run();
            }
        }
    }
}
