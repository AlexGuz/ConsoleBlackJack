using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBlackJack
{
    class Program
    {
        private const string GameTitle = "Playing BlackJack";        

        static void Main(string[] args)
        {
            Console.WriteLine(GameTitle);
            Game game = new Game();
            GameProvider.FirstDistribution(game.player, game.diller);
        }
    }
}