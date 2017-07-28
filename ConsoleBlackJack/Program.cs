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
        private static Gambler _player = new Gambler(PlayerType.Player);
        private static Gambler _diller = new Gambler(PlayerType.Diller);

        static void Main(string[] args)
        {
            Console.WriteLine(GameTitle);
            Game game = new Game();
            Game.FirstDistribution(_player, _diller);
        }
    }
}